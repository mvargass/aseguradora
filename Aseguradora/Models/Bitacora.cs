using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Aseguradora.Models
{
    [Table("bitacora_consulta")]
    public class Bitacora
    {
        [Key]
        public int id { get; set; }
        [StringLength(15, ErrorMessage = "The field {0} can contain maximun {1} and minimum {2} characters", MinimumLength = 7)]
        public string nit { get; set; }
        public int codigo_paciente { get; set; }
        public DateTime fecha_cobertura { get; set; }
        public DateTime fecha_consulta { get; set; }
        [StringLength(50, ErrorMessage = "The field {0} can contain maximun {1} and minimum {2} characters", MinimumLength = 7)]
        public string respuesta { get; set; }
        
    }
}