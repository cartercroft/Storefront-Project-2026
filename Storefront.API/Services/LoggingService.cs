
namespace Storefront.API.Services
{
    public class LoggingService : ILogger
    {
        private readonly Serilog.ILogger _logger;
        public LoggingService(Serilog.ILogger logger)
        {
            _logger = logger;
        }
        public IDisposable? BeginScope<TState>(TState state) where TState : notnull => default!;

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel >= LogLevel.Information;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            switch (logLevel)
            {
                case LogLevel.Warning:
                    _logger.Warning(exception, "");
                break;
                case LogLevel.Error:
                case LogLevel.Critical:
                    _logger.Error(exception, "");
                    break;
                default:
                    _logger.Information(exception, "");
                    break;
            }
        }
    }
}
