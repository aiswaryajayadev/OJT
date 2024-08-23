using Infrastructure.Models;
using Infrastructure.Models.DTO;
using Infrastructure.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;

        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> CreateUserAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<IEnumerable<DisplayUserDTO>> UserListAsync()
        {
            return await _userManager.Users
                .Select(u => new DisplayUserDTO
                {
                    Id = u.Id,
                    Username = u.UserName,
                    Email = u.Email
                })
                .ToListAsync();
        }
        public async Task<User> FindByIdAsync(int userId)
        {
            return await _userManager.FindByIdAsync(userId.ToString());
        }

        public async Task<IdentityResult> UpdateUserAsync(User user)
        {
            return await _userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> RemovePasswordAsync(User user)
        {
            return await _userManager.RemovePasswordAsync(user);
        }

        public async Task<IdentityResult> AddPasswordAsync(User user, string newPassword)
        {
            return await _userManager.AddPasswordAsync(user, newPassword);
        }
    }
}