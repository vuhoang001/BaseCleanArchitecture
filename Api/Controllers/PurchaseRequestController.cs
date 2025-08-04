using Application.Dtos;
using Application.PurchaseRequest.Commands.Create;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PurchaseRequestController(ISender sender) : ControllerBase
{
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateAsync([FromBody] PurchaseRequestDto pr)
    {
        var command = new CreatePurchaseRequestCommand(pr);
        var result  = await sender.Send(command);
        return Ok(result);
    }
}