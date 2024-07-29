using API.Models;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers;

public class BaseController : ControllerBase
{
    private readonly IUserService _userService;

    public BaseController(IUserService userService)
    {
        _userService = userService;
    }

    protected async Task<ApplicationUser?> GetCurrentUserAsync()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
            return null;

        return await _userService.GetUserById(userId);
    }
}