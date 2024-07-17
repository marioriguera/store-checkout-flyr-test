using StoreCheckout.Application.CheckoutUseCase.Configurations;
using StoreCheckout.Application.CheckoutUseCase.DTOs;

namespace StoreCheckout.Application.CheckoutUseCase.Strategy.Abstractions
{
    /// <summary>
    /// Abstract base class for discount strategies based on product amount.
    /// </summary>
    internal abstract class DiscountByAmountBase
    {
        private int _amountOfProductsToApplyStrategy;
        private decimal _discountPrice;

        /// <summary>
        /// Initializes a new instance of the <see cref="DiscountByAmountBase"/> class.
        /// </summary>
        /// <param name="strategiesConfigurations">The configuration settings for strategies.</param>
        /// <param name="strategyDiscountName">The name of the strategy discount.</param>
        /// <param name="defaultStrategyConfiguration">The default strategy configuration to use if none is provided.</param>
        public DiscountByAmountBase(
                                    StrategiesConfigurations? strategiesConfigurations,
                                    string strategyDiscountName,
                                    DefaultStrategyConfiguration defaultStrategyConfiguration)
        {
            if (strategiesConfigurations is null)
            {
                InitializeProperties(defaultStrategyConfiguration.Amount, defaultStrategyConfiguration.Discount, new List<string> { defaultStrategyConfiguration.FirtsProduct });
                return;
            }

            if (strategiesConfigurations!.ConfigurationsRules.TryGetValue(strategyDiscountName, out ProductDiscountRuleConfiguration? config))
            {
                InitializeProperties(config.Amount ?? defaultStrategyConfiguration.Amount, config.Discount ?? defaultStrategyConfiguration.Discount, config.Products);
                return;
            }

            InitializeProperties(defaultStrategyConfiguration.Amount, defaultStrategyConfiguration.Discount, new List<string> { defaultStrategyConfiguration.FirtsProduct });
        }

        /// <summary>
        /// Gets or sets the amount of products required to apply the strategy.
        /// </summary>
        protected int AmountOfProductsToApplyStrategy
        {
            get => _amountOfProductsToApplyStrategy;
            set => _amountOfProductsToApplyStrategy = (value == int.MaxValue) ? 0 : value;
        }

        /// <summary>
        /// Gets or sets the discount price to apply.
        /// </summary>
        protected decimal DiscountPrice
        {
            get => _discountPrice;
            set => _discountPrice = (value == decimal.MaxValue) ? 0 : value;
        }

        /// <summary>
        /// Gets or sets the collection of product codes applicable for the discount.
        /// </summary>
        protected IEnumerable<string>? ProductsCodeToAmount { get; set; }

        private void InitializeProperties(int amount = int.MinValue, decimal discount = decimal.Zero, IEnumerable<string>? products = null)
        {
            AmountOfProductsToApplyStrategy = amount;
            DiscountPrice = discount;
            ProductsCodeToAmount = products ?? Enumerable.Empty<string>();
        }
    }
}