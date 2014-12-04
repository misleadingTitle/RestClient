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
    class UpdateVM : EndpointVM
    {
        private const string webMethod="UpdateArticle/";

        public UpdateVM()
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

        private Article _articleUpd;
        public Article ArticleUpd
        {
            get { return _articleUpd; }
            set
            {
                this._articleUpd = value;
                OnPropertyChanged("ArticleUpd");
            }
        }

        internal void goForIt(string Token)
        {
            var client = new RestClient();
            client.EndPoint = this.Endpoint + this.Method;
            client.Method = HttpVerb.PUT;
            client.ContentType = "application/json";
            if (!String.IsNullOrEmpty(Token))
            {
                string headerValue = string.Format("WRAP access_token=\"{0}\"", Token);
                client.Token = headerValue;
            }



            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Article));
            var writerStream =new MemoryStream();
            //XmlDictionaryWriter writer = XmlDictionaryWriter.CreateTextWriter(writerStreamnew, UTF8Encoding);
            serializer.WriteObject(writerStream, this.ArticleUpd);
            StreamReader rd = new StreamReader(writerStream);
            rd.DiscardBufferedData();
            rd.BaseStream.Seek(0, SeekOrigin.Begin);
            rd.BaseStream.Position = 0;
            client.PostData=rd.ReadToEnd();
            var result = client.MakeRequest(this.ArticleUpd.Id);
            setResponseRequest(client);
            Id = result;
        }
    }
}
