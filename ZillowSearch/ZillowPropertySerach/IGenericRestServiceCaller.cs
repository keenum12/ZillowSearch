using System.Threading.Tasks;

namespace ZillowSearch.ZillowPropertySerach
{
    public interface IGenericRestServiceCaller
    {
        Task<string> GetStringResponse(string baseUrl, string actionAndParameters);
    }
}