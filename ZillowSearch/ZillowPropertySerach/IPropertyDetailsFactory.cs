using System.Collections.Generic;
using System.Xml.Linq;
using ZillowSearch.Models;

namespace ZillowSearch.ZillowPropertySerach
{
    public interface IPropertyDetailsFactory
    {
        IList<PropertyDetails> GetPropertyDetails(XElement xElement);
    }
}