using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using RestClientLib;
using Shared;

namespace WpfClient
{
    class MainVM : EndpointVM
    {
        private const string webMethod="GetArticleList/";

        public MainVM()
        {
            this.Method = webMethod;
        }
        private List<Article> _articleShowcase;
        public List<Article> ArticleShowcase
        {
            get { return _articleShowcase; }
            set
            {
                this._articleShowcase = value;
                OnPropertyChanged("ArticleShowcase");
            }
        }

        internal void goForIt(string Token)
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
            var result = client.MakeRequest();
            setResponseRequest(client);
            if (!String.IsNullOrEmpty(result))
            {
                try
                {
                    DataContractSerializer serializer = new DataContractSerializer(typeof(List<Article>));
                    XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(new MemoryStream(Encoding.UTF8.GetBytes(result)), new XmlDictionaryReaderQuotas());

                    var games = serializer.ReadObject(reader);
                    this.ArticleShowcase = (List<Article>)games;
                    reader.Close();
                }
                catch
                {
                }
            }
        }
    }
}
