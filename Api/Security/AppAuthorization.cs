using DAL;
using System;
using System.Collections.Generic;
using System.IdentityModel.Claims;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Thinktecture.IdentityModel.Owin.ResourceAuthorization;

namespace Api.Security
{
    public class AppAuthorization : ResourceAuthorizationManager
    {
        private readonly IUnitOfWork _uow;

        public AppAuthorization()
        {
            _uow = new EFUnitOfWork(new SystemContext());
        }

        public override async Task<bool> CheckAccessAsync(ResourceAuthorizationContext context)
        {
            var userAccount = _uow.UserAccountRepository.Get(context.Principal.Identity.Name);

            var resource = context.Resource.First().Value;

            if(resource == "User")
            {
                return userAccount.Claims.Any(x => x.Type == System.Security.Claims.ClaimTypes.Role &&
                    x.Value == "SysAdmin");
            
            }

            return await Nok();
        }
    }
}