using System.Globalization;
using Newtonsoft.Json;

namespace StoreCheckout.Application.CheckoutUseCase.Configurations
{
    /// <summary>
    /// Represents a configuration manager for strategies related to product discounts.
    /// </summary>
    internal sealed class StrategiesConfigurations
    {
        private string _jsonConfgPath = Path.Combine(Directory.GetCurrentDirectory(), "CheckoutUseCase\\Configurations\\strategies.configuration.json");

        /// <summary>
        /// Initializes a new instance of the <see cref="StrategiesConfigurations"/> class.
        /// </summary>
        public StrategiesConfigurations()
        {
            DeserializeConfigurationJson();
        }

        /// <summary>
        /// Gets or sets the path to the JSON configuration file.
        /// </summary>
        public string JsonConfgPath
        {
            get => _jsonConfgPath;
            set
            {
                _jsonConfgPath = value;
                DeserializeConfigurationJson();
            }
        }

        /// <summary>
        /// Gets the configuration rules for product discount strategies from an external JSON configuration file.
        /// </summary>
        public Dictionary<string, ProductDiscountRuleConfiguration> ConfigurationsRules { get; private set; } = [];

        /// <summary>
        /// Deserializes the JSON configuration file and populates the <see cref="ConfigurationsRules"/> property.
        /// </summary>
        private void DeserializeConfigurationJson()
        {
            if (File.Exists(JsonConfgPath))
            {
                string json = File.ReadAllText(JsonConfgPath);

                var settings = new JsonSerializerSettings
                {
                    Culture = new CultureInfo("es-ES"),
                };

                ConfigurationsRules = JsonConvert.DeserializeObject<Dictionary<string, ProductDiscountRuleConfiguration>>(json, settings)
                    ?? new Dictionary<string, ProductDiscountRuleConfiguration>();
            }
            else
            {
                Console.WriteLine($"Can't find products discount rules configurations file at path: {JsonConfgPath}.");
            }
        }
    }

    /// <summary>
    /// Represent products discount rules configurations.
    /// </summary>
    /// <param name="Amount">Amount products value rule.</param>
    /// <param name="Discount">Discount products value rule.</param>
    public record ProductDiscountRuleConfiguration(int? Amount, decimal? Discount);
}
