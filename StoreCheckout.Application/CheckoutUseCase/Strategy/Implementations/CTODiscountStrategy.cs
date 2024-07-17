using StoreCheckout.Application.CheckoutUseCase.Configurations;
using StoreCheckout.Application.CheckoutUseCase.DTOs;
using StoreCheckout.Application.CheckoutUseCase.Strategy.Abstractions;
using StoreCheckout.Application.CheckoutUseCase.Strategy.Common;
using StoreCheckout.Application.CheckoutUseCase.Strategy.Contrats;
using StoreCheckout.Domain.Entities;

namespace StoreCheckout.Application.CheckoutUseCase.Strategy.Implementations
{
    /// <summary>
    /// Represents the CTO discount strategy, which applies a discount to coffee products.
    /// </summary>
    internal sealed class CTODiscountStrategy : DiscountByAmountBase, IStrategy
    {
        private const int AMOUNT_DEFAULT = 2;
        private const decimal DISCOUNT_DEFAULT = 0.5m;

        /// <summary>
        /// Initializes a new instance of the <see cref="CTODiscountStrategy"/> class with the specified configurations.
        /// </summary>
        /// <param name="strategiesConfigurations">The configuration settings for strategies.</param>
        public CTODiscountStrategy(StrategiesConfigurations strategiesConfigurations)
            : base(
                  strategiesConfigurations,
                  DiscountCodes.CTODiscount,
                  new DefaultStrategyConfiguration(AMOUNT_DEFAULT, DISCOUNT_DEFAULT, Configurations.ProductsCode.Coffee))
        {
        }

        /// <inheritdoc/>
        public string DiscountCode => DiscountCodes.CTODiscount;

        /// <inheritdoc/>
        public IEnumerable<string> ProductsCode => ProductsCodeToAmount;

        /// <summary>
        /// Executes the discount strategy for coffee products.
        /// </summary>
        /// <param name="products">The list of products to apply the strategy to.</param>
        /// <returns>The total price after applying the discount strategy.</returns>
        public decimal Execute(List<Product> products)
        {
            return CalculateTotalByDiscountForAmount.Do(
                                                        products.Count,
                                                        AmountOfProductsToApplyStrategy,
                                                        products.First().Price,
                                                        DiscountPrice * products.First().Price);
        }
    }
}