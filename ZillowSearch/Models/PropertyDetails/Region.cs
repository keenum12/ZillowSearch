namespace ZillowSearch.Models.PropertyDetails
{
    public class Region
    {
        public int RegionID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public Region (int regionID, string name, string type)
        {
            RegionID = regionID;
            Name = name;
            Type = type;
        }
    }
}