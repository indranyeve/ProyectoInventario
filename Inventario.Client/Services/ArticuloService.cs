using Inventario.Client.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace Inventario.Client.Services
{
    public class ArticuloService
    {
        string Baseurl = "http://indranyeve-001-site1.dtempurl.com/";
        public async Task<List<MvcArticuloModel>> GetArticuloModels()
        {
            List<MvcArticuloModel> Info = new List<MvcArticuloModel>();

            try
            {
                using (var client = new HttpClient())
                {
                    //Passing service base url  
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                    HttpResponseMessage Res = await client.GetAsync("api/Articulo/");

                    //Checking the response is successful or not which is sent using HttpClient  
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var Response = Res.Content.ReadAsStringAsync().Result;

                        //Deserializing the response recieved from web api and storing into the Employee list  
                        Info = JsonConvert.DeserializeObject<List<MvcArticuloModel>>(Response);

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return Info;
        }
    }
}