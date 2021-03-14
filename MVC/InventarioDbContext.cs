using MVC.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace MVC
{
    public class InventarioDbContext : DbContext
    {
        public InventarioDbContext() : base("name=DefaultConnection")
        {

        }

        public DbSet<mvcAlmacenModel> Almacen { get; set; }
        //public DbSet<Articulo> Articulo { get; set; }
        //public DbSet<AsientosContables> AsientosContables { get; set; }
        //public DbSet<TipoInventario> TipoInventario { get; set; }
        //public DbSet<Transaccion> Transaccion { get; set; }   

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Conventions.Remove<PluralizingTableNameConvention;
        //}
    }
}