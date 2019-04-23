using System;
using System.Text;

namespace OCGatewayForWebApi
{
    public class AuthParams
    {
        public string User { get; set; }
        public string Password { get; set; }
        public string Hash => Convert.ToBase64String(Encoding.UTF8.GetBytes(User + ":" + Password));
    }
}