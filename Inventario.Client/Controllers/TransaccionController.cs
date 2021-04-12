using Inventario.Client.Models;
using Inventario.Client.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Inventario.Client.Controllers
{
    public class TransaccionController : Controller
    {
        // GET: Transaccion
        //Hosted web API REST Service base url  
        string Baseurl = "https://localhost:44350/";

        //Llamando Servicio Almacen
        private readonly AlmacenService _almacenService = new AlmacenService();

        //Llamando Servicio Articulo
        private readonly ArticuloService _articuloService = new ArticuloService();

        public async Task<ActionResult> Index()
        {
            List<MvcTransaccionModel> Info = new List<MvcTransaccionModel>();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("api/Transaccions/");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var Response = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    Info = JsonConvert.DeserializeObject<List<MvcTransaccionModel>>(Response);

                }
                //returning the employee list to view  
                return View(Info);
            }
        }

        public async Task<ActionResult> AddOrEdit(int id = 0)
        {
            //var tipoInventarioList = await _tipoInventarioService.GetTipoInventarios();
            //ViewBag.TipoInventarioList = tipoInventarioList.Select(x => new SelectListItem { Value = x.Id_TipoInventario.ToString(), Text = x.Descripcion }).ToList();

            var almacenList = await _almacenService.GetAlmacenModels();
            ViewBag.AlmacenList = almacenList.Select(x => new SelectListItem { Value = x.Id_Almacen.ToString(), Text = x.Descripcion }).ToList();

            var articuloList = await _articuloService.GetArticuloModels();
            ViewBag.ArticuloList = articuloList.Select(x => new SelectListItem { Value = x.Id_Articulo.ToString(), Text = x.Descripcion }).ToList();


            if (id == 0)
            {
                return View(new MvcTransaccionModel());
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Transaccions/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<MvcTransaccionModel>().Result);
            }
        }
        [HttpPost]
        public ActionResult AddOrEdit(MvcTransaccionModel Trans)
        {
            if (Trans.Id_Transaccion == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Transaccions", Trans).Result;
                TempData["SuccessMessage"] = "Save Successfully";
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("Transaccions/" + Trans.Id_Transaccion, Trans).Result;
                TempData["SuccessMessage"] = "Updated Successfully";
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("Transaccions/" + id.ToString()).Result;
            return RedirectToAction("Index");
        }
    }
}