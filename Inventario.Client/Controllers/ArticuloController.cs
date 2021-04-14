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
    public class ArticuloController : Controller
    {
        //Hosted web API REST Service base url  
        string Baseurl = "https://localhost:44350/";

        //Llamando Servicio Articulo
        private readonly ArticuloService _articuloService = new ArticuloService();

        //Llamando Servicio Tipo Inventario
        private readonly TipoInventarioService _tipoInventarioService = new TipoInventarioService();

        public async Task<ActionResult> Index()
        {
            List<MvcArticuloModel> Info = new List<MvcArticuloModel>();

            Info = await _articuloService.GetArticuloModels();
            return View(Info);
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
                return View(new MvcArticuloModel());
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Articulo/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<MvcArticuloModel>().Result);
            }
        }
        //ACTUALIZAR
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

        //BORRAR
        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("Articulo/" + id.ToString()).Result;
            return RedirectToAction("Index");
        }

    }
}