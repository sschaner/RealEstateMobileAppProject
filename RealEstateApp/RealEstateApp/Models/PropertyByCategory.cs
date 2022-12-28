namespace RealEstateApp.Models
{
    using Newtonsoft.Json;

    public class PropertyByCategory
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the detail.
        /// </summary>
        /// <value>
        /// The detail.
        /// </value>
        [JsonProperty("detail")]
        public string Detail { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        [JsonProperty("address")]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the image URL.
        /// </summary>
        /// <value>
        /// The image URL.
        /// </value>
        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }

        public string FullImageUrl => AppSettings.ApiUrl + ImageUrl;

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        [JsonProperty("price")]
        public int Price { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is trending.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is trending; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("isTrending")]
        public bool IsTrending { get; set; }

        /// <summary>
        /// Gets or sets the category identifier.
        /// </summary>
        /// <value>
        /// The category identifier.
        /// </value>
        [JsonProperty("categoryId")]
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        [JsonProperty("userId")]
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the bookmarks.
        /// </summary>
        /// <value>
        /// The bookmarks.
        /// </value>
        [JsonProperty("bookmarks")]
        public object Bookmarks { get; set; }
    }
}
