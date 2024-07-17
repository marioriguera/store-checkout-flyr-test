using Microsoft.Extensions.DependencyInjection;
using StoreCheckout.Application;
using StoreCheckout.Application.CheckoutUseCase;
using StoreCheckout.Application.CheckoutUseCase.Configurations;
using StoreCheckout.Application.CheckoutUseCase.DTOs;

namespace StoreCheckout.Tests
{
    /// <summary>
    /// Test class to verify expected behaviors of the checkout system.
    /// </summary>
    public class CheckoutExpectedBehaviorsTests
    {
        private readonly ServiceProvider _serviceProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckoutExpectedBehaviorsTests"/> class.
        /// </summary>
        public CheckoutExpectedBehaviorsTests()
        {
            // Create services collection.
            var serviceCollection = new ServiceCollection();

            // Add services from application layer.
            serviceCollection.AddApplication();

            // Get service provider.
            _serviceProvider = serviceCollection.BuildServiceProvider();

            // Change configuration file
            var configs = _serviceProvider.GetService<StrategiesConfigurations>();
            configs!.JsonConfgPath = Path.Combine(Directory.GetCurrentDirectory(), "Configs\\expectedbehaviors.tests.json");
        }

        /// <summary>
        /// Verifies the total cost when items with normal behavior are scanned.
        /// </summary>
        [Fact]
        public void Calculate_Total_For_Normal_Items()
        {
            ProductDTO greenTea = new(ProductsCode.GreenTea, "Green Tea", 3.00m);
            ProductDTO strawberry = new(ProductsCode.Strawberries, "Strawberry", 5.00m);
            ProductDTO coffee = new(ProductsCode.Coffee, "Coffee", 10.00m);

            Checkout checkout = _serviceProvider.GetService<Checkout>()
                ?? throw new Exception($"Failed to resolve {nameof(Checkout)} dependency.");

            checkout.Scan(greenTea);
            checkout.Scan(strawberry);
            checkout.Scan(strawberry);
            checkout.Scan(strawberry);
            checkout.Scan(coffee);
            checkout.Scan(coffee);
            checkout.Scan(coffee);

            Assert.Equal(45.00m, checkout.Total());
        }

        /// <summary>
        /// Verifies that buying one green tea follows the two-for-one rule and costs the value of one green tea.
        /// </summary>
        [Fact]
        public void Buy_One_Green_Tea()
        {
            ProductDTO greenTea = new(ProductsCode.GreenTea, "Green Tea", 4.00m);

            Checkout checkout = _serviceProvider.GetService<Checkout>()
                ?? throw new Exception($"Failed to resolve {nameof(Checkout)} dependency.");

            checkout.Scan(greenTea);

            Assert.Equal(4.00m, checkout.Total());
        }

        /// <summary>
        /// Verifies that buying two green teas follows the two-for-one rule and costs the value of one green tea.
        /// </summary>
        [Fact]
        public void Buy_Two_Green_Teas()
        {
            ProductDTO greenTea = new(ProductsCode.GreenTea, "Green Tea", 4.00m);

            Checkout checkout = _serviceProvider.GetService<Checkout>()
                ?? throw new Exception($"Failed to resolve {nameof(Checkout)} dependency.");

            checkout.Scan(greenTea);
            checkout.Scan(greenTea);

            Assert.Equal(4.00m, checkout.Total());
        }

        /// <summary>
        /// Verifies that buying three green teas follows the two-for-one rule and costs the value of two green teas.
        /// </summary>
        [Fact]
        public void Buy_Three_Green_Teas()
        {
            ProductDTO greenTea = new(ProductsCode.GreenTea, "Green Tea", 4.00m);

            Checkout checkout = _serviceProvider.GetService<Checkout>()
                ?? throw new Exception($"Failed to resolve {nameof(Checkout)} dependency.");

            checkout.Scan(greenTea);
            checkout.Scan(greenTea);
            checkout.Scan(greenTea);

            Assert.Equal(8.00m, checkout.Total());
        }

        /// <summary>
        /// Verifies that buying four green teas follows the two-for-one rule and costs the value of two green teas.
        /// </summary>
        [Fact]
        public void Buy_Four_Green_Teas()
        {
            ProductDTO greenTea = new(ProductsCode.GreenTea, "Green Tea", 4.00m);

            Checkout checkout = _serviceProvider.GetService<Checkout>()
                ?? throw new Exception($"Failed to resolve {nameof(Checkout)} dependency.");

            checkout.Scan(greenTea);
            checkout.Scan(greenTea);
            checkout.Scan(greenTea);
            checkout.Scan(greenTea);

            Assert.Equal(8.00m, checkout.Total());
        }

        /// <summary>
        /// Buys two packages of strawberries and expects no quantity discount to apply.
        /// </summary>
        [Fact]
        public void Buy_Two_Strawberries_Packages()
        {
            ProductDTO strawberries = new(ProductsCode.Strawberries, "Strawberries", 5.00m);

            Checkout checkout = _serviceProvider.GetService<Checkout>()
                ?? throw new Exception($"Failed to resolve {nameof(Checkout)} dependency.");

            checkout.Scan(strawberries);
            checkout.Scan(strawberries);

            Assert.Equal(10.00m, checkout.Total());
        }

        /// <summary>
        /// Buys three packages of strawberries and expects a quantity discount to apply.
        /// </summary>
        [Fact]
        public void Buy_Three_Strawberries_Packages()
        {
            ProductDTO strawberries = new(ProductsCode.Strawberries, "Strawberries", 5.00m);

            Checkout checkout = _serviceProvider.GetService<Checkout>()
                ?? throw new Exception($"Failed to resolve {nameof(Checkout)} dependency.");

            checkout.Scan(strawberries);
            checkout.Scan(strawberries);
            checkout.Scan(strawberries);

            Assert.Equal(12.00m, checkout.Total());
        }

        /// <summary>
        /// Buys four packages of strawberries and expects a quantity discount to apply.
        /// </summary>
        [Fact]
        public void Buy_Four_Strawberries_Packages()
        {
            ProductDTO strawberries = new(ProductsCode.Strawberries, "Strawberries", 5.00m);

            Checkout checkout = _serviceProvider.GetService<Checkout>()
                ?? throw new Exception($"Failed to resolve {nameof(Checkout)} dependency.");

            checkout.Scan(strawberries);
            checkout.Scan(strawberries);
            checkout.Scan(strawberries);
            checkout.Scan(strawberries);

            Assert.Equal(16.00m, checkout.Total());
        }

        /// <summary>
        /// Buys four coffees and expects no quantity discount to apply.
        /// </summary>
        [Fact]
        public void Buy_Four_Coffees()
        {
            ProductDTO coffee = new(ProductsCode.Coffee, "Coffee", 10.00m);

            Checkout checkout = _serviceProvider.GetService<Checkout>()
                ?? throw new Exception($"Failed to resolve {nameof(Checkout)} dependency.");

            checkout.Scan(coffee);
            checkout.Scan(coffee);
            checkout.Scan(coffee);
            checkout.Scan(coffee);

            Assert.Equal(40.00m, checkout.Total());
        }

        /// <summary>
        /// Buys five coffees and expects a quantity discount to apply.
        /// </summary>
        [Fact]
        public void Buy_Five_Coffees()
        {
            ProductDTO coffee = new(ProductsCode.Coffee, "Coffee", 10.00m);

            Checkout checkout = _serviceProvider.GetService<Checkout>()
                ?? throw new Exception($"Failed to resolve {nameof(Checkout)} dependency.");

            checkout.Scan(coffee);
            checkout.Scan(coffee);
            checkout.Scan(coffee);
            checkout.Scan(coffee);
            checkout.Scan(coffee);

            Assert.Equal(25.00m, checkout.Total());
        }

        /// <summary>
        /// Buys five coffees and five pop corns, expects a quantity discount to apply.
        /// </summary>
        [Fact]
        public void Buy_Five_Coffees_And_Five_Pop_Corn()
        {
            ProductDTO coffee = new(ProductsCode.Coffee, "Coffee", 10.00m);
            ProductDTO popCorn = new(ProductsCode.PopCorn, "Pop Corn", 10.00m);

            Checkout checkout = _serviceProvider.GetService<Checkout>()
                ?? throw new Exception($"Failed to resolve {nameof(Checkout)} dependency.");

            checkout.Scan(coffee);
            checkout.Scan(coffee);
            checkout.Scan(coffee);
            checkout.Scan(coffee);
            checkout.Scan(coffee);

            checkout.Scan(popCorn);
            checkout.Scan(popCorn);
            checkout.Scan(popCorn);
            checkout.Scan(popCorn);
            checkout.Scan(popCorn);

            Assert.Equal(50.00m, checkout.Total());
        }

        /// <summary>
        /// Buys six coffees and expects a quantity discount to apply.
        /// </summary>
        [Fact]
        public void Buy_Six_Coffees()
        {
            ProductDTO coffee = new(ProductsCode.Coffee, "Coffee", 10.00m);

            Checkout checkout = _serviceProvider.GetService<Checkout>()
                ?? throw new Exception($"Failed to resolve {nameof(Checkout)} dependency.");

            checkout.Scan(coffee);
            checkout.Scan(coffee);
            checkout.Scan(coffee);
            checkout.Scan(coffee);
            checkout.Scan(coffee);
            checkout.Scan(coffee);

            Assert.Equal(30.00m, checkout.Total());
        }

        /// <summary>
        /// Checks the total cost with products without discounts.
        /// </summary>
        [Fact]
        public void Calculate_Total_For_Products_Without_Discount()
        {
            ProductDTO potatoes = new(ProductsCode.Potatoes, "Potatoes", 2.00m);
            ProductDTO beer = new(ProductsCode.Beer, "Beer", 3.00m);

            Checkout checkout = _serviceProvider.GetService<Checkout>()
                ?? throw new Exception($"Failed to resolve {nameof(Checkout)} dependency.");

            checkout.Scan(potatoes);
            checkout.Scan(potatoes);
            checkout.Scan(beer);

            Assert.Equal(7.00m, checkout.Total());
        }

        /// <summary>
        /// Verifies the cost with products outside the <see cref="ProductsCode"/> class.
        /// </summary>
        [Fact]
        public void Playing_normal_behavior_with_products_Without_ProdductsCode_TEST()
        {
            ProductDTO pasta = new("PC9", "Pasta Carbonara", 2.00m);
            ProductDTO eggs = new("EG1", "Eggs", 2.00m);

            Checkout checkout = _serviceProvider.GetService<Checkout>()
                ?? throw new Exception($"Cant resolve {nameof(Checkout)} dependency.");

            checkout.Scan(pasta);
            checkout.Scan(eggs);

            Assert.Equal(4.00m, checkout.Total());
        }

        /// <summary>
        /// Check the cost by adding and removing products from the checkout.
        /// </summary>
        [Fact]
        public void Playing_normal_behavior_deleting_products_TEST()
        {
            ProductDTO greenTea = new(ProductsCode.GreenTea, "Green Tea", 3.00m);
            ProductDTO strawberry = new(ProductsCode.Strawberries, "Strawberry", 5.00m);
            ProductDTO coffee = new(ProductsCode.Coffee, "Coffee", 10.00m);

            Checkout checkout = _serviceProvider.GetService<Checkout>()
                ?? throw new Exception($"Cant resolve {nameof(Checkout)} dependency.");

            checkout.Scan(greenTea);
            checkout.Scan(strawberry);
            checkout.Scan(strawberry);
            checkout.Scan(strawberry);
            checkout.Scan(coffee);
            checkout.Scan(coffee);
            checkout.Scan(coffee);

            checkout.Delete(coffee);
            checkout.Delete(strawberry);

            Assert.Equal(33.00m, checkout.Total());
        }

        /// <summary>
        /// Calculate the total to pay when no product has been registered in the purchase box.
        /// The total must be zero.
        /// </summary>
        [Fact]
        public void Total_of_a_purchase_without_products_TEST()
        {
            Checkout checkout = _serviceProvider.GetService<Checkout>()
                ?? throw new Exception($"Cant resolve {nameof(Checkout)} dependency.");

            Assert.Equal(0.00m, checkout.Total());
        }

        /// <summary>
        /// Trying to remove a product from the shopping cart without it existing. It shouldn't cause problems.
        /// </summary>
        [Fact]
        public void Remove_a_product_from_checkout_without_existing_TEST()
        {
            ProductDTO greenTea = new(ProductsCode.GreenTea, "Green Tea", 3.00m);
            ProductDTO strawberry = new(ProductsCode.Strawberries, "Strawberry", 5.00m);
            ProductDTO coffee = new(ProductsCode.Coffee, "Coffee", 10.00m);

            Checkout checkout = _serviceProvider.GetService<Checkout>()
                ?? throw new Exception($"Cant resolve {nameof(Checkout)} dependency.");

            checkout.Scan(strawberry);
            checkout.Scan(strawberry);
            checkout.Scan(coffee);
            checkout.Scan(coffee);

            checkout.Delete(greenTea);

            Assert.Equal(30.00m, checkout.Total());
        }
    }
}