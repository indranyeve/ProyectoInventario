using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class MvcArticuloModel
    {
        public int Id_Articulo { get; set; }
        public string Descripcion { get; set; }
        public int Id_TipoInventario { get; set; }
        public string Unidad { get; set; }
        public int Existencia { get; set; }
        public int Num_Referencia { get; set; }
        public float Costo_Unitario { get; set; }
        public string Estado { get; set; }
        public string Marca { get; set; }
    }
}