using AutoMapper;
using StoreCheckout.Application.CheckoutUseCase.DTOs;
using StoreCheckout.Application.CheckoutUseCase.Strategy.Contrats;
using StoreCheckout.Domain.Entities;

namespace StoreCheckout.Application.CheckoutUseCase
{
    /// <summary>
    /// Represents a checkout process for handling products and calculating the total price using different discount strategies.
    /// </summary>
    public sealed class Checkout
    {
        private readonly IMapper _mapper;
        private readonly IStrategyContext _context;
        private readonly List<Product> _products = [];

        /// <summary>
        /// Initializes a new instance of the <see cref="Checkout"/> class.
        /// </summary>
        /// <param name="mapper">The mapper used for object-object mapping.</param>
        /// <param name="context">The strategy context for applying discount strategies.</param>
        public Checkout(IMapper mapper, IStrategyContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        /// <summary>
        /// Scans a product and adds it to the checkout process.
        /// </summary>
        /// <param name="product">The product to be added to the checkout.</param>
        public void Scan(ProductDTO product)
        {
            _products.Add(_mapper.Map<Product>(product));
        }

        /// <summary>
        /// Deletes a product from the checkout process.
        /// </summary>
        /// <param name="product">The product to be deleted from the checkout.</param>
        public void Delete(ProductDTO product)
        {
            var productToRemove = _products.FirstOrDefault(p => p.Code.Equals(product.Code));

            if (productToRemove is not null)
                _products.Remove(productToRemove);
        }

        /// <summary>
        /// Returns a copy of the products currently in the shopping cart.
        /// </summary>
        /// <returns>A new list containing copies of the products.</returns>
        public List<Product> SeeProductsInShoppingCart() => _products.ToList();

        /// <summary>
        /// Calculates the total price of all products in the checkout process, applying applicable discounts.
        /// </summary>
        /// <returns>The total price of all products after applying discounts.</returns>
        public decimal Total()
        {
            decimal total = 0;

            Dictionary<string, List<Product>> productsGroupByCode = _products.GroupBy(p => p.Code)
                                                                                .ToDictionary(g => g.Key, g => g.ToList());

            foreach (var item in productsGroupByCode)
            {
                _context.SetStrategy(item.Key);
                total += _context.ExecuteStrategy(item.Value);
            }

            return Math.Round(total, 2);
        }
    }
}
