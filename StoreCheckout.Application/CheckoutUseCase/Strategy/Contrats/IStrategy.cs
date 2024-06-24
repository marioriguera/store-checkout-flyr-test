using StoreCheckout.Domain.Entities;

namespace StoreCheckout.Application.CheckoutUseCase.Strategy.Contrats
{
    /// <summary>
    /// Defines a strategy interface for executing operations on a list of products.
    /// </summary>
    public interface IStrategy
    {
        /// <summary>
        /// Gets product code implemented by the strategy.
        /// </summary>
        string ProductCode { get; }

        /// <summary>
        /// Executes a specific operation on the provided list of products.
        /// </summary>
        /// <param name="products">The list of products on which to execute the strategy.</param>
        /// <returns>The result of the operation as a decimal value.</returns>
        decimal Execute(List<Product> products);
    }
}
