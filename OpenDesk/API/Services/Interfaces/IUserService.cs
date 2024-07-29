using API.Models;

namespace API.Services.Interfaces;

public interface IUserService
{
    public Task<ApplicationUser> GetUserById(string id);
}
