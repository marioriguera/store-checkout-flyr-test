using StoreCheckout.Application.CheckoutUseCase.Configurations;
using StoreCheckout.Application.CheckoutUseCase.DTOs;
using StoreCheckout.Application.CheckoutUseCase.Strategy.Abstractions;
using StoreCheckout.Application.CheckoutUseCase.Strategy.Contrats;
using StoreCheckout.Domain.Entities;

namespace StoreCheckout.Application.CheckoutUseCase.Strategy.Implementations
{
    /// <summary>
    /// Represents a strategy for calculating the total price without applying any discounts.
    /// </summary>
    internal sealed class ProductWithoutDiscountStrategy : DiscountByAmountBase, IStrategy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductWithoutDiscountStrategy"/> class.
        /// </summary>
        public ProductWithoutDiscountStrategy()
            : base(
                  null,
                  DiscountCodes.Default,
                  new DefaultStrategyConfiguration(0, 0, string.Empty))
        {
        }

        /// <inheritdoc/>
        public string DiscountCode => DiscountCodes.Default;

        /// <inheritdoc/>
        public IEnumerable<string> ProductsCode => ProductsCodeToAmount;

        /// <summary>
        /// Executes the strategy to calculate the total price of products without applying discounts.
        /// </summary>
        /// <param name="products">The list of products for which to calculate the total price.</param>
        /// <returns>The total price of all products without any discounts applied.</returns>
        public decimal Execute(List<Product> products)
        {
            return products.Count * products.First().Price;
        }
    }
}
