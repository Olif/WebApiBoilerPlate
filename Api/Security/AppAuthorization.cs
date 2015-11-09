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
            var resource = context.Resource.First().Value;

            if(resource == "User")
            {
                return await CheckUserAccessAsync(context);
            
            }

            return await Nok();
        }

        private async Task<bool> CheckUserAccessAsync(ResourceAuthorizationContext context)
        {
            if (!context.Principal.Identity.IsAuthenticated)
            {
                return await Nok();
            }

            if (context.Action.Any(x => x.Value == "Edit"))
            {
                return await CheckUserEditAccessByRoleAsync(context);
            }

            return await Ok();
        }

        private async Task<bool> CheckUserEditAccessByRoleAsync(ResourceAuthorizationContext context)
        {
            var user = _uow.UserAccountRepository.Get(context.Principal.Identity.Name);
            if (user.Claims.Any(x => x.Type == System.Security.Claims.ClaimTypes.Role &&
                x.Value == "SystemAdmin"))
            {
                return await Ok();
            }

            return await Nok();
        }
    }
}