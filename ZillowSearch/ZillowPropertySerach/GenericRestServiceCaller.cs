using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;

namespace ZillowSearch.ZillowPropertySerach
{
    public class GenericRestServiceCaller : IGenericRestServiceCaller
    {   
        public async Task<string> GetStringResponse(string baseUrl, string actionAndParameters)
        {
            if (string.IsNullOrWhiteSpace(baseUrl))
            {
                throw new ArgumentException("baseUrl is required.", nameof(baseUrl));
            }
            if (string.IsNullOrWhiteSpace(actionAndParameters))
            {
                throw new ArgumentException("actionAndParameters is required.", nameof(actionAndParameters));
            }

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                
                HttpResponseMessage response = await client.GetAsync(HttpUtility.UrlEncode(actionAndParameters));
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsStringAsync().Result;
                }
            }
            return null;
        }
    }
}
