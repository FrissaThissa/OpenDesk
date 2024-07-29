using API.Data;
using API.Models;
using API.Services.Interfaces;

namespace API.Services;

public class UserService : IUserService
{
    private readonly OpenDeskContext _context;

    public UserService(OpenDeskContext context)
    {
        _context = context;
    }

    public async Task<ApplicationUser> GetUserById(string id)
    {
        throw new NotImplementedException();
    }
}
