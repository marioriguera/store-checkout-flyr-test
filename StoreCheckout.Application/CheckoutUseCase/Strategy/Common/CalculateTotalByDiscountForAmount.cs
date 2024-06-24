namespace StoreCheckout.Application.CheckoutUseCase.Strategy.Common
{
    /// <summary>
    /// Static class containing a method to calculate total cost with optional discount based on amount.
    /// </summary>
    internal static class CalculateTotalByDiscountForAmount
    {
        /// <summary>
        /// Calculates the total cost based on the provided count, applying a discount if the count meets or exceeds the specified threshold.
        /// </summary>
        /// <param name="count">The count of items.</param>
        /// <param name="countToDiscount">The threshold count at which the discount applies.</param>
        /// <param name="price">The original price per item.</param>
        /// <param name="priceToDiscount">The discounted price per item.</param>
        /// <returns>The total cost after applying any applicable discount.</returns>
        public static decimal Do(int count, int countToDiscount, decimal price, decimal priceToDiscount)
        {
            if (count >= countToDiscount) return count * priceToDiscount;
            return count * price;
        }
    }
}
