using StoreCheckout.Application.CheckoutUseCase.Configurations;
using StoreCheckout.Application.CheckoutUseCase.Strategy.Common;
using StoreCheckout.Application.CheckoutUseCase.Strategy.Contrats;
using StoreCheckout.Domain.Entities;

namespace StoreCheckout.Application.CheckoutUseCase.Strategy.Implementations
{
    /// <summary>
    /// Represents a strategy for applying discounts to coffee products based on configured rules.
    /// </summary>
    internal sealed class CoffeeDiscountStrategy : IStrategy
    {
        private readonly int _maxCountToDiscount;
        private readonly decimal _discountFunctionPrice;

        /// <summary>
        /// Initializes a new instance of the <see cref="CoffeeDiscountStrategy"/> class.
        /// </summary>
        /// <param name="strategiesConfigurations">The configuration object containing discount rules.</param>
        public CoffeeDiscountStrategy(StrategiesConfigurations strategiesConfigurations)
        {
            int amountDefault = 3;
            decimal discountDefault = 2.00m / 3.00m;

            if (strategiesConfigurations.ConfigurationsRules.TryGetValue(ProductsCode.Coffee, out ProductDiscountRuleConfiguration? config))
            {
                _maxCountToDiscount = config.Amount ?? amountDefault;
                _discountFunctionPrice = config.Discount ?? discountDefault;
            }
            else
            {
                _maxCountToDiscount = amountDefault;
                _discountFunctionPrice = discountDefault;
            }
        }

        /// <inheritdoc/>
        public string ProductCode => ProductsCode.Coffee;

        /// <summary>
        /// Executes the discount strategy for coffee products.
        /// </summary>
        /// <param name="products">The list of products to apply the strategy to.</param>
        /// <returns>The total price after applying the discount strategy.</returns>
        public decimal Execute(List<Product> products)
        {
            return CalculateTotalByDiscountForAmount.Do(
                                                        products.Count,
                                                        _maxCountToDiscount,
                                                        products.First().Price,
                                                        _discountFunctionPrice * products.First().Price);
        }
    }
}
