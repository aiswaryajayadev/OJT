using Application.Command_Handler;
using Application.Queries;
using Infrastructure.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Serilog;

public class GetUserRolesQueryHandler : IRequestHandler<GetUserRolesQuery, IList<string>>
{
    private readonly UserManager<User> _userManager;
    private readonly ILogger _logger;

    public GetUserRolesQueryHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
        _logger = Log.ForContext<CreateUserCommandHandler>();
    }

    public async Task<IList<string>> Handle(GetUserRolesQuery request, CancellationToken cancellationToken)
    {
        _logger.Information("Handling GetUserRolesQuery for UserId: {@UserId}", request.UserId);

        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null)
        {
            _logger.Warning("User with Id: {@UserId} not found.", request.UserId);
            return new List<string>();
        }

        _logger.Information("Found User: {@UserName}, retrieving roles.", user.UserName);

        var roles = await _userManager.GetRolesAsync(user);

        _logger.Information("Roles retrieved for User: {@UserName}: {@Roles}", user.UserName, roles);

        return roles;
    }
}
