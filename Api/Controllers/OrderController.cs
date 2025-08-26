using Application.Dtos;
using Application.Handlers.Order.Commands.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : BaseController
{
    private readonly ISender _sender;

    // Constructor nhận ISender thông qua Dependency Injection
    public OrderController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateOrderRequest order)
    {
        var command = new CreateOrderCommand(order);
        var result  = await _sender.Send(command);
        return HandleResult(result, "Tạo đơn hàng thành công");
    }
}