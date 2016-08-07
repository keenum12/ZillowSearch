using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web;
using System.Linq;

namespace ZillowSearch.ZillowPropertySerach
{
    public class GenericRestServiceCaller : IGenericRestServiceCaller
    {   
        public string GetStringResponse(string baseUrl, string action, IDictionary<string,string> parameters)
        {
            if (string.IsNullOrWhiteSpace(baseUrl))
            {
                throw new ArgumentException("baseUrl is required.", nameof(baseUrl));
            }
            if (string.IsNullOrWhiteSpace(action))
            {
                throw new ArgumentException("action is required.", nameof(action));
            }

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                HttpResponseMessage response = client.GetAsync(action + "?" + string.Join("&", parameters.Select(
                    x => $"{HttpUtility.HtmlEncode(x.Key)}={HttpUtility.HtmlEncode(x.Value)}"))).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsStringAsync().Result;
                }
            }
            return null;
        }
    }
}
