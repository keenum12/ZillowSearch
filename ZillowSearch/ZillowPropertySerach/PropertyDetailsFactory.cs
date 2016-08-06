using System.Xml.Linq;
using System.Linq;
using System;
using ZillowSearch.Models;
using System.Collections.Generic;

namespace ZillowSearch.ZillowPropertySerach
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
                    HomeDetailsLink = (string)el.Element("links")?.Element("homedetails"),
                    GraphDataLink = (string)el.Element("links")?.Element("graphsanddata"),
                    MapThisHomeLink = (string)el.Element("links")?.Element("mapthishome"),
                    ComparablesLink = (string)el.Element("links")?.Element("comparables"),
                    Address = GetPropertyAddress(el.Element("address")),
                    ZetimateDetails = GetZestimateDetails(el.Element("zestimate")),
                    LocalNeighborhoods = (from lre in el.Element("localRealEstate").Elements("region")
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
                ValuationChangeLow = new Currency((string)xElement.Element("valuationRange")?.Element("low")?.Attribute("currency"),
                    (int)xElement.Element("valuationRange")?.Element("low")),
                ValuationChangeHigh = new Currency((string)xElement.Element("valuationRange")?.Element("high")?.Attribute("currency"),
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
                Latitiude = (string)xElement.Element("latitude"),
                Longitude = (string)xElement.Element("longitude"),
            };
        }

        private LocalRealEstateDetail GetLocalRealEstate(XElement xElement)
        {
            return new LocalRealEstateDetail()
            {
                Region = new Region((int)xElement.Attribute("id"), (string)xElement.Attribute("name"),
                    (string)xElement.Attribute("type")),
                ZindexValue = (string)xElement.Element("zindexValue"),
                ZindexYearChange = (string)xElement.Element("zindexOneYearChange"),
                OverviewLink = (string)xElement.Element("links")?.Element("overview"),
                ForSaleByOwnerLink = (string)xElement.Element("links")?.Element("forSaleByOwner"),
                ForSaleLink = (string)xElement.Element("links")?.Element("forSale"),
            };
        }
    }
}