using Application.Commands;
using Application.Common;
using Infrastructure.Models;
using Infrastructure.Repository.IRepository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Command_Handler
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, APIResponse>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<APIResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                UserName = request.Username,
                Email = request.Email
            };

            var result = await _userRepository.CreateUserAsync(user, request.Password);

            if (result.Succeeded)
            {
                return new APIResponse { Success = true, Message = "User added successfully." };
            }
            else
            {
                return new APIResponse
                {
                    Success = false,
                    Message = string.Join(", ", result.Errors.Select(e => e.Description))
                };
            }
        }
    }
}
