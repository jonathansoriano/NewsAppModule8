using NewsApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.Services
{
    public class ApiService
    {
        public async Task<Root> GetNews(string categoryName)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync($"https://gnews.io/api/v4/top-headlines?category={categoryName.ToLower()}&apikey=52d8a12aff5e69609892ccb76c62f8e2&lang=en");
            return JsonConvert.DeserializeObject<Root>(response); // Does this ever get deserialized into a C# Object? It does, because it works on HomePage...
        }
    }
}
