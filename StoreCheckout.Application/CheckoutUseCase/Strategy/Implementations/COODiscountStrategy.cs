using StoreCheckout.Application.CheckoutUseCase.Configurations;
using StoreCheckout.Application.CheckoutUseCase.DTOs;
using StoreCheckout.Application.CheckoutUseCase.Strategy.Abstractions;
using StoreCheckout.Application.CheckoutUseCase.Strategy.Common;
using StoreCheckout.Application.CheckoutUseCase.Strategy.Contrats;
using StoreCheckout.Domain.Entities;

namespace StoreCheckout.Application.CheckoutUseCase.Strategy.Implementations
{
    /// <summary>
    /// Represents a strategy for applying discounts to strawberries products based on configured rules.
    /// COO discount.
    /// </summary>
    internal sealed class COODiscountStrategy : DiscountByAmountBase, IStrategy
    {
        private const int AMOUNT_DEFAULT = 3;
        private const decimal DISCOUNT_DEFAULT = 4.50m;

        /// <summary>
        /// Initializes a new instance of the <see cref="COODiscountStrategy"/> class with the specified configurations.
        /// </summary>
        /// <param name="strategiesConfigurations">The configuration settings for strategies.</param>
        public COODiscountStrategy(StrategiesConfigurations strategiesConfigurations)
            : base(
                  strategiesConfigurations,
                  DiscountCodes.COODiscount,
                  new DefaultStrategyConfiguration(AMOUNT_DEFAULT, DISCOUNT_DEFAULT, Configurations.ProductsCode.Strawberries))
        {
        }

        /// <inheritdoc/>
        public string DiscountCode => DiscountCodes.COODiscount;

        /// <inheritdoc/>
        public IEnumerable<string> ProductsCode => ProductsCodeToAmount;

        /// <summary>
        /// Executes the discount strategy for strawberries products.
        /// </summary>
        /// <param name="products">The list of products to apply the strategy to.</param>
        /// <returns>The total price after applying the discount strategy.</returns>
        public decimal Execute(List<Product> products)
        {
            return CalculateTotalByDiscountForAmount.Do(
                                                        products.Count,
                                                        AmountOfProductsToApplyStrategy,
                                                        products.First().Price,
                                                        DiscountPrice);
        }
    }
}
