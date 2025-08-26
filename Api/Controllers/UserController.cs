using System.Security.Claims;
using Application.Dtos;
using Application.Handlers.User.Commands.Login;
using Application.Handlers.User.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(ISender sender) : BaseController
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginIn)
    {
        var req    = new LoginCommand(loginIn);
        var result = await sender.Send(req);
        return HandleResult(result);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateRegisterRequest loginIn)
    {
        var req    = new RegisterCommand(loginIn);
        var result = await sender.Send(req);
        return Ok(result);
    }

    [HttpGet("google-login")]
    public IActionResult GoogleLogin()
    {
        var properties = new AuthenticationProperties
        {
            RedirectUri = "/api/User/google-callback"
        };
        return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    }

    [HttpGet("google-callback")]
    public async Task<IActionResult> GoogleCallback()
    {
        var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        if (!result.Succeeded)
        {
            return BadRequest("Google authentication failed");
        }

        var email    = result.Principal.FindFirst(ClaimTypes.Email)?.Value;
        var name     = result.Principal.FindFirst(ClaimTypes.Name)?.Value;
        var googleId = result.Principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        // Tạo JWT token thay vì trả về email

        return Redirect($"http://localhost:3000/auth/success?token={email}");
    }
}