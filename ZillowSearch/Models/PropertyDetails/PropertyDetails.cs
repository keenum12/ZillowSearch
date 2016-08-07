using System.Collections.Generic;

namespace ZillowSearch.Models.PropertyDetails
{
    public class PropertyDetails
    {
        public int ZPID { get; set; }
        public string HomeDetailsLink { get; set; }
        public string GraphDataLink { get; set; }
        public string MapThisHomeLink { get; set; }
        public string ComparablesLink { get; set; }

        public Address Address { get; set; }
        public ZestimateDetail ZestimateDetail { get; set; }
        public ZestimateRentDetail ZestimateRentDetail { get; set; }
        public IList<LocalRealEstateDetail> LocalRealEstate { get; set; }
    }
}