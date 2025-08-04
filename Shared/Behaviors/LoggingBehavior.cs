using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Shared.Behaviors;

public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("[START] Handler Request={request} - Response={response} - RequestData={requestData}",
            typeof(TRequest).Name, typeof(TResponse).Name, request);

        var timer = new Stopwatch();
        timer.Start();

        var response = await next();

        timer.Stop();
        var timeTaken = timer.Elapsed;
        if (timeTaken > TimeSpan.FromSeconds(3))
            logger.LogWarning("[PERFORMANCE] the request {request} took {timeTaken}", typeof(TRequest).Name,
                timeTaken.Seconds);

        logger.LogInformation("[END] Handle {request} with {response}", typeof(TRequest).Name, response);

        return response;
    }
}