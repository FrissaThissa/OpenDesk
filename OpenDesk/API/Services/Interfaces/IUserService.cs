using API.Models.Auth;

namespace API.Services.Interfaces;

public interface IUserService
{
    public Task<ApplicationUser?> GetUserById(int id);
}
