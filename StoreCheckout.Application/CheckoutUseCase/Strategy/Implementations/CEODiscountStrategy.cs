using StoreCheckout.Application.CheckoutUseCase.Configurations;
using StoreCheckout.Application.CheckoutUseCase.DTOs;
using StoreCheckout.Application.CheckoutUseCase.Strategy.Abstractions;
using StoreCheckout.Application.CheckoutUseCase.Strategy.Contrats;
using StoreCheckout.Domain.Entities;

namespace StoreCheckout.Application.CheckoutUseCase.Strategy.Implementations
{
    /// <summary>
    /// Represents a strategy for applying discounts to green tea products based on configured rules.
    /// CEO discount.
    /// </summary>
    internal sealed class CEODiscountStrategy : DiscountByAmountBase, IStrategy
    {
        private const int AMOUNT_DEFAULT = 2;
        private const decimal DISCOUNT_DEFAULT = 0.5m;

        /// <summary>
        /// Initializes a new instance of the <see cref="CEODiscountStrategy"/> class with the specified configurations.
        /// </summary>
        /// <param name="strategiesConfigurations">The configuration settings for strategies.</param>
        public CEODiscountStrategy(StrategiesConfigurations strategiesConfigurations)
            : base(
                  strategiesConfigurations,
                  DiscountCodes.CEODiscount,
                  new DefaultStrategyConfiguration(AMOUNT_DEFAULT, DISCOUNT_DEFAULT, Configurations.ProductsCode.GreenTea))
        {
        }

        /// <inheritdoc/>
        public string DiscountCode => DiscountCodes.CEODiscount;

        /// <inheritdoc/>
        public IEnumerable<string> ProductsCode => ProductsCodeToAmount;

        /// <summary>
        /// Executes the discount strategy for green tea products.
        /// </summary>
        /// <param name="products">The list of products to apply the strategy to.</param>
        /// <returns>The total price after applying the discount strategy.</returns>
        public decimal Execute(List<Product> products)
        {
            int timesToApply = products.Count / AmountOfProductsToApplyStrategy;

            if (products.Count % AmountOfProductsToApplyStrategy != 0)
                timesToApply++;

            return timesToApply * products.First().Price;
        }
    }
}
