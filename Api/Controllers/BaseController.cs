using Microsoft.AspNetCore.Mvc;
using Shared.ExceptionBase;

namespace Api.Controllers;

public class BaseController : ControllerBase
{
    protected IActionResult HandleResult<T>(Result<T> result, string successMessage = null)
    {
        var response = result.ToApiResponse(successMessage);

        if (result.IsSuccess)
        {
            return Ok(response);
        }

        return StatusCode((int)result.StatusCode, response);
    }

    protected IActionResult HandleResult(Result result, string successMessage = null)
    {
        var response = result.ToApiResponse(successMessage);

        if (result.IsSuccess)
        {
            return Ok(response);
        }

        return StatusCode((int)result.StatusCode, response);
    }

    protected IActionResult HandleResult<T>(Result<T> result, string successMessage, string location = null)
    {
        var response = result.ToApiResponse(successMessage);

        if (result.IsSuccess)
        {
            return string.IsNullOrEmpty(location)
                ? Created("", response) // 201 Created
                : Created(location, response);
        }

        return StatusCode((int)result.StatusCode, response);
    }
}