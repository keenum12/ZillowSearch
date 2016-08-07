using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;

namespace ZillowSearch.Models.PropertyDetails
{
    public class PropertyDetailsFactory : IPropertyDetailsFactory
    {
        public IList<PropertyDetails> GetPropertyDetails(XElement xElement)
        {
            return (
                from el in xElement.Elements("result")
                select new PropertyDetails()
                {
                    ZPID = (int)el.Element("zpid"),
                    HomeDetailsLink = (string)el.Element("links")?.Element("homedetails") ?? "No Data Available",
                    GraphDataLink = (string)el.Element("links")?.Element("graphsanddata") ?? "No Data Available",
                    MapThisHomeLink = (string)el.Element("links")?.Element("mapthishome") ?? "No Data Available",
                    ComparablesLink = (string)el.Element("links")?.Element("comparables") ?? "No Data Available",
                    Address = GetPropertyAddress(el.Element("address")),
                    ZestimateDetail = GetZestimateDetails(el.Element("zestimate")),
                    ZestimateRentDetail = ((el.Element("rentzestimate") == null) ? null 
                        : GetRentZestimateDetails(el.Element("rentzestimate"))),
                    LocalRealEstate = (from lre in el.Element("localRealEstate").Elements("region")
                                            select GetLocalRealEstate(lre)).ToList()
                }).ToList();
        }

        private ZestimateDetail GetZestimateDetails(XElement xElement)
        {
            return new ZestimateDetail()
            {
                Amount = new Currency((string)xElement.Element("amount")?.Attribute("currency"),
                    (int)xElement.Element("amount")),
                LastUpdated = (string)xElement.Element("last-updated"),
                ValueChange30Day = new Currency((string)xElement.Element("valueChange")?.Attribute("currency"),
                    (int)xElement.Element("valueChange")),
                ValuationRangeLow = new Currency((string)xElement.Element("valuationRange")?.Element("low")?.Attribute("currency"),
                    (int)xElement.Element("valuationRange")?.Element("low")),
                ValuationRangeHigh = new Currency((string)xElement.Element("valuationRange")?.Element("high")?.Attribute("currency"),
                    (int)xElement.Element("valuationRange")?.Element("high")),
                Percentile = (string)xElement.Element("percentile") ?? "No Data Available"
            };
        }

        private ZestimateRentDetail GetRentZestimateDetails(XElement xElement)
        {
            return new ZestimateRentDetail()
            {
                RentEstimate = new Currency((string)xElement.Element("amount")?.Attribute("currency"),
                    (int)xElement.Element("amount")),
                LastUpdated = (string)xElement.Element("last-updated"),
                ValueChange30Day = new Currency((string)xElement.Element("valueChange")?.Attribute("currency"),
                    (int)xElement.Element("valueChange")),
                ValuationRangeLow = new Currency((string)xElement.Element("valuationRange")?.Element("low")?.Attribute("currency"),
                    (int)xElement.Element("valuationRange")?.Element("low")),
                ValuationRangeHigh = new Currency((string)xElement.Element("valuationRange")?.Element("high")?.Attribute("currency"),
                    (int)xElement.Element("valuationRange")?.Element("high"))
            };
        }

        private Address GetPropertyAddress(XElement xElement)
        {
            return new Address()
            {
                Street = (string)xElement.Element("street"),
                ZipCode = (int)xElement.Element("zipcode"),
                City = (string)xElement.Element("city"),
                State = (string)xElement.Element("state"),
                Latitude = (string)xElement.Element("latitude"),
                Longitude = (string)xElement.Element("longitude"),
            };
        }

        private LocalRealEstateDetail GetLocalRealEstate(XElement xElement)
        {
            return new LocalRealEstateDetail()
            {
                Region = new Region((int)xElement.Attribute("id"), (string)xElement.Attribute("name"),
                    (string)xElement.Attribute("type")),
                ZindexValue = (string)xElement.Element("zindexValue") ?? "No Data Available",
                ZindexYearChange = (string)xElement.Element("zindexOneYearChange") ?? "No Data Available",
                OverviewLink = (string)xElement.Element("links")?.Element("overview") ?? "No Data Available",
                ForSaleByOwnerLink = (string)xElement.Element("links")?.Element("forSaleByOwner") ?? "No Data Available",
                ForSaleLink = (string)xElement.Element("links")?.Element("forSale") ?? "No Data Available",
            };
        }
    }
}