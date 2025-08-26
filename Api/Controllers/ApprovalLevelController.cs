using Application.Handlers.ApprovalLevel.Commands.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApprovalLevelController : BaseController
{
    private readonly ISender _sender;

    public ApprovalLevelController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateApprovalLevel request)
    {
        var command = new CreateApprovalLevelCommand(request);
        var handler = await _sender.Send(command);
        return HandleResult(handler);
    }
}