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
    public class TransaccionsController : ApiController
    {
        private BaseDatosInventarioEntities1 db = new BaseDatosInventarioEntities1();

        // GET: api/Transaccions
        public List<Transaccion> GetTransaccions()
        {
            List<Transaccion> transaccion = db.Transaccions.Include(x => x.Almacen)
                .Include(x => x.Articulo)
                .Where(x => x.Estado.Equals("1"))
                .ToList();
            return transaccion;
        }

        // GET: api/Transaccions/5
        [ResponseType(typeof(Transaccion))]
        public IHttpActionResult GetTransaccion(int id)
        {
            Transaccion transaccion = db.Transaccions.Find(id);
            if (transaccion == null)
            {
                return NotFound();
            }

            return Ok(transaccion);
        }

        // PUT: api/Transaccions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTransaccion(int id, Transaccion transaccion)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            if (id != transaccion.Id_Transaccion)
            {
                return BadRequest();
            }

            db.Entry(transaccion).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransaccionExists(id))
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

        // POST: api/Transaccions
        [ResponseType(typeof(Transaccion))]
        public IHttpActionResult PostTransaccion(Transaccion transaccion)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            db.Transaccions.Add(transaccion);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = transaccion.Id_Transaccion }, transaccion);
        }

        // DELETE: api/Transaccions/5
        [ResponseType(typeof(Transaccion))]
        public IHttpActionResult DeleteTransaccion(int id)
        {
            Transaccion transaccion = db.Transaccions.Find(id);
            if (transaccion == null)
            {
                return NotFound();
            }

            /*FUNCION DELETE*/
            transaccion.Estado = "0";
            db.Entry(transaccion).State = EntityState.Modified;
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

        private bool TransaccionExists(int id)
        {
            return db.Transaccions.Count(e => e.Id_Transaccion == id) > 0;
        }
    }
}