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

namespace Aseguradora.Controllers
{
    public class SegurosController : ApiController
    {
        private SeguroContext db = new SeguroContext();

        [HttpPost]
        [Route("api/seguros/consultar")]
        public IHttpActionResult Consultar(JObject json)
        {
            long codigo = 0;
            string fechaNacimiento;
            dynamic jsonObject = json;
            try
            {
                codigo = jsonObject.codigo.Value;
                fechaNacimiento = jsonObject.fecha_nacimiento.Value;
            }
            catch
            {
                return this.BadRequest("Incorrect call");
            }
            var seguro = db.seguro.Where(u => u.poliza == codigo).FirstOrDefault();
            if (seguro == null)
            {
                return BadRequest("No hay ningun Asegurado con este codigo");
            }

            var asegurado = db.asegurado.Where(u => u.id == seguro.asegurado_id).FirstOrDefault();

            if (asegurado == null || !asegurado.fecha_nacimiento.ToString().Contains(fechaNacimiento))
            {
                return BadRequest("No hay ningun Asegurado con esta fecha de nacimiento");
            }
            return Ok(seguro);
        }

        [HttpPost]
        [Route("api/seguros/consultar_proveedor")]
        public IHttpActionResult ConsultarProveedor(JObject json)
        {
            string nit = string.Empty;
            long codigoAfiliado = 0;
            string fechaNacimiento = string.Empty;
            string fechaCobertura = string.Empty;
            dynamic jsonObject = json;
            try
            {
                nit = jsonObject.nit_proveedor.Value;
                codigoAfiliado = jsonObject.codigo.Value;
                fechaNacimiento = jsonObject.fecha_nacimiento.Value;
                fechaCobertura = jsonObject.fecha_cobertura.Value;
            }
            catch
            {
                return this.BadRequest("Incorrect call");
            }
            var bitacora = new Bitacora
            {
                codigo_paciente = Convert.ToInt32(codigoAfiliado),
                fecha_cobertura = DateTime.Parse(fechaCobertura),
                fecha_consulta = DateTime.Now,
                nit = nit                
            };
            var proveedor = db.proveedor.Where(u => u.nit.Equals(nit)).FirstOrDefault();
            if(proveedor == null)
            {
                bitacora.respuesta = "No Existe ningun proveedor con este nit";
                db.bitacora.Add(bitacora);
                db.SaveChanges();
                return BadRequest("No Existe ningun proveedor con este nit");
            }
            var seguro = db.seguro.Where(u => u.proveedor_id == proveedor.id && u.poliza == codigoAfiliado).FirstOrDefault();
            if (seguro == null)
            {
                bitacora.respuesta = "No hay ningun Asegurado con este codigo";
                db.bitacora.Add(bitacora);
                db.SaveChanges();
                return BadRequest("No hay ningun Asegurado con este codigo");
            }

            int vigente = seguro.inicio_cobertura.CompareTo(DateTime.Parse(fechaCobertura));

            var asegurado = db.asegurado.Where(u => u.id == seguro.asegurado_id).FirstOrDefault();

            if (asegurado == null || !asegurado.fecha_nacimiento.ToString().Contains(fechaNacimiento))
            {
                bitacora.respuesta = "No hay ningun Asegurado con esta fecha de nacimiento";
                db.bitacora.Add(bitacora);
                db.SaveChanges();
                return BadRequest("No hay ningun Asegurado con esta fecha de nacimiento");
            }
            
            if(vigente <= 0)
            {
                return BadRequest("Sin Cobertura");
            }

            var g = Guid.NewGuid();
            var respuesta = new Respuesta
            {
                codigo = g.ToString(),
                mensaje = "Con Cobertura",
                monto_deducible = seguro.monto_deducible
            };
            return Ok(respuesta);
        }

        // GET: api/Seguros
        public IQueryable<Seguro> GetSeguro()
        {
            return db.seguro;
        }

        // GET: api/Seguros/5
        [ResponseType(typeof(Seguro))]
        public IHttpActionResult GetSeguro(int id)
        {
            Seguro seguro = db.seguro.Find(id);
            if (seguro == null)
            {
                return NotFound();
            }

            return Ok(seguro);
        }

        // PUT: api/Seguros/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSeguro(int id, Seguro seguro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != seguro.id)
            {
                return BadRequest();
            }

            db.Entry(seguro).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeguroExists(id))
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

        // POST: api/Seguros
        [ResponseType(typeof(Seguro))]
        public IHttpActionResult PostSeguro(Seguro seguro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.seguro.Add(seguro);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = seguro.id }, seguro);
        }

        // DELETE: api/Seguros/5
        [ResponseType(typeof(Seguro))]
        public IHttpActionResult DeleteSeguro(int id)
        {
            Seguro seguro = db.seguro.Find(id);
            if (seguro == null)
            {
                return NotFound();
            }

            db.seguro.Remove(seguro);
            db.SaveChanges();

            return Ok(seguro);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SeguroExists(int id)
        {
            return db.seguro.Count(e => e.id == id) > 0;
        }
    }
}