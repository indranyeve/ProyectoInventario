using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class MvcArticuloModel
    {
        public int Id_Articulo { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public int Id_TipoInventario { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Unidad { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public int Existencia { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public int Num_Referencia { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public decimal Costo_Unitario { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Estado { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Marca { get; set; }

        public virtual TipoInventario TipoInventario { get; set; }
    }
}