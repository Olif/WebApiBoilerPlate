using Autofac;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Api.AppConfig
{
    public class DIConfig
    {
        public static ContainerBuilder BuildDependencies()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<EFUnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<SystemContext>().AsSelf();
            builder.RegisterType<EFUserAccountRepository>().As<IUserAccountRepository>();

            return builder;
        }
    }
}