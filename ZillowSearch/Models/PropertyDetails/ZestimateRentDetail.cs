namespace ZillowSearch.Models.PropertyDetails
{
    public class ZestimateRentDetail
    {
        public Currency RentEstimate { get; set; }
        public string LastUpdated { get; set; }
        public Currency ValueChange30Day { get; set; }
        public Currency ValuationRangeLow { get; set; }
        public Currency ValuationRangeHigh { get; set; }
    }
}