using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Api.Controllers
{
    [RoutePrefix("api/forecasts")]
    public class ForecastController : ApiController
    {
        private readonly ILogger _logger;
        
        public ForecastController(ILogger logger)
        {
            _logger = logger;
        }

        [Route("")]
        public IHttpActionResult Get()
        {
            _logger.Debug("Testing");
            return Ok("forecast");
        }
    }
}
