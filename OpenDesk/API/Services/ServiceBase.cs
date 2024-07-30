using API.Extensions;
using API.Models.Auth;
using API.Services.Interfaces;

namespace API.Services;

public class ServiceBase
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserService _userService;

    public ServiceBase(IHttpContextAccessor httpContextAccessor, IUserService userService)
    {
        _httpContextAccessor = httpContextAccessor;
        _userService = userService;
    }

    public async Task<ApplicationUser?> GetCurrentUser()
    {
        if (_httpContextAccessor.HttpContext == null)
            return null;

        var userId = _httpContextAccessor.HttpContext.User.GetLoggedInUserId<int>();
        return await _userService.GetUserById(userId);
    }
}
