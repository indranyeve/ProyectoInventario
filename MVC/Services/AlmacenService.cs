using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using MVC.Models;
using Newtonsoft.Json;

namespace MVC.Services
{
    public class AlmacenService
    {
        //Hosted web API REST Service base url 
        string Baseurl = "https://localhost:44350/";
        public async Task<List<mvcAlmacenModel>> GetAlmacenModels()
        {
            List<mvcAlmacenModel> Info = new List<mvcAlmacenModel>();

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
                    HttpResponseMessage Res = await client.GetAsync("api/Almacens/");

                    //Checking the response is successful or not which is sent using HttpClient  
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var Response = Res.Content.ReadAsStringAsync().Result;

                        //Deserializing the response recieved from web api and storing into the Employee list  
                        Info = JsonConvert.DeserializeObject<List<mvcAlmacenModel>>(Response);

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