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
using System.Web.Http.ExceptionHandling;
using Api.Infrastructure;
using NLog;
using Autofac.Core;

namespace Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            app.SetupFilters(config);
            app.SetupRoutes(config);
            app.SetupDependencies(config);

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

    public static class SetupDependenciesExtensions
    {
        public static void SetupDependencies(this IAppBuilder app, HttpConfiguration config)
        { 
            var builder = new ContainerBuilder();
            builder.RegisterType<EFUnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<SystemContext>().AsSelf();
            builder.RegisterType<EFUserAccountRepository>().As<IUserAccountRepository>();
            builder.Register(c => LogManager.GetLogger("ApiControllerLogger")).As<ILogger>();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            var container = builder.Build(); 
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            app.UseAutofacMiddleware(container);
        }
    }

    /*public class LoggingModule : Autofac.Module
    {
        protected override void AttachToComponentRegistration(
            IComponentRegistry componentRegistry, IComponentRegistration registration)
        {
            registration.Preparing += (sender, e) =>
            {
                Type limitType = e.Component.Activator.LimitType;
                e.Parameters = e.Parameters.Union(new[] {
                new ResolvedParameter((pi, c) => pi.ParameterType == typeof(ILogger), 
                                      (pi, c) => c.Resolve<ILogger>(new NamedParameter("name", limitType.Name))),
            });
            };
        }
    }*/

    public static class SetupFilterExtensions
    {
        public static void SetupFilters(this IAppBuilder app, HttpConfiguration config)
        { 
            config.Services.Add(typeof(IExceptionLogger), new NLogExceptionLogger());
        }
    }

    public static class SetupRoutesExtensions
    {
        public static void SetupRoutes(this IAppBuilder app, HttpConfiguration config)
        {
            // Use attribute routing
            config.MapHttpAttributeRoutes();

            // Default route
            config.Routes.MapHttpRoute(
                name: "AllCustomers",
                routeTemplate: "customers",
                defaults: new { controller = "Customers", action = "Get" }
                );
        }
    }
}