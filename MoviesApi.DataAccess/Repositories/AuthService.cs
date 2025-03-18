using Microsoft.AspNetCore.Identity;
using MoviesApi.Core.Dtos;
using MoviesApi.Core.IRepositories;
using MoviesApi.Core.Models;


namespace MoviesApi.DataAccess.Repositories
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            
        }
        public async Task<string> AddRoleAsync(AddRoleDto addRoleDto)
        {
            var user = await _userManager.FindByIdAsync(addRoleDto.UserId);

            if (user == null || !await _roleManager.RoleExistsAsync(addRoleDto.Role))
                return "Invalid UserId or Role";

            if(await _userManager.IsInRoleAsync(user,addRoleDto.Role))
                return "User Already Assigned to this Role";

            var result = await _userManager.AddToRoleAsync(user, addRoleDto.Role);

            return result.Succeeded ? string.Empty : "SomThing Wont Wrong";
        }
    }
}
