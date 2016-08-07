using ZillowSearch.Models;

namespace ZillowSearch.Models.PropertyDetails
{
    public class ZestimateDetail
    {
        public Currency Amount { get; set; }
        public string LastUpdated { get; set; }
        public Currency ValueChange30Day { get; set; }
        public Currency ValuationChangeLow { get; set; }
        public Currency ValuationChangeHigh { get; set; }
        public string Percentile { get; set; }
    }
}