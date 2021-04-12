using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Inventario.Client.Models
{
    public class TipoInventario
    {
        
        public TipoInventario()
        {
            this.Articuloes = new HashSet<MvcArticuloModel>();
        }

        public int Id_TipoInventario { get; set; }

        [Display(Name = "Tipo Inventario")]
        public string Descripcion { get; set; }
        public int CuentaContable { get; set; }
        public string Estado { get; set; }

        public virtual ICollection<MvcArticuloModel> Articuloes { get; set; }
    }
}