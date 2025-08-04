using System.Security.Claims;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Middleware;

public class UserContextMiddleware
{
    private readonly RequestDelegate  _next;
    private readonly IServiceProvider _serviceProvider;

    public UserContextMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
    {
        _next            = next;
        _serviceProvider = serviceProvider;
    }

    public async Task InvokeAsync(HttpContext context, IUserContext userContext)
    {
        if (context.User.Identity?.IsAuthenticated ?? false)
        {
            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

            using var scope          = _serviceProvider.CreateScope();
            var       userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();

            if (userId is not null)
            {
                var res = await userRepository.GetByIdAsync(userId);
                if (res is not null)
                {
                    userContext.UserId   = res.Id;
                    userContext.Email    = res.Email;
                    userContext.UserName = res.UserName;
                }
            }
        }

        await _next(context);
    }
}