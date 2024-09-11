using Application.Commands;
using Infrastructure.Models;
using Infrastructure.Models.DTO;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Command_Handler
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponseDTO>
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public LoginCommandHandler(SignInManager<User> signInManager, UserManager<User> userManager, IConfiguration configuration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
            _logger = Log.ForContext<CreateUserCommandHandler>();
        }

        public async Task<LoginResponseDTO> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            _logger.Information("Login attempt for Username: {Username}", request.Username);

            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null)
            {
                _logger.Warning("Login failed for Username: {Username}. User not found.", request.Username);
                throw new Exception("Invalid login attempt.");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded)
            {
                _logger.Warning("Login failed for Username: {Username}. Incorrect password.", request.Username);
                throw new Exception("Invalid login attempt.");
            }

            _logger.Information("Login successful for Username: {Username}. Generating JWT.", request.Username);

            string token = await GenerateJwtToken(user);

            _logger.Information("JWT generated for Username: {Username}", request.Username);

            return new LoginResponseDTO
            {
                Token = token,
                Username = user.UserName,
                Email = user.Email
            };
        }

        public async Task<string> GenerateJwtToken(User user)
        {
            _logger.Information("Generating JWT token for user {UserId}", user.Id);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
                _logger.Information("Added role {Role} to JWT for user {UserId}", role, user.Id);
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
