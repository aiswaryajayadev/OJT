using Infrastructure.Models;
using Infrastructure.Models.DTO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<IdentityResult> CreateUserAsync(User user, string password);
        Task<IEnumerable<DisplayUserDTO>> UserListAsync();
        Task<User> FindByIdAsync(int userId);
        Task<IdentityResult> UpdateUserAsync(User user);
        Task<IdentityResult> RemovePasswordAsync(User user);
        Task<IdentityResult> AddPasswordAsync(User user, string newPassword);
    }
}
