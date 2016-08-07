using System.Collections.Generic;
using System.Xml.Linq;

namespace ZillowSearch.Models.PropertyDetails
{
    public interface IPropertyDetailsFactory
    {
        IList<PropertyDetails> GetPropertyDetails(XElement xElement);
    }
}