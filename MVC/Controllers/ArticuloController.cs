using MVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class ArticuloController : Controller
    {
        // GET: Articulo
        //Hosted web API REST Service base url  
        string Baseurl = "https://localhost:44350/";
        public async Task<ActionResult> Index()
        {
            List<MvcArticuloModel> Info = new List<MvcArticuloModel>();

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
                    Info = JsonConvert.DeserializeObject < List<MvcArticuloModel>>(Response);

                }
                //returning the employee list to view  
                return View(Info);
            }
        }

        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                return View(new MvcArticuloModel());
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Articulo/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<MvcArticuloModel>().Result);
            }
        }
        [HttpPost]
        public ActionResult AddOrEdit(MvcArticuloModel Art)
        {
            if (Art.Id_Articulo == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Articulo", Art).Result;
                TempData["SuccessMessage"] = "Save Successfully";
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("Articulo/" + Art.Id_Articulo, Art).Result;
                TempData["SuccessMessage"] = "Updated Successfully";
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("Articulo/" + id.ToString()).Result;
            return RedirectToAction("Index");
        }

    }
}