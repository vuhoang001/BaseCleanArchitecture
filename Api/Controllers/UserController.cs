using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Dtos;
using Application.User.Commands.Login;
using Application.User.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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
}