using MVC.Models;
using MVC.Services;
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
    public class TipoInvetarioController : Controller
    {
        //Llamando Servicio Tipo Inventario
        private readonly TipoInventarioService _tipoInventarioService = new TipoInventarioService();

        //Hosted web API REST Service base url  
        string Baseurl = "https://localhost:44350/";
        public async Task<ActionResult> Index()
        {
            List<MvcTipoInventarioModel> Info = new List<MvcTipoInventarioModel>();

            Info = await _tipoInventarioService.GetTipoInventarios();
            return View(Info);
        }

        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                return View(new MvcTipoInventarioModel());
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("TipoInvetario/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<MvcTipoInventarioModel>().Result);
            }
        }
        [HttpPost]
        public ActionResult AddOrEdit(MvcTipoInventarioModel TipoInv)
        {
            if (TipoInv.Id_TipoInventario == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("TipoInventario", TipoInv).Result;
                TempData["SuccessMessage"] = "Save Successfully";
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("TipoInventario/" + TipoInv.Id_TipoInventario, TipoInv).Result;
                TempData["SuccessMessage"] = "Updated Successfully";
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("TipoInventario/" + id.ToString()).Result;
            return RedirectToAction("Index");
        }

    }
}