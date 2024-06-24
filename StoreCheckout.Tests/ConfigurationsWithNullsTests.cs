using Microsoft.Extensions.DependencyInjection;
using StoreCheckout.Application;
using StoreCheckout.Application.CheckoutUseCase;
using StoreCheckout.Application.CheckoutUseCase.Configurations;
using StoreCheckout.Application.CheckoutUseCase.DTOs;

namespace StoreCheckout.Tests
{
    /// <summary>
    /// Contains tests for configuration scenarios where the discount configuration parameters are null.
    /// Ensures that the default configuration is used in such cases.
    /// </summary>
    public class ConfigurationsWithNullsTests
    {
        private readonly ServiceProvider _serviceProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationsWithNullsTests"/> class.
        /// Sets up the service provider and configures the application services.
        /// </summary>
        public ConfigurationsWithNullsTests()
        {
            // Create services collection.
            var serviceCollection = new ServiceCollection();

            // Add services from application layer.
            serviceCollection.AddApplication();

            // Get service provider.
            _serviceProvider = serviceCollection.BuildServiceProvider();

            // Change configuration file
            var configs = _serviceProvider.GetService<StrategiesConfigurations>();
            configs!.JsonConfgPath = Path.Combine(Directory.GetCurrentDirectory(), "Configs\\configurationswithnulls.tests.json");
        }

        /// <summary>
        /// Tests the scenario where the discount configuration parameters are null.
        /// Ensures that the default configuration is used.
        /// </summary>
        [Fact]
        public void Buy_with_discount_configuration_parameters_nulls_TEST()
        {
            ProductDTO greenTea = new(ProductsCode.GreenTea, "Green Tea", 3.00m);
            ProductDTO strawberry = new(ProductsCode.Strawberries, "Strawberry", 5.00m);
            ProductDTO coffee = new(ProductsCode.Coffee, "Coffee", 10.00m);

            Checkout checkout = _serviceProvider.GetService<Checkout>()
                ?? throw new Exception($"Cannot resolve {nameof(Checkout)} dependency.");

            checkout.Scan(greenTea);
            checkout.Scan(strawberry);
            checkout.Scan(coffee);

            Assert.Equal(18.00m, checkout.Total());
        }
    }
}
