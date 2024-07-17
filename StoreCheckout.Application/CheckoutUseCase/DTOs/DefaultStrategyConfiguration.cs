using System.ComponentModel.DataAnnotations;

namespace StoreCheckout.Application.CheckoutUseCase.DTOs
{
    /// <summary>
    /// Represents the default strategy configuration.
    /// </summary>
    /// <param name="Amount">The required amount for the discount.</param>
    /// <param name="Discount">The discount value.</param>
    /// <param name="FirtsProduct">The first product to apply the discount.</param>
    internal record DefaultStrategyConfiguration([Required] int Amount, [Required] decimal Discount, [Required] string FirtsProduct);
}
