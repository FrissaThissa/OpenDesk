using API.Data;
using API.Extensions;
using API.Models.Auth;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace API.Services;

public class UserService : IUserService
{
    private readonly OpenDeskContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(OpenDeskContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ApplicationUser?> GetUserById(int id)
    {
        return await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<ApplicationUser?> GetCurrentUser()
    {
        if (_httpContextAccessor.HttpContext == null)
            return null;

        var userId = _httpContextAccessor.HttpContext.User.GetLoggedInUserId<int>();
        return await GetUserById(userId);
    }

    public async Task<UserDto?> GetCurrentUserDto()
    {
        if (_httpContextAccessor.HttpContext == null)
            return null;

        var userId = _httpContextAccessor.HttpContext.User.GetLoggedInUserId<int>();
        ApplicationUser? user = await GetUserById(userId);
        if (user == null)
            return null;

        return ConvertObjectToDto(user);
    }

    private UserDto ConvertObjectToDto(ApplicationUser user)
    {
        UserDto dto = new UserDto()
        {
            Id = user.Id,
            Name = user.UserName,
            Email = user.Email
        };
        return dto;
    }
}
