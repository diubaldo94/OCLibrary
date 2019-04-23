using System.Net.Http;

namespace OCGatewayForWebApi
{
    public interface IAccessPoint
    {
        T MakeGetCall<T>(string url);
        HttpResponseMessage MakePostCall<T>(string url, T body);
        HttpResponseMessage MakeDeleteCall(string url);
    }
}