using MotorcycleRental.Api.RabbitMq;
using MotorcycleRental.Bussiness.Interfaces;
using MotorcycleRental.Bussiness.Notifications;
using MotorcycleRental.Bussiness.RabbitMq;
using MotorcycleRental.Bussiness.Services;
using MotorcycleRental.Data.Context;
using MotorcycleRental.Data.Repositories;

namespace MotorcycleRental.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services) 
        {
            services.AddScoped<ApiDbContext>();

            // Repositories
            services.AddScoped<IDeliveryManRepository, DeliveryManRepository>();
            services.AddScoped<IMotorcycleRepository, MotorcycleRepository>();
            services.AddScoped<IRentalRepository, RentalRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderNotificationRepository, OrderNotificationRepository>();
            services.AddScoped<INotifier, Notifier>();

            // Services
            services.AddScoped<IMotorcycleService, MotorcycleService>();
            services.AddScoped<IDeliveryManService, DeliveryManService>();
            services.AddScoped<IRentalService, RentalService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IRabbitMqConfig, RabbiMqConfig>();

            services.AddHostedService<RabbitMqConsumer>();

            return services;
        }            
    }
}
