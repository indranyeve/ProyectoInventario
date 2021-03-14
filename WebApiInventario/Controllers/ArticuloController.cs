using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApiInventario.Models;

namespace WebApiInventario.Controllers
{
    public class ArticuloController : ApiController
    {
        private BaseDatosInventarioEntities1 db = new BaseDatosInventarioEntities1();

        // GET: api/Articulo
        public IQueryable<Articulo> GetArticulo()
        {
            return db.Articulo.Where(x => x.Estado.Equals("1"));
        }

        // GET: api/Articulo/5
        [ResponseType(typeof(Articulo))]
        public IHttpActionResult GetArticulo(int id)
        {
            Articulo articulo = db.Articulo.Find(id);
            if (articulo == null)
            {
                return NotFound();
            }

            return Ok(articulo);
        }

        // PUT: api/Articulo/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutArticulo(int id, Articulo articulo)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            if (id != articulo.Id_Articulo)
            {
                return BadRequest();
            }

            db.Entry(articulo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticuloExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Articulo
        [ResponseType(typeof(Articulo))]
        public IHttpActionResult PostArticulo(Articulo articulo)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            db.Articulo.Add(articulo);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = articulo.Id_Articulo }, articulo);
        }

        // DELETE: api/Articulo/5
        [ResponseType(typeof(Articulo))]
        public IHttpActionResult DeleteArticulo(int id)
        {
            Articulo articulo = db.Articulo.Find(id);
            if (articulo == null)
            {
                return NotFound();
            }

            // FUNCION DELETE 
             articulo.Estado = "0";
            db.Entry(articulo).State = EntityState.Modified;
            db.SaveChanges();

            return Ok();

            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ArticuloExists(int id)
        {
            return db.Articulo.Count(e => e.Id_Articulo == id) > 0;
        }
    }
}