using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);

            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions() { 
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(24),
                AllowInsecureHttp = true,
                Provider = new SimpleAuthorizationServerProvider(),
                TokenEndpointPath = new PathString("/api/token")
            });

            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions() { });

            app.UseWebApi(config);
        }
    }
}