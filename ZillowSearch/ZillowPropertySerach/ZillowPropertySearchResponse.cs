using System.Collections.Generic;
using System.Xml.Linq;
using ZillowSearch.Models;
using ZillowSearch.Models.PropertyDetails;

namespace ZillowSearch.ZillowPropertySerach
{
    public class ZillowPropertySearchResponse
    {
        public bool IsSuccessful { get; private set; }
        public string Message { get; private set; }
        public IList<PropertyDetail> Data { get; private set; }

        public ZillowPropertySearchResponse(bool isSuccessful, string message, IList<PropertyDetail> data = null)
        {
            IsSuccessful = isSuccessful;
            Message = message;
            Data = data;
        }
    }
}