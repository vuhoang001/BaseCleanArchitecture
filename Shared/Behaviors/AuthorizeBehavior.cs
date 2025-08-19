using MediatR;
using Microsoft.Extensions.Logging;
using Shared.Extensions;
using Shared.Interfaces;

namespace Shared.Behaviors;

public class AuthorizeBehavior<TRequest, TResponse>(
    ILogger<AuthorizeBehavior<TRequest, TResponse>> logger,
    IUserContextService userContextService)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var authorizeAttributes =
            (PermissionAttribute[])Attribute.GetCustomAttributes(typeof(TRequest), typeof(PermissionAttribute));

        if (authorizeAttributes.Length == 0)
            return await next();

        var userClaims = userContextService.GetCurrencyUserClaims();
        var userRoles  = userContextService.GetCurrencyUserRole();

        var userPermissions = new List<string>();
        userPermissions.AddRange(userClaims);
        userPermissions.AddRange(userRoles);


        foreach (var attribute in authorizeAttributes)
        {
            var requiredPermissions = attribute.RoleAndPermissions;

            var missingPermissions = new List<string>();

            foreach (var requiredPermission in requiredPermissions)
            {
                var hasPermission = userPermissions.Contains(requiredPermission, StringComparer.OrdinalIgnoreCase);

                if (!hasPermission)
                {
                    missingPermissions.Add(requiredPermission);
                }
            }

            if (missingPermissions.Any())
            {
                logger.LogWarning("User lacks required permissions: [{MissingPermissions}] for {RequestType}",
                    string.Join(", ", missingPermissions), typeof(TRequest).Name);

                throw new UnauthorizedAccessException(
                    $"You do not have required permissions: {string.Join(", ", missingPermissions)}");
            }

            logger.LogDebug("All permission checks passed for attribute: [{RequiredPermissions}]",
                string.Join(", ", requiredPermissions));
        }

        logger.LogInformation("Authorization successful for {RequestType}", typeof(TRequest).Name);

        var response = await next();
        return response;
    }
}