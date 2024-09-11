using MediatR;
using Serilog;


public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
     where TRequest : IRequest<TResponse>
{
    private readonly ILogger _logger;

    public LoggingBehavior()
    {
        _logger = Log.ForContext<TRequest>(); // Create Serilog logger for the specific request type
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        // Log the request using Serilog
        _logger.Information("Handling {RequestName} with data {@Request}", typeof(TRequest).Name, request);

        var response = await next();

        // Log the response using Serilog
        _logger.Information("Handled {RequestName} with response {@Response}", typeof(TRequest).Name, response);

        return response;
    }

    
}
