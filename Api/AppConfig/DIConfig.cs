using Autofac;
using Autofac.Integration.WebApi;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace Api.AppConfig
{
    public class DIConfig
    {
        public static IContainer BuildDIContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<EFUnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<SystemContext>().AsSelf();
            builder.RegisterType<EFUserAccountRepository>().As<IUserAccountRepository>();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            
            var container = builder.Build();

            return container;
        }
    }
}