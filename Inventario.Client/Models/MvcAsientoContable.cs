using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventario.Client.Models
{
    public class MvcAsientoContable
    {
        public int Id_AsientosContables { get; set; }
        public string descripcion { get; set; }
        public int Id_TipoInventario { get; set; }
        public int cuentaContable { get; set; }
        public Tipo1 Tipo_de_Movimiento { get; set; }
        public System.DateTime Fecha_Asiento { get; set; }
        public decimal Monto_Asiento { get; set; }
        public string Estado { get; set; }

        public virtual TipoInventario TipoInventario { get; set; }
    }

    public enum Tipo1
    {
        Seleccione,
        DB,
        CR
    }
}