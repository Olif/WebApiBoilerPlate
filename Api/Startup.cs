using Autofac;
using DAL;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Autofac.Integration.WebApi;
using System.Reflection;
using Api.Security;
using Api.AppConfig;
using System.Web.Http.ExceptionHandling;
using Api.Infrastructure;

namespace Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            var container = DIConfig.BuildDIContainer();
            
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            RouteConfiguration.Register(config);
            config.Services.Replace(typeof(IExceptionHandler), new LoggingExceptionHandler());

            app.UseAutofacMiddleware(container);

            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions() { 
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(24),
                AllowInsecureHttp = true,
                Provider = new SimpleAuthorizationServerProvider(),
                TokenEndpointPath = new PathString("/api/token")
            });

            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions() { });

            app.UseResourceAuthorization(new AppAuthorization());   

            app.UseWebApi(config);
        }
    }
}