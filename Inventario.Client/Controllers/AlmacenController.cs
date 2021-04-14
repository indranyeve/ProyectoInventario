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
    public class AlmacenController : Controller
    {
        //Llamando Servicio Almacen Service
        private readonly AlmacenService _almacenService = new AlmacenService();

        //Hosted web API REST Service base url  
        string Baseurl = "https://localhost:44350/";
        public async Task<ActionResult> Index()
        {
            List<mvcAlmacenModel> Info = new List<mvcAlmacenModel>();
            Info = await _almacenService.GetAlmacenModels();
            return View(Info);
        }

        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                return View(new mvcAlmacenModel());
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Almacens/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<mvcAlmacenModel>().Result);
            }
        }
        [HttpPost]
        public ActionResult AddOrEdit(mvcAlmacenModel Alm)
        {
            if (Alm.Id_Almacen == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Almacens", Alm).Result;
                TempData["SuccessMessage"] = "Save Successfully";
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("Almacens/" + Alm.Id_Almacen, Alm).Result;
                TempData["SuccessMessage"] = "Updated Successfully";
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("Almacens/" + id.ToString()).Result;
            return RedirectToAction("Index");
        }
    }
}