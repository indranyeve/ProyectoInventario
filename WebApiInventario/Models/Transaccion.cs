//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApiInventario.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Transaccion
    {
        public int Id_Transaccion { get; set; }
        public string Tipo_Transaccion { get; set; }
        public int Id_Articulo { get; set; }
        public System.DateTime Fecha { get; set; }
        public int Cantidad { get; set; }
        public int Id_Almacen { get; set; }
        public string Estado { get; set; }
    }
}