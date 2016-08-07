using System.Collections.Generic;

namespace ZillowSearch.ZillowPropertySerach
{
    public interface IGenericRestServiceCaller
    {
        string GetStringResponse(string baseUrl, string action, IDictionary<string, string> parameters);
    }
}