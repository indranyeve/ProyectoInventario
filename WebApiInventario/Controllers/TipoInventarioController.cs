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
    public class TipoInventarioController : ApiController
    {
        private BaseDatosInventarioEntities3 db = new BaseDatosInventarioEntities3();

        // GET: api/TipoInventario
        public IQueryable<TipoInventario> GetTipoInventarios()
        {
            return db.TipoInventarios.Where(x => x.Estado.Equals("1"));
        }

        // GET: api/TipoInventario/5
        [ResponseType(typeof(TipoInventario))]
        public IHttpActionResult GetTipoInventario(int id)
        {
            TipoInventario tipoInventario = db.TipoInventarios.Find(id);
            if (tipoInventario == null)
            {
                return NotFound();
            }

            return Ok(tipoInventario);
        }

        // PUT: api/TipoInventario/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTipoInventario(int id, TipoInventario tipoInventario)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            if (id != tipoInventario.Id_TipoInventario)
            {
                return BadRequest();
            }

            db.Entry(tipoInventario).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoInventarioExists(id))
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

        // POST: api/TipoInventario
        [ResponseType(typeof(TipoInventario))]
        public IHttpActionResult PostTipoInventario(TipoInventario tipoInventario)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            db.TipoInventarios.Add(tipoInventario);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tipoInventario.Id_TipoInventario }, tipoInventario);
        }

        // DELETE: api/TipoInventario/5
        [ResponseType(typeof(TipoInventario))]
        public IHttpActionResult DeleteTipoInventario(int id)
        {
            TipoInventario tipoInventario = db.TipoInventarios.Find(id);
            if (tipoInventario == null)
            {
                return NotFound();
            }

            /*FUNCION DELETE*/
            tipoInventario.Estado = "0";
            db.Entry(tipoInventario).State = EntityState.Modified;
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

        private bool TipoInventarioExists(int id)
        {
            return db.TipoInventarios.Count(e => e.Id_TipoInventario == id) > 0;
        }
    }
}