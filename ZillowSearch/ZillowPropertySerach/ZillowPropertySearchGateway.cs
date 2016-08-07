using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;
using ZillowSearch.Models.PropertyDetails;

namespace ZillowSearch.ZillowPropertySerach
{
    public class ZillowPropertySearchGateway : IZillowPropertySearchGateway
    {
        private const string _baseURL = "http://www.zillow.com/webservice/";
        private const string _apiKey = "X1-ZWz1dyb53fdhjf_6jziz";
        private const string _genericErrorMessage = "An unknown error has occurred, please try again later.";

        private readonly IPropertyDetailFactory _propertyDetailsFactory;
        private readonly IGenericRestServiceCaller _genericRestServiceCaller;

        public ZillowPropertySearchGateway(IPropertyDetailFactory propertyDetailsFactory,
            IGenericRestServiceCaller genericRestCaller)
        {
            _propertyDetailsFactory = propertyDetailsFactory;
            _genericRestServiceCaller = genericRestCaller;
        }

        public ZillowPropertySearchResponse GetPropertyDetails(string address, string cityStateOrZipCode, bool includeRentData)
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
            string response = _genericRestServiceCaller.GetStringResponse(_baseURL, "GetSearchResults.htm",
                new Dictionary<string, string>() {
                    { "zws-id", _apiKey },
                    { "address", address },
                    { "citystatezip", cityStateOrZipCode },
                    { "rentzestimate", includeRentData.ToString().ToLower() }
                });

            // Process Result
            XElement xmlDocument = response == null ? null : XElement.Parse(response);
            if (xmlDocument == null)
            {
                return new ZillowPropertySearchResponse(false, _genericErrorMessage);
            }

            int responseCode = (int)xmlDocument.Element("message").Element("code");
            string responseText = (string)xmlDocument.Element("message").Element("text");
            if (responseCode == 0)
            {
                return new ZillowPropertySearchResponse(true, string.Empty,
                    _propertyDetailsFactory.GetPropertyDetails(xmlDocument.Element("response").Element("results")));
            }

            return new ZillowPropertySearchResponse(false, GetErrorCodeMessage(responseCode, responseText));
        }

        private string GetErrorCodeMessage(int errorCode, string responseText)
        {
            if (errorCode >= 500)
            {
                return responseText;
            }
            else
            {
                // TODO: Add some logging for these critical error messages
                return "The Zillow Web Service is currently not available. Please come back later and try again";
            }
        }
    }
}
