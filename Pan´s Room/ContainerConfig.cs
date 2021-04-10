using Autofac;
using Logger;
using Models;
using Repository;
using Repository.Context;
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

            //Services
            builder.RegisterType<CollectionServices>()
                .As<IDiscServices<Collection>>();
            builder.RegisterType<WishListServices>()
                .As<IDiscServices<WishList>>();

            //Repository
            builder.RegisterGeneric(typeof(EntityRepository<>))
                .As(typeof(IEntityRepository<>))
                .InstancePerDependency();

            //Logger
            builder.RegisterType<Logger.Logger>()
                .As<ILogger>();

            //DbContext
            builder.RegisterType<PansRoomContext>().AsSelf();

            return builder.Build();
        }
    }
}
