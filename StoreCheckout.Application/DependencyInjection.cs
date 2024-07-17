using Microsoft.Extensions.DependencyInjection;
using StoreCheckout.Application.CheckoutUseCase;
using StoreCheckout.Application.CheckoutUseCase.Configurations;
using StoreCheckout.Application.CheckoutUseCase.Strategy;
using StoreCheckout.Application.CheckoutUseCase.Strategy.Contrats;
using StoreCheckout.Application.CheckoutUseCase.Strategy.Implementations;
using StoreCheckout.Application.CheckoutUseCase.Utils;

namespace StoreCheckout.Application
{
    /// <summary>
    /// Provides extension methods for adding application services to the IServiceCollection.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Adds application services to the specified IServiceCollection.
        /// </summary>
        /// <param name="services">The IServiceCollection to add services to.</param>
        /// <returns>The IServiceCollection with application services added.</returns>
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<Checkout>();
            services.AddSingleton<StrategiesConfigurations>();

            services.AddAutoMapper(typeof(MappingProfile));

            services.AddScoped<IStrategy, CEODiscountStrategy>();
            services.AddScoped<IStrategy, COODiscountStrategy>();
            services.AddScoped<IStrategy, CTODiscountStrategy>();
            services.AddScoped<IStrategy, ProductWithoutDiscountStrategy>();
            services.AddScoped<IStrategyContext, StrategyContext>();

            return services;
        }
    }
}
