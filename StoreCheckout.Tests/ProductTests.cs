using StoreCheckout.Domain.Entities;

namespace StoreCheckout.Tests
{
    /// <summary>
    /// Contains tests for the Product class implementing the IItem interface.
    /// </summary>
    public class ProductTests
    {
        /// <summary>
        /// Tests the creation of a Product object and verifies its properties.
        /// </summary>
        [Fact]
        public void Creating_of_a_product_TEST()
        {
            string code = "SomeCode";
            string name = "Banana";
            decimal price = 1.00m;

            Product product = new Product(code, name, price);

            Assert.Equal(code, product.Code);
            Assert.Equal(name, product.Name);
            Assert.Equal(price, product.Price);
        }

        /// <summary>
        /// Tests creating a product with a null name, expecting an ArgumentNullException.
        /// </summary>
        [Fact]
        public void Product_with_null_name_TEST()
        {
            string code = "SomeCode";
            string? name = null;
            decimal price = 1.00m;

            Assert.Throws<ArgumentNullException>("name", () =>
            {
                var product = new Product(code, name!, price);
            });
        }

        /// <summary>
        /// Tests creating a product with a null code, expecting an ArgumentNullException.
        /// </summary>
        [Fact]
        public void Product_with_null_code_TEST()
        {
            string? code = null;
            string name = "Banana";
            decimal price = 1.00m;

            Assert.Throws<ArgumentNullException>("code", () =>
            {
                var product = new Product(code!, name, price);
            });
        }
    }
}
