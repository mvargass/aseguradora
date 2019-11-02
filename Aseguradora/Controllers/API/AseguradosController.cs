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
using Aseguradora.Models;
using Newtonsoft.Json.Linq;

namespace Aseguradora.Controllers.API
{
    [RoutePrefix("api/asegurados")]
    public class AseguradosController : ApiController
    {
        private SeguroContext db = new SeguroContext();

        

        // GET: api/Aseguradoes
        public IQueryable<Asegurado> GetAsegurado()
        {
            return db.asegurado;
        }

        // GET: api/Aseguradoes/5
        [ResponseType(typeof(Asegurado))]
        public IHttpActionResult GetAsegurado(int id)
        {
            Asegurado asegurado = db.asegurado.Find(id);
            if (asegurado == null)
            {
                return NotFound();
            }

            return Ok(asegurado);
        }

        // PUT: api/Aseguradoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAsegurado(int id, Asegurado asegurado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != asegurado.id)
            {
                return BadRequest();
            }

            db.Entry(asegurado).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AseguradoExists(id))
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

        // POST: api/Aseguradoes
        [ResponseType(typeof(Asegurado))]
        public IHttpActionResult PostAsegurado(Asegurado asegurado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.asegurado.Add(asegurado);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = asegurado.id }, asegurado);
        }    

        // DELETE: api/Aseguradoes/5
        [ResponseType(typeof(Asegurado))]
        public IHttpActionResult DeleteAsegurado(int id)
        {
            Asegurado asegurado = db.asegurado.Find(id);
            if (asegurado == null)
            {
                return NotFound();
            }

            db.asegurado.Remove(asegurado);
            db.SaveChanges();

            return Ok(asegurado);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AseguradoExists(int id)
        {
            return db.asegurado.Count(e => e.id == id) > 0;
        }
    }
}