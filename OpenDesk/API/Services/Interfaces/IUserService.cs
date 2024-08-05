using API.Models.Auth;
using Shared.Models;

namespace API.Services.Interfaces;

public interface IUserService
{
    public Task<ApplicationUser?> GetUserById(int id);
    public Task<ApplicationUser?> GetCurrentUser();
    public Task<UserDto?> GetCurrentUserDto();
}
