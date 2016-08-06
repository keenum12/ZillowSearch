using System;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;

namespace ZillowSearch.ZillowPropertySerach
{
    public class ZillowPropertySearchGateway : IZillowPropertySearchGateway
    {
        private const string _baseURL = "http://www.zillow.com/webservice/";
        private const string _apiKey = "X1-ZWz1dyb53fdhjf_6jziz";
        private const string _genericErrorMessage = "An unknown error has occurred, please try again later.";

        private readonly IPropertyDetailsFactory _propertyDetailsFactory;
        private readonly IGenericRestServiceCaller _genericRestServiceCaller;

        public ZillowPropertySearchGateway(IPropertyDetailsFactory propertyDetailsFactory,
            IGenericRestServiceCaller genericRestCaller)
        {
            _propertyDetailsFactory = propertyDetailsFactory;
            _genericRestServiceCaller = genericRestCaller;
        }

        public ZillowPropertySearchResponse GetPropertyDetails(string address, string cityStateOrZipCode)
        {
            // Verify Input
            if (string.IsNullOrWhiteSpace(address))
            {
                throw new ArgumentException("Address is required.", nameof(address));
            }

            if (string.IsNullOrWhiteSpace(cityStateOrZipCode))
            {
                throw new ArgumentException("City and State or ZipCode is required.");
            }

            // Call service
            string urlParameters = "?zws-id=" + _apiKey + "&address=" + HttpUtility.UrlEncode(address) 
                + "&citystatezip=" + HttpUtility.UrlEncode(cityStateOrZipCode);
            Task<string> response = _genericRestServiceCaller.GetStringResponse(_baseURL, 
                "GetSearchResults.htm" + urlParameters);

            // Process Result
            XDocument xmlDocument = response == null ? null : XDocument.Parse(response.Result);
            if (xmlDocument == null)
            {
                return new ZillowPropertySearchResponse(false, _genericErrorMessage);
            }

            int responseCode = (int)xmlDocument.Element("message").Element("code");
            if (responseCode == 0)
            {
                return new ZillowPropertySearchResponse(true, string.Empty,
                    _propertyDetailsFactory.GetPropertyDetails(xmlDocument.Element("response").Element("results")));
            }

            return new ZillowPropertySearchResponse(false, GetErrorCodeMessage(responseCode));
        }

        private string GetErrorCodeMessage(int errorCode)
        {
            switch(errorCode)
            {
                case 3:
                case 4:
                    return "The Zillow Web Service is currently not available. Please come back later and try again";
                case 500:
                    return "Invalid address.  Please verify the address and try again.";
                case 501:
                case 503:
                    return "Invalid city & state or zip code.  Please verify the information and try again.";
                case 502:
                case 507:
                    return "No data was found for the information provided.";
                case 504:
                    return "The specified area is not covered by Zillow property database.";
                case 505:
                    return "Request timed out, please try again later.";
                case 506:
                    return "Address provided is too long, please shorten the address using abbreviations and try again.";
                default:
                    return _genericErrorMessage;
            }
        }
    }
}
