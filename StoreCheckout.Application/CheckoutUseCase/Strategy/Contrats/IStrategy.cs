using StoreCheckout.Domain.Entities;

namespace StoreCheckout.Application.CheckoutUseCase.Strategy.Contrats
{
    /// <summary>
    /// Represents a strategy for applying discounts to products.
    /// </summary>
    public interface IStrategy
    {
        /// <summary>
        /// Gets the discount code associated with the strategy.
        /// </summary>
        string DiscountCode { get; }

        /// <summary>
        /// Gets the collection of product codes that the strategy applies to.
        /// </summary>
        IEnumerable<string>? ProductsCode { get; }

        /// <summary>
        /// Executes the strategy on a list of products and returns the total discount.
        /// </summary>
        /// <param name="products">The list of products to apply the strategy to.</param>
        /// <returns>The total discount amount.</returns>
        decimal Execute(List<Product> products);
    }
}
