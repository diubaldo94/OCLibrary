using System.Collections.Generic;
using System.Configuration;

namespace OCGatewayForWebApi
{
    public class UtilitiesProvider : IUtilitiesProvider<AuthParams>
    {
        public Dictionary<string, string> GetHeaders(AuthParams parameters)
        {
            var headerCollection = new Dictionary<string, string>()
            {
                {"Authorization", "Basic " + parameters.Hash},
                {"Accept", "application/json, text/plain" },
            };
            return headerCollection;
        }

        public string GetBaseUrl()
        {
            return ConfigurationManager.AppSettings["baseWebApiUrl"];
        }
    }
}