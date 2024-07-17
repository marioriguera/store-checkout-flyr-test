using StoreCheckout.Application.CheckoutUseCase.Configurations;
using StoreCheckout.Application.CheckoutUseCase.Strategy.Contrats;
using StoreCheckout.Application.CheckoutUseCase.Strategy.Implementations;
using StoreCheckout.Domain.Entities;

namespace StoreCheckout.Application.CheckoutUseCase.Strategy
{
    /// <summary>
    /// Represents a context for executing a strategy on a list of products.
    /// </summary>
    internal sealed class StrategyContext : IStrategyContext
    {
        private readonly IEnumerable<IStrategy> _strategies;
        private IStrategy? _strategy;

        /// <summary>
        /// Initializes a new instance of the <see cref="StrategyContext"/> class with the provided strategies.
        /// </summary>
        /// <param name="strategies">The collection of strategies implementations.</param>
        public StrategyContext(IEnumerable<IStrategy> strategies)
        {
            _strategies = strategies ?? new List<IStrategy>()
            {
                new ProductWithoutDiscountStrategy(),
            };
        }

        /// <inheritdoc/>
        public void SetStrategy(string key)
        {
            _strategy = _strategies.Where(s => s.DiscountCode.Equals(GetStrategyCodeByProductKey(key))).First();
        }

        /// <inheritdoc/>
        public decimal ExecuteStrategy(List<Product> products)
        {
            return _strategy?.Execute(products) ?? 0;
        }

        private string GetStrategyCodeByProductKey(string key)
        {
            return _strategies.Where(s => s.ProductsCode!.Contains(key)).FirstOrDefault()?.DiscountCode ?? DiscountCodes.Default;
        }
    }
}
