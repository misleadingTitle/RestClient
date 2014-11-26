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
    class DeleteVM : EndpointVM
    {
        private const string webMethod = "DeleteArticle/";

        public DeleteVM()
        {
            this.Method = webMethod;
        }
        internal string goForIt(Article sel)
        {
            var client = new RestClient();
            client.EndPoint = this.Endpoint+this.Method;
            client.Method = HttpVerb.DELETE;
            client.ContentType = "xml";

            var result = client.MakeRequest(sel.Id);
            DataContractSerializer serializer = new DataContractSerializer(typeof(String));
            XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(new MemoryStream(Encoding.UTF8.GetBytes(result)), new XmlDictionaryReaderQuotas());

            string games = (string)serializer.ReadObject(reader);
            return games;
        }
    }
}
