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
            
            if (user == null) throw new ArgumentNullException(nameof(user)); // Null check for user
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Password cannot be null or whitespace.", nameof(password)); // Check password validity

            return await _userManager.CreateAsync(user, password);
        }


        public async Task<IEnumerable<DisplayUserDTO>> UserListAsync()
        {
            try
            {
                var users = await _userManager.Users.ToListAsync();

                if (users == null || !users.Any()) return Enumerable.Empty<DisplayUserDTO>();

                return await MapUsersToDTOsAsync(users);
            }
            catch (Exception ex)
            {
                
                throw new ApplicationException("An error occurred while retrieving the user list.", ex);
            }
        }

        private async Task<List<DisplayUserDTO>> MapUsersToDTOsAsync(IEnumerable<User> users)
        {
            var userDTOs = new List<DisplayUserDTO>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user); // Fetch roles for each user

                var userDTO = new DisplayUserDTO
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Email = user.Email,
                    Roles = roles?.ToList() ?? new List<string>() // Ensure roles are not null
                };

                userDTOs.Add(userDTO);
            }

            return userDTOs;
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