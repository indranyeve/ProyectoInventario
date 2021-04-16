using Inventario.Client.Models;
using Inventario.Client.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Inventario.Client.Controllers
{
    public class AsientoContableController : Controller
    {
        //Hosted web API REST Service base url  
        string Baseurl = "https://localhost:44350/";

        //Llamando Servicio Tipo Inventario
        private readonly TipoInventarioService _tipoInventarioService = new TipoInventarioService();

        // GET: AsientoContable
        public async Task<ActionResult> Index()
        {
            List<MvcAsientoContable> Info = new List<MvcAsientoContable>();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("api/AsientosContables/");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var Response = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    Info = JsonConvert.DeserializeObject<List<MvcAsientoContable>>(Response);

                }
                //returning the employee list to view  
                return View(Info);
            }
        }

        //CREATE
        public async Task<ActionResult> AddOrEdit(int id = 0)
        {
            var selectList = new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "Seleccione", Selected = true }
            };

            var tipoInventarioList = await _tipoInventarioService.GetTipoInventarios();
            foreach (var item in tipoInventarioList)
            {
                selectList.Add(new SelectListItem { Value = item.Id_TipoInventario.ToString(), Text = item.Descripcion, Selected = false });
            }

            ViewBag.TipoInventarioList = selectList;

            if (id == 0)
            {
                return View(new MvcAsientoContable());
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("AsientosContables/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<MvcAsientoContable>().Result);
            }
        }
        //ACTUALIZAR
        [HttpPost]
        public ActionResult AddOrEdit(MvcAsientoContable AsientoCon)
        {
            if (AsientoCon.Id_AsientosContables == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("AsientosContables", AsientoCon).Result;
                TempData["SuccessMessage"] = "Save Successfully";
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("AsientosContables/" + AsientoCon.Id_AsientosContables, AsientoCon).Result;
                TempData["SuccessMessage"] = "Updated Successfully";
            }

            return RedirectToAction("Index");
        }

        //BORRAR
        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("AsientosContables/" + id.ToString()).Result;
            return RedirectToAction("Index");
        }
    }
}