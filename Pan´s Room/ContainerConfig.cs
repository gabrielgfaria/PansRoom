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

            builder.RegisterGeneric(typeof(CollectionRepository<>))
                .As(typeof(ICollectionRepository<>))
                .InstancePerDependency();

            builder.RegisterGeneric(typeof(CollectionServices<>))
                .As(typeof(ICollectionServices<>))
                .InstancePerDependency();

            return builder.Build();
        }
    }
}
