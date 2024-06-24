using Microsoft.Extensions.DependencyInjection;
using StoreCheckout.Application;
using StoreCheckout.Application.CheckoutUseCase;
using StoreCheckout.Application.CheckoutUseCase.DTOs;

namespace StoreCheckout.Presentation
{
    /// <summary>
    /// Main class where the application starts.
    /// </summary>
    internal class Program
    {
        private static List<ProductDTO> _availableProducts = new List<ProductDTO>
        {
            new ProductDTO("GR1", "Green Tea", 3.11m),
            new ProductDTO("SR1", "Strawberries", 5.00m),
            new ProductDTO("CF1", "Coffee", 11.23m),
            new ProductDTO("PT1", "Potatoes", 3.00m),
            new ProductDTO("BR1", "Beer", 3.00m),
            new ProductDTO("CH1", "Chocolate", 2.50m),
        };

        /// <summary>
        /// Main function.
        /// </summary>
        /// <param name="args">String array of arguments.</param>
        public static void Main(string[] args)
        {
            // Create services collection.
            var serviceCollection = new ServiceCollection();

            // Add services from application layer.
            serviceCollection.AddApplication();

            // Get service provider.
            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            // Get checkout instance.
            Checkout checkout = serviceProvider.GetService<Checkout>() ?? throw new Exception($"Cant resolve {nameof(Checkout)} dependency.");
            ShowMenu(checkout);
        }

        private static void ShowMenu(Checkout checkout)
        {
            Console.WriteLine("Welcome to the interactive checkout!");

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Choose an action:");
                Console.WriteLine("1. Add a product");
                Console.WriteLine("2. Remove a product");
                Console.WriteLine("3. Show total price");
                Console.WriteLine("4. Finish and exit");

                Console.Write("Enter your choice (1-4): ");
                string input = Console.ReadLine() ?? "4";

                bool exit = input switch
                {
                    "1" => AddProduct(checkout),
                    "2" => RemoveProduct(checkout),
                    "3" => ShowTotalPrice(checkout),
                    "4" => FinishAndExit(checkout),
                    _ => InvalidChoiceSelected(),
                };

                if (exit) return;
            }
        }

        private static bool AddProduct(Checkout checkout)
        {
            Console.WriteLine("Available products:");
            for (int i = 0; i < _availableProducts.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_availableProducts[i].Code} - {_availableProducts[i].Name} (£{_availableProducts[i].Price})");
            }

            Console.Write("Enter product number to add: ");
            if (int.TryParse(Console.ReadLine(), out int productNumber) && productNumber > 0 && productNumber <= _availableProducts.Count)
            {
                ProductDTO selectedProduct = _availableProducts[productNumber - 1];
                checkout.Scan(selectedProduct);
                Console.WriteLine($"{selectedProduct.Name} added to cart.");
            }
            else
            {
                Console.WriteLine("Invalid product number.");
            }

            Console.Clear();
            return false;
        }

        private static bool RemoveProduct(Checkout checkout)
        {
            Console.WriteLine("Products in cart:");
            var productsInCart = checkout.SeeProductsInShoppingCart();
            for (int i = 0; i < productsInCart.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {productsInCart[i].Name} (£{productsInCart[i].Price})");
            }

            Console.Write("Enter product number to remove: ");
            if (int.TryParse(Console.ReadLine(), out int productNumber) && productNumber > 0 && productNumber <= productsInCart.Count)
            {
                var prod = productsInCart[productNumber - 1];
                ProductDTO selectedProduct = new(prod.Code, prod.Name, prod.Price);
                checkout.Delete(selectedProduct);
                Console.WriteLine($"{selectedProduct.Name} removed from cart.");
            }
            else
            {
                Console.WriteLine("Invalid product number.");
            }

            Console.Clear();
            return false;
        }

        private static bool ShowTotalPrice(Checkout checkout)
        {
            decimal totalPrice = checkout.Total();
            Console.WriteLine($"Total price in cart: £{totalPrice}");
            return false;
        }

        private static bool FinishAndExit(Checkout checkout)
        {
            decimal totalPrice = checkout.Total();
            Console.WriteLine($"Final total price: £{totalPrice}");
            Console.WriteLine("Thank you for using the checkout. Goodbye!");
            return true;
        }

        private static bool InvalidChoiceSelected()
        {
            Console.WriteLine("Invalid choice. Please enter a number between 1 and 4.");
            return false;
        }
    }
}
