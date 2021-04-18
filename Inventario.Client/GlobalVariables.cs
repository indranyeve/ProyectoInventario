using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace Inventario.Client
{
    public static class GlobalVariables
    {
        public static HttpClient WebApiClient = new HttpClient();

       static GlobalVariables()
        {
            WebApiClient.BaseAddress = new Uri("http://indranyeve-001-site1.dtempurl.com/");
            WebApiClient.DefaultRequestHeaders.Clear();
            WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}