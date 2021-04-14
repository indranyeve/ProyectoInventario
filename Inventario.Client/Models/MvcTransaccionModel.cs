using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inventario.Client.Models
{
    public class MvcTransaccionModel
    {
        public int Id_Transaccion { get; set; }
        public Tipo Tipo_Transaccion { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public int Id_Articulo { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public System.DateTime Fecha { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public int Cantidad { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public int Id_Almacen { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Estado { get; set; }

        public virtual mvcAlmacenModel Almacen { get; set; }
        public virtual MvcArticuloModel Articulo { get; set; }
    }


    public enum Tipo
    {
        Seleccione,
        Entrada,
        Salida,
        Traslado,
        Ajustes
    }
}
