using Application.Commands;
using Application.Common;
using Infrastructure.Models;
using Infrastructure.Repository.IRepository;
using MediatR;

using Serilog;


namespace Application.Command_Handler
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, APIResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger _logger;

        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _logger = Log.ForContext<CreateUserCommandHandler>();
        }

        public async Task<APIResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            _logger.Information("Creating user: {@Username}", request.Username);
            var user = new User
            {
                UserName = request.Username,
                Email = request.Email
            };

            var result = await _userRepository.CreateUserAsync(user, request.Password);

            if (result.Succeeded)
            {
                _logger.Information("User {Username} created successfully", request.Username);
                return new APIResponse { Success = true, Message = "User added successfully." };
            }
            else
            {
                _logger.Warning("Failed to create user {Username}. Errors: {Errors}", request.Username, result.Errors);
                return new APIResponse
                {

                    Success = false,
                    Message = string.Join(", ", result.Errors.Select(e => e.Description))
                };
            }
        }
    }
}
