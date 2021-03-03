using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Logger;
using Repository;
using Services;

namespace Pan_s_Room
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Application>()
                .As<IApplication>();

            builder.RegisterType<DiscServices>()
                .As<IDiscServices>();
            builder.RegisterType<WishListServices>()
                .As<IWishListServices>();

            builder.RegisterGeneric(typeof(CollectionRepository<>))
                .As(typeof(ICollectionRepository<>))
                .InstancePerDependency();

            builder.RegisterType<Logger.Logger>()
                .As<ILogger>();


            return builder.Build();
        }
    }
}
