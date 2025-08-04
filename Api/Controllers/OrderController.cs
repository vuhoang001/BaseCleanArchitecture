using Application.Dtos;
using Application.Order.Commands.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateOrderDto order)
    {
        var command = new CreateOrderCommand(order);
        var result  = await sender.Send(command);
        return Ok(result);
    }
}