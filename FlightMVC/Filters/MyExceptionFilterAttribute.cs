using Microsoft.AspNetCore.Mvc.Filters;

namespace FlightMVC.Filters
{
    public class MyExceptionFilterAttribute : ExceptionFilterAttribute
    {
        ILogger _logger;

        public MyExceptionFilterAttribute(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<MyExceptionFilterAttribute>();
        }

        public override void OnException(ExceptionContext context)
        {
            _logger.LogError($"Uh oh: {context.Exception.Message}");

            base.OnException(context);
        }
    }
}
