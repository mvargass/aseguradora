using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Aseguradora.Models
{
    public class SeguroContext : DbContext
    {
        public SeguroContext() : base("DefaultConnection")
        {

        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        public DbSet<Asegurado> asegurado { get; set; }
        public DbSet<Seguro> seguro { get; set; }
        public DbSet<Proveedor> proveedor { get; set; }
        public DbSet<Bitacora> bitacora { get; set; }
    }
}