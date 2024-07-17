using StoreCheckout.Domain.Entities;

namespace StoreCheckout.Application.CheckoutUseCase.Strategy.Contrats
{
    /// <summary>
    /// Defines the contract for a strategy context that can execute a strategy on a list of products.
    /// </summary>
    public interface IStrategyContext
    {
        /// <summary>
        /// Sets the strategy to be executed.
        /// </summary>
        /// <param name="key">The key product to set the strategy.</param>
        void SetStrategy(string key);

        /// <summary>
        /// Executes the currently set strategy on the provided list of products.
        /// </summary>
        /// <param name="products">The list of products to apply the strategy to.</param>
        /// <returns>The result of executing the strategy.</returns>
        decimal ExecuteStrategy(List<Product> products);
    }
}
