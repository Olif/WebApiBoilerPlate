using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Api.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        private IUnitOfWork _uow;

        public UserController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [Route("")]
        public IHttpActionResult Get()
        {
            var users = _uow.UserAccountRepository.Find();
            return Ok(users);
        }
    }
}
