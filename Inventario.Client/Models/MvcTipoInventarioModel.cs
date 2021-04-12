using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inventario.Client.Models
{
    public class MvcTipoInventarioModel
    {
        public int Id_TipoInventario { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public int CuentaContable { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Estado { get; set; }
    }
}