using System.Collections.Generic;

namespace OCGatewayForWebApi
{
    public interface IUtilitiesProvider<in T>
    {
        Dictionary<string, string> GetHeaders(T parameters);
        string GetBaseUrl();
    }
}