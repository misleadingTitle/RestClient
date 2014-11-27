using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using RestClientLib;
using Shared;

namespace WpfClient
{
    class InsertVM : EndpointVM
    {
        private const string webMethod="InsertArticle/";

        public InsertVM()
        {
            this.Method = webMethod;
        }

        private string _id;
        public string Id
        {
            get { return _id; }
            set
            {
                this._id = value;
                OnPropertyChanged("Id");
            }
        }

        private Article _articleIns;
        public Article ArticleIns
        {
            get { return _articleIns; }
            set
            {
                this._articleIns = value;
                OnPropertyChanged("ArticleIns");
            }
        }

        internal void goForIt(string Token)
        {
            var client = new RestClient();
            client.EndPoint = this.Endpoint + this.Method;
            client.Method = HttpVerb.POST;
            client.ContentType = "application/json";

            //TOKEN!!!!!!
            string headerValue = string.Format("WRAP access_token=\"{0}\"", Token);
            client.Token=headerValue;

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Article));
            var writerStream =new MemoryStream();
            //XmlDictionaryWriter writer = XmlDictionaryWriter.CreateTextWriter(writerStreamnew, UTF8Encoding);
            serializer.WriteObject(writerStream, this.ArticleIns);
            StreamReader rd = new StreamReader(writerStream);
            rd.DiscardBufferedData();
            rd.BaseStream.Seek(0, SeekOrigin.Begin);
            rd.BaseStream.Position = 0;
            client.PostData=rd.ReadToEnd();
            var result = client.MakeRequest(this.ArticleIns.Id);
            Id = result;
        }
    }
}
