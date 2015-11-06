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
namespace Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            var builder = new ContainerBuilder();
            builder.RegisterType<EFUnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<SystemContext>().AsSelf();
            builder.RegisterType<EFUserAccountRepository>().As<IUserAccountRepository>();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            
            WebApiConfig.Register(config);

            app.UseAutofacMiddleware(container);

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