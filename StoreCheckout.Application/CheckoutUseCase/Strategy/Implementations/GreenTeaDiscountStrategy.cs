using StoreCheckout.Application.CheckoutUseCase.Configurations;
using StoreCheckout.Application.CheckoutUseCase.Strategy.Contrats;
using StoreCheckout.Domain.Entities;

namespace StoreCheckout.Application.CheckoutUseCase.Strategy.Implementations
{
    /// <summary>
    /// Represents a strategy for applying discounts to green tea products based on configured rules.
    /// </summary>
    internal sealed class GreenTeaDiscountStrategy : IStrategy
    {
        private readonly int _amountOfProductsToApplyStrategy;

        /// <summary>
        /// Initializes a new instance of the <see cref="GreenTeaDiscountStrategy"/> class.
        /// </summary>
        /// <param name="strategiesConfigurations">The configuration object containing discount rules.</param>
        public GreenTeaDiscountStrategy(StrategiesConfigurations strategiesConfigurations)
        {
            int amountDefault = 2;

            if (strategiesConfigurations.ConfigurationsRules.TryGetValue(ProductsCode.GreenTea, out ProductDiscountRuleConfiguration? config))
            {
                _amountOfProductsToApplyStrategy = config.Amount ?? amountDefault;
            }
            else
            {
                _amountOfProductsToApplyStrategy = amountDefault;
            }
        }

        /// <inheritdoc/>
        public string ProductCode => ProductsCode.GreenTea;

        /// <summary>
        /// Executes the discount strategy for green tea products.
        /// </summary>
        /// <param name="products">The list of products to apply the strategy to.</param>
        /// <returns>The total price after applying the discount strategy.</returns>
        public decimal Execute(List<Product> products)
        {
            int timesToApply = products.Count / _amountOfProductsToApplyStrategy;

            if (products.Count % _amountOfProductsToApplyStrategy != 0)
                timesToApply++;

            return timesToApply * products.First().Price;
        }
    }
}
