namespace StoreCheckout.Application.CheckoutUseCase.DTOs
{
    /// <summary>
    /// Represents a data transfer object for a product, containing basic product details.
    /// </summary>
    /// <param name="Code">The unique code identifying the product.</param>
    /// <param name="Name">The name of the product.</param>
    /// <param name="Price">The price of the product.</param>
    public record ProductDTO(string Code, string Name, decimal Price);
}
