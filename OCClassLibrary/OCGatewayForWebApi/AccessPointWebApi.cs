using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Web;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace OCGatewayForWebApi
{
    public class AccessPointWebApi : IAccessPoint
    {
        private readonly string _baseUrl;
        private readonly Dictionary<string, string> _headerDictionary; 

        public AccessPointWebApi(string baseUrl, Dictionary<string, string> headerDictionary)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = CertificateHandler;
            _baseUrl = baseUrl;
            _headerDictionary = headerDictionary;
        }

        public T MakeGetCall<T>(string url)
        {
            try
            {
                using (var client = new HttpClient())
                {

                    client.DefaultRequestHeaders.Clear();
                    foreach (var header in _headerDictionary)
                    {
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                    client.BaseAddress = new Uri(_baseUrl);
                    var response = client.GetAsync(url).Result;
                    var result = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<T>(result);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Failed to get resource: see inner exception", e);
            }
        }

        public HttpResponseMessage MakePostCall<T>(string url, T body)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_baseUrl);
                    client.DefaultRequestHeaders.Clear();
                    foreach (var header in _headerDictionary)
                    {
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                    var v = JsonConvert.SerializeObject(body);
                    var stringContent = new StringContent(v, Encoding.UTF8,
                        "application/json");
                    var response = client.PostAsync(url, stringContent).Result;
                    return response;
                }
            }
            catch (Exception e)
            {
                var request = new HttpRequestMessage();
                return request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        public HttpResponseMessage MakeDeleteCall(string url)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_baseUrl);
                    client.DefaultRequestHeaders.Clear();
                    foreach (var header in _headerDictionary)
                    {
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                    var response = client.DeleteAsync(url).Result;
                    return response;
                }
            }
            catch (Exception e)
            {
                var request = new HttpRequestMessage();
                return request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        private static bool CertificateHandler(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors SSLerror)
        {
            return true;
        }
    }
}


