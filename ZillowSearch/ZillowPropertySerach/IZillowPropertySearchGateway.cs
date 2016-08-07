using System.Threading.Tasks;
using System.Xml.Linq;

namespace ZillowSearch.ZillowPropertySerach
{
    public interface IZillowPropertySearchGateway
    {
        ZillowPropertySearchResponse GetPropertyDetails(string address, string cityStateOrZipCode, bool includeRentData);
    }
}
