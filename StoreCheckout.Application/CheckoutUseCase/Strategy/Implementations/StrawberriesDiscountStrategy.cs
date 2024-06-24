using StoreCheckout.Application.CheckoutUseCase.Configurations;
using StoreCheckout.Application.CheckoutUseCase.Strategy.Common;
using StoreCheckout.Application.CheckoutUseCase.Strategy.Contrats;
using StoreCheckout.Domain.Entities;

namespace StoreCheckout.Application.CheckoutUseCase.Strategy.Implementations
{
    /// <summary>
    /// Represents a strategy for applying discounts to strawberries products based on configured rules.
    /// </summary>
    internal sealed class StrawberriesDiscountStrategy : IStrategy
    {
        private readonly int _maxCountToDiscount;
        private readonly decimal _discountPrice;

        /// <summary>
        /// Initializes a new instance of the <see cref="StrawberriesDiscountStrategy"/> class.
        /// </summary>
        /// <param name="strategiesConfigurations">The configuration object containing discount rules.</param>
        public StrawberriesDiscountStrategy(StrategiesConfigurations strategiesConfigurations)
        {
            int amountDefault = 3;
            decimal discountDefault = 4.50m;

            if (strategiesConfigurations.ConfigurationsRules.TryGetValue(ProductsCode.Strawberries, out ProductDiscountRuleConfiguration? config))
            {
                _maxCountToDiscount = config.Amount ?? amountDefault;
                _discountPrice = config.Discount ?? discountDefault;
            }
            else
            {
                _maxCountToDiscount = amountDefault;
                _discountPrice = discountDefault;
            }
        }

        /// <inheritdoc/>
        public string ProductCode => ProductsCode.Strawberries;

        /// <summary>
        /// Executes the discount strategy for strawberries products.
        /// </summary>
        /// <param name="products">The list of products to apply the strategy to.</param>
        /// <returns>The total price after applying the discount strategy.</returns>
        public decimal Execute(List<Product> products)
        {
            return CalculateTotalByDiscountForAmount.Do(products.Count, _maxCountToDiscount, products.First().Price, _discountPrice);
        }
    }
}
