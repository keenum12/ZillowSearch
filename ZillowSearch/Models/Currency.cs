namespace ZillowSearch.Models
{
    public class Currency
    {
        public string Type { get; private set; }
        public int DollarAmount { get; private set; }

        public Currency(string type, int dollars)
        {
            Type = type;
            DollarAmount = dollars;
        }
    }
}