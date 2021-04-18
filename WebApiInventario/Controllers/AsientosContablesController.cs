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
    public class AsientosContablesController : ApiController
    {
        private BaseDatosInventarioEntities4 db = new BaseDatosInventarioEntities4();

        // GET: api/AsientosContables
        public IQueryable<AsientosContable> GetAsientosContables()
        {
            
            try
            {
                return db.AsientosContables;
            }
            catch (Exception e)
            {
                
                throw e;
            }
        }

        // GET: api/AsientosContables/5
        [ResponseType(typeof(AsientosContable))]
        public IHttpActionResult GetAsientosContable(int id)
        {
            AsientosContable asientosContable = db.AsientosContables.Find(id);
            if (asientosContable == null)
            {
                return NotFound();
            }

            return Ok(asientosContable);
        }

        // PUT: api/AsientosContables/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAsientosContable(int id, AsientosContable asientosContable)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            if (id != asientosContable.Id_AsientosContables)
            {
                return BadRequest();
            }

            db.Entry(asientosContable).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AsientosContableExists(id))
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

        // POST: api/AsientosContables
        [ResponseType(typeof(AsientosContable))]
        public IHttpActionResult PostAsientosContable(AsientosContable asientosContable)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //asientosContable.Fecha_Asiento = asientosContable.Fecha_Asiento == DateTime.MinValue ? null : asientosContable.Fecha_Asiento;

            AccountingService service = new AccountingService();
            var dataToSend = new AccountingParameters
            {
                idAuxiliarSystem = 4,
                account = asientosContable.cuentaContable,
                seatAmount = asientosContable.Monto_Asiento,
                description = asientosContable.descripcion,
                entreyDate = asientosContable.Fecha_Asiento,
                movementType = asientosContable.Tipo_de_Movimiento,
                state = true
            };

            var response = service.Contabilizar(dataToSend);
            asientosContable.AsientoContabilidad_ID = response.id;

            db.AsientosContables.Add(asientosContable);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = asientosContable.Id_AsientosContables }, asientosContable);
        }

        // DELETE: api/AsientosContables/5
        [ResponseType(typeof(AsientosContable))]
        public IHttpActionResult DeleteAsientosContable(int id)
        {
            AsientosContable asientosContable = db.AsientosContables.Find(id);
            if (asientosContable == null)
            {
                return NotFound();
            }

            //db.AsientosContables.Remove(asientosContable);
            //db.SaveChanges();

            //return Ok(asientosContable);

            /*FUNCION DELETE*/
            asientosContable.Estado = "0";
            db.Entry(asientosContable).State = EntityState.Modified;
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

        private bool AsientosContableExists(int id)
        {
            return db.AsientosContables.Count(e => e.Id_AsientosContables == id) > 0;
        }
    }
}