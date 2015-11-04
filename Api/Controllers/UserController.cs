using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Api.Controllers
{
    [Authorize]
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        public UserController()
        {

        }

        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok(base.User.Identity.Name);
        }
    }
}
