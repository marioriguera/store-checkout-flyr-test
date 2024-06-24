namespace StoreCheckout.Domain.Entities
{
    /// <summary>
    /// Represents a product with a code, name and price.
    /// </summary>
    public sealed class Product
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Product"/> class.
        /// </summary>
        /// <param name="code">The code of the product.</param>
        /// <param name="name">The name of the product.</param>
        /// <param name="price">The price of the product.</param>
        public Product(string code, string name, decimal price)
        {
            Code = code ?? throw new ArgumentNullException(nameof(code));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Price = price;
        }

        /// <summary>
        /// Gets or sets product code.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets product name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets product price.
        /// </summary>
        public decimal Price { get; set; }
    }
}
