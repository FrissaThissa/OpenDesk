using API.Data;
using API.Models.Auth;
using API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class UserService : IUserService
{
    private readonly OpenDeskContext _context;

    public UserService(OpenDeskContext context)
    {
        _context = context;
    }

    public async Task<ApplicationUser?> GetUserById(int id)
    {
        return await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == id);
    }
}
