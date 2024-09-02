using Application.Queries;
using Infrastructure.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;


namespace Application.Query_Handler
{
    public class GetUserRolesQueryHandler : IRequestHandler<GetUserRolesQuery, IList<string>>
    {
        private readonly UserManager<User> _userManager;
        public GetUserRolesQueryHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IList<string>> Handle(GetUserRolesQuery request,CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null) return new List<string>();

            return await _userManager.GetRolesAsync(user);
        }
    }
}