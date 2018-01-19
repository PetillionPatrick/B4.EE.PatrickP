using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Kwaliteit.Domain.Services.API
{
    public class APIService
    {
        public HttpClient Client { get; set; }

        public APIService(string nav)
        {
            Client = new HttpClient();
            
        }
    }
}
