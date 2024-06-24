using StoreCheckout.Application.CheckoutUseCase.Strategy.Contrats;
using StoreCheckout.Domain.Entities;

namespace StoreCheckout.Application.CheckoutUseCase.Strategy
{
    /// <summary>
    /// Represents a context for executing a strategy on a list of products.
    /// </summary>
    internal sealed class StrategyContext : IStrategyContext
    {
        private readonly Dictionary<string, IStrategy> _strategies;
        private IStrategy? _strategy;

        /// <summary>
        /// Initializes a new instance of the <see cref="StrategyContext"/> class with the provided strategies.
        /// </summary>
        /// <param name="strategies">The dictionary of strategies where the key is the strategy identifier and the value is the strategy implementation.</param>
        public StrategyContext(Dictionary<string, IStrategy> strategies)
        {
            _strategies = strategies;
        }

        /// <inheritdoc/>
        public void SetStrategy(string key)
        {
            if (_strategies.TryGetValue(key, out IStrategy? strategy))
            {
                _strategy = strategy;
                return;
            }

            _strategy = _strategies[string.Empty];
        }

        /// <inheritdoc/>
        public decimal ExecuteStrategy(List<Product> products)
        {
            return _strategy?.Execute(products) ?? 0;
        }
    }
}
