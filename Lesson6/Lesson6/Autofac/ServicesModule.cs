using Autofac;
using Lesson6.Services;
using Lesson6.Services.Impl;

namespace Lesson6.Autofac
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<OrderService>()
            .As<IOrderService>()
            .InstancePerLifetimeScope();
        }
    }
}
