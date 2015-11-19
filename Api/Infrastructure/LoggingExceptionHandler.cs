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
    public class NLogExceptionLogger : ExceptionLogger
    {
        private ILogger _logger = LogManager.GetLogger("ExceptionLogger");

        public override void Log(ExceptionLoggerContext context)
        {
            _logger.Error(context.Exception);
        }
    }
}