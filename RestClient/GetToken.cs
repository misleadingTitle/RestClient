using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RestClientLib
{
    static public class GetToken
    {
        //static string serviceNamespace = "restfulproject";
        //static string acsHostUrl = "accesscontrol.windows.net";
        //static string realm = @"http://localhost:50052/RESTfulWCFUsersServiceEndPoint.svc";
        //static string uid = "prova";
        //static string pwd = "prova";
        public static string GetTokenFromACS(string scope, string serviceNamespace, string acsHostUrl, string uid, string pwd)
        {
            string wrapPassword = pwd;
            string wrapUsername = uid;

            // request a token from ACS
            WebClient client = new WebClient();

            //--------------PROXY!!!!
            //WebProxy p = new WebProxy("10.173.201.11:3128", true);
            //p.Credentials = new NetworkCredential("manuel minuti", "password");
            //WebRequest.DefaultWebProxy = p;
            //client.Proxy = p;
            //-----------------------
            client.BaseAddress = string.Format("https://{0}.{1}", serviceNamespace, acsHostUrl);

            NameValueCollection values = new NameValueCollection();
            values.Add("wrap_name", wrapUsername);
            values.Add("wrap_password", wrapPassword);
            values.Add("wrap_scope", scope);

            byte[] responseBytes = client.UploadValues("WRAPv0.9/", "POST", values);

            string response = Encoding.UTF8.GetString(responseBytes);

            Console.WriteLine("\nreceived token from ACS: {0}\n", response);

            return HttpUtility.UrlDecode(
                response
                .Split('&')
                .Single(value => value.StartsWith("wrap_access_token=", StringComparison.OrdinalIgnoreCase))
                .Split('=')[1]);
        }
    }
}
