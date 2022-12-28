namespace RealEstateApp.Services
{
    using Newtonsoft.Json;
    using RealEstateApp.Models;
    using System.Net.Http.Headers;
    using System.Text;

    public static class ApiService
    {
        /// <summary>
        /// Registers the user.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <param name="phone">The phone.</param>
        /// <returns></returns>
        public static async Task<bool> RegisterUser(string name, string email, string password, string phone)
        {
            var register = new Register()
            {
                Name = name,
                Email = email,
                Password = password,
                Phone = phone
            };

            HttpClientHandlerService handler = new HttpClientHandlerService();
            HttpClient httpClient = new HttpClient(handler.GetPlatformMessageHandler());

            var json = JsonConvert.SerializeObject(register);
            var content =  new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync($"{AppSettings.ApiUrl}api/users/register", content);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Logins the specified email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public static async Task<bool> Login(string email, string password)
        {
            var login = new Login()
            {
                Email = email,
                Password = password
            };

            HttpClientHandlerService handler = new HttpClientHandlerService();
            HttpClient httpClient = new HttpClient(handler.GetPlatformMessageHandler());

            // Convert C# classes and objects to Json is Serialization
            var json = JsonConvert.SerializeObject(login);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync($"{AppSettings.ApiUrl}api/users/login", content);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            var jsonResult = await response.Content.ReadAsStringAsync();
            // Convert Json data to C# classes and objects is Deserialization
            var result = JsonConvert.DeserializeObject<Token>(jsonResult);

            Preferences.Set("accesstoken", result.AccessToken);
            Preferences.Set("userid", result.UserId);
            Preferences.Set("username", result.UserName);

            return true;
        }

        /// <summary>
        /// Gets the categories.
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Category>> GetCategories()
        {
            HttpClientHandlerService handler = new HttpClientHandlerService();
            HttpClient httpClient = new HttpClient(handler.GetPlatformMessageHandler());

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));

            var response = await httpClient.GetStringAsync($"{AppSettings.ApiUrl}api/categories");

            return JsonConvert.DeserializeObject<List<Category>>(response);
        }

        /// <summary>
        /// Gets the trending properties.
        /// </summary>
        /// <returns></returns>
        public static async Task<List<TrendingProperty>> GetTrendingProperties()
        {
            HttpClientHandlerService handler = new HttpClientHandlerService();
            HttpClient httpClient = new HttpClient(handler.GetPlatformMessageHandler());

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));

            var response = await httpClient.GetStringAsync($"{AppSettings.ApiUrl}api/Properties/TrendingProperties");

            return JsonConvert.DeserializeObject<List<TrendingProperty>>(response);
        }

        /// <summary>
        /// Finds the properties.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        public static async Task<List<SearchProperty>> FindProperties(string address)
        {
            HttpClientHandlerService handler = new HttpClientHandlerService();
            HttpClient httpClient = new HttpClient(handler.GetPlatformMessageHandler());

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));

            var response = await httpClient.GetStringAsync($"{AppSettings.ApiUrl}api/Properties/SearchProperties?address=Tower");

            return JsonConvert.DeserializeObject<List<SearchProperty>>(response);
        }

        /// <summary>
        /// Gets the property by category.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns></returns>
        public static async Task<List<PropertyByCategory>> GetPropertyByCategory(int categoryId)
        {
            HttpClientHandlerService handler = new HttpClientHandlerService();
            HttpClient httpClient = new HttpClient(handler.GetPlatformMessageHandler());

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));

            var response = await httpClient.GetStringAsync($"{AppSettings.ApiUrl}api/Properties/PropertyList?categoryId={categoryId}");

            return JsonConvert.DeserializeObject<List<PropertyByCategory>>(response);
        }

        /// <summary>
        /// Gets the property detail.
        /// </summary>
        /// <param name="propertyId">The property identifier.</param>
        /// <returns></returns>
        public static async Task<PropertyDetail> GetPropertyDetail(int propertyId)
        {
            HttpClientHandlerService handler = new HttpClientHandlerService();
            HttpClient httpClient = new HttpClient(handler.GetPlatformMessageHandler());

            // Get the access token
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));

            var response = await httpClient.GetStringAsync($"{AppSettings.ApiUrl}api/Properties/PropertyDetail?id={propertyId}");

            return JsonConvert.DeserializeObject<PropertyDetail>(response);
        }

        /// <summary>
        /// Gets the bookmark list.
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Bookmark>> GetBookmarkList()
        {
            HttpClientHandlerService handler = new HttpClientHandlerService();
            HttpClient httpClient = new HttpClient(handler.GetPlatformMessageHandler());

            // Get the access token
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));

            var response = await httpClient.GetStringAsync($"{AppSettings.ApiUrl}api/bookmarks");

            return JsonConvert.DeserializeObject<List<Bookmark>>(response);
        }

        /// <summary>
        /// Adds the bookmark.
        /// </summary>
        /// <param name="addBookmark">The add bookmark.</param>
        /// <returns></returns>
        public static async Task<bool> AddBookmark(AddBookmark addBookmark)
        {
            HttpClientHandlerService handler = new HttpClientHandlerService();
            HttpClient httpClient = new HttpClient(handler.GetPlatformMessageHandler());

            var json = JsonConvert.SerializeObject(addBookmark);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Get access token
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));

            var response = await httpClient.PostAsync($"{AppSettings.ApiUrl}api/bookmarks", content);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Deletes the bookmark.
        /// </summary>
        /// <param name="bookmarkId">The bookmark identifier.</param>
        /// <returns></returns>
        public static async Task<bool> DeleteBookmark(int bookmarkId)
        {
            HttpClientHandlerService handler = new HttpClientHandlerService();
            HttpClient httpClient = new HttpClient(handler.GetPlatformMessageHandler());

            // Get access token
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));

            var response = await httpClient.DeleteAsync($"{AppSettings.ApiUrl}api/bookmarks/{bookmarkId}");

            if(!response.IsSuccessStatusCode)
            {
                return false;
            }

            return true;
        }
    }
}
