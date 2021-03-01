using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
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

            builder.RegisterType<DiscRepository>()
                .As<IDiscRepository>();
            builder.RegisterType<WishListRepository>()
                .As<IWishListRepository>();

            builder.RegisterGeneric(typeof(CollectionServices<>))
                .As(typeof(ICollectionServices<>))
                .InstancePerDependency();

            return builder.Build();
        }
    }
}
