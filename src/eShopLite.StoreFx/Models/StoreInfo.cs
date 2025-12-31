using System.Text.Json.Serialization;

namespace eShopLite.StoreFx.Models
{
    /// <summary>
    /// Represents store location information.
    /// </summary>
    public class StoreInfo
    {
        /// <summary>
        /// Gets or sets the unique identifier for the store.
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the store name.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the city where the store is located.
        /// </summary>
        [JsonPropertyName("city")]
        public string City { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the state where the store is located.
        /// </summary>
        [JsonPropertyName("state")]
        public string State { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the store operating hours.
        /// </summary>
        [JsonPropertyName("hours")]
        public string Hours { get; set; } = string.Empty;
    }
}
