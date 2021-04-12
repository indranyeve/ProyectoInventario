﻿using Inventario.Client.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Inventario.Client
{
    public class InventarioDbContext : DbContext
    {
        public InventarioDbContext() : base("name=DefaultConnection")
        {

        }

        public DbSet<mvcAlmacenModel> Almacen { get; set; }
        public DbSet<MvcArticuloModel> Articulo { get; set; }
        //public DbSet<AsientosContables> AsientosContables { get; set; }
        public DbSet<TipoInventario> TipoInventario { get; set; }
        public DbSet<MvcTransaccionModel> Transaccion { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Conventions.Remove<PluralizingTableNameConvention;
        //}
    }
}