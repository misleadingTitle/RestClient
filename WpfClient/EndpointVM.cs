﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfClient
{
    class EndpointVM:INotifyPropertyChanged
    {
        // Create the OnPropertyChanged method to raise the event 
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private string _lastResponse;
        public string LastResponse
        {
            get { return _lastResponse; }
            set
            {
                this._lastResponse = value;
                OnPropertyChanged("LastResponse");
            }
        }

        private string _lastRequest;
        public string LastRequest
        {
            get { return _lastRequest; }
            set
            {
                this._lastRequest = value;
                OnPropertyChanged("LastRequest");
            }
        }

        private string _lastRequestCode;
        public string LastRequestCode
        {
            get { return _lastRequestCode; }
            set
            {
                this._lastRequestCode = value;
                OnPropertyChanged("LastRequestCode");
            }
        }

        private string _endpoint;
        public string Endpoint
        {
            get { return _endpoint; }
            set
            {
                this._endpoint = value;
                OnPropertyChanged("Endpoint");
            }
        }

        private string _method;
        public string Method
        {
            get { return _method; }
            set
            {
                this._method = value;
                OnPropertyChanged("Method");
            }
        }

        protected void setResponseRequest(RestClientLib.RestClient client)
        {
            this.LastRequest = client.Request;
            this.LastResponse = client.Response;
            this.LastRequestCode = client.ResponseCode;
        }
    }
}
