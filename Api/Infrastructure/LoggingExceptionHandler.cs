using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.ExceptionHandling;

namespace Api.Infrastructure
{
    public class LoggingExceptionHandler : ExceptionHandler
    {
        private ILogger _logger = LogManager.GetLogger("ExceptionLogger");

        public override Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            _logger.Error(context.Exception);
            return base.HandleAsync(context, cancellationToken);
        }
    }
}