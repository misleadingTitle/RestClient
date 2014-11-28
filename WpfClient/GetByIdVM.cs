using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using RestClientLib;
using Shared;

namespace WpfClient
{
    class GetByIdVM : EndpointVM
    {
        private const string webMethod = "GetArticleList/";

        public GetByIdVM()
        {
            this.Method = webMethod;
        }
        internal Article goForIt(Article sel, string Token)
        {
            var client = new RestClient();
            client.EndPoint = this.Endpoint+this.Method;
            client.Method = HttpVerb.GET;
            client.ContentType = "xml";
            //TOKEN!!!!!!
            if (!String.IsNullOrEmpty(Token))
            {
                string headerValue = string.Format("WRAP access_token=\"{0}\"", Token);
                client.Token = headerValue;
            }

            var result = client.MakeRequest(sel.Id);
            setResponseRequest(client);
            try
            {
                if (!String.IsNullOrEmpty(result))
                {
                    DataContractSerializer serializer = new DataContractSerializer(typeof(Article));
                    XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(new MemoryStream(Encoding.UTF8.GetBytes(result)), new XmlDictionaryReaderQuotas());

                    var games = serializer.ReadObject(reader);
                    return (Article)games;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
