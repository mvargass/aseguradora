using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Aseguradora.Models
{ 
    [Table("seguro")]
    public class Seguro
    {
        [Key]
        public int id { get; set; }
        public int poliza { get; set; }
        public DateTime inicio_cobertura { get; set; }
        public decimal monto_cobertura { get; set; }
        public decimal monto_deducible { get; set; }
        public int asegurado_id { get; set; }
        public int proveedor_id { get; set; }
    }

    [Table("proveedor")]
    public class Proveedor
    {
        [Key]
        public int id { get; set; }
        [StringLength(15, ErrorMessage = "The field {0} can contain maximun {1} and minimum {2} characters", MinimumLength = 7)]
        public string nit { get; set; }
        [StringLength(100, ErrorMessage = "The field {0} can contain maximun {1} and minimum {2} characters", MinimumLength = 7)]
        public string razon_social { get; set; }
    }
    [NotMapped]
    public class Respuesta
    {
        public string codigo { get; set; }
        public string mensaje { get; set; }
        public decimal monto_deducible { get; set; }
    }
}