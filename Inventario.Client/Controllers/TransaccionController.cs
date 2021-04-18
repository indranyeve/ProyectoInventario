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
    public class TransaccionController : Controller
    {
        // GET: Transaccion
        //Hosted web API REST Service base url  
        string Baseurl = "http://indranyeve-001-site1.dtempurl.com/";

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

        //CREAR
        public async Task<ActionResult> AddOrEdit(int id = 0)
        {

            var selectList = new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "Seleccione", Selected = true }
            };

            var almacenList = await _almacenService.GetAlmacenModels();
            
            foreach (var item in almacenList)
            {
                selectList.Add(new SelectListItem { Value = item.Id_Almacen.ToString(), Text = item.Descripcion, Selected = false });
            }
            ViewBag.AlmacenList = selectList;

            var selectList2 = new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "Seleccione", Selected = true }
            };

            var articuloList = await _articuloService.GetArticuloModels();

            foreach (var item in articuloList)
            {
                selectList2.Add(new SelectListItem { Value = item.Id_Articulo.ToString(), Text = item.Descripcion, Selected = false });
            }
            ViewBag.ArticuloList = selectList2;



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
        //ACTUALIZAR
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
            UpdateCantidad(Trans.Cantidad, Trans.Id_Articulo, Trans.Tipo_Transaccion.ToString());

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("Transaccions/" + id.ToString()).Result;
            return RedirectToAction("Index");
        }

        public void UpdateCantidad(int a, int b, string TipoTrans)
        {
            //Articulo articulo = db.Articuloes.FirstOrDefault(x => x.Id_Articulo == a);

            if (TipoTrans == "Entrada")
            {
                sqlCommand("Update Articulo set Existencia += '" + a + "' Where Id_Articulo = '" + b + "'", openConnection());
            }
            else
            {
                sqlCommand("Update Articulo set Existencia -= '" + a + "' Where Id_Articulo = '" + b + "'", openConnection());
            }
        }

        public SqlConnection openConnection()
        {
            var connection = new SqlConnection(@"Data source=DESKTOP-3AO6C47;initial catalog=BaseDatosInventario;integrated security=True;");
            if (connection.State != ConnectionState.Open) connection.Open();
            return connection;
        }

        public SqlCommand sqlCommand(string sqlQuery, SqlConnection connection)
        {
            var newSqlCommand = new SqlCommand(sqlQuery, connection);
            int i = newSqlCommand.ExecuteNonQuery();
            return newSqlCommand;
        }
    }
}