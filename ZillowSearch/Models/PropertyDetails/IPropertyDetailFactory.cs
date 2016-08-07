using System.Collections.Generic;
using System.Xml.Linq;

namespace ZillowSearch.Models.PropertyDetails
{
    public interface IPropertyDetailFactory
    {
        IList<PropertyDetail> GetPropertyDetails(XElement xElement);
    }
}