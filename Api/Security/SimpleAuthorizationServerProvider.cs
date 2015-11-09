using DAL;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Api.Security
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private readonly IUnitOfWork _uow;

        public SimpleAuthorizationServerProvider()
        {
            _uow = new EFUnitOfWork(new SystemContext());
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Validate all clients
            await Task.FromResult(context.Validated());
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            // Try get the useraccount by provided username
            var userAccount = _uow.UserAccountRepository.Get(context.UserName);

            // If the useraccount was not found, reject the token request
            if (userAccount == null)
            {
                context.Rejected();
                return;
            }

            // If password is invalid, reject the token request
            if (!PasswordHelper.Verify(userAccount.Password, userAccount.Salt, context.Password))
            {
                context.Rejected();
                return;
            }

            // Create identity which will be included in the token
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);

            // All claims added here will be written to the token. Thus claims should
            // be added with moderation
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            identity.AddClaim(new Claim(ClaimTypes.Role, "administrator"));
            
            // Validate the reqeust and return a token 
            context.Validated(identity);
        }
    }
}
