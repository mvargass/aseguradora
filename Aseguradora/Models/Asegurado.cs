using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Aseguradora.Models
{
    [Table("asegurado")]
    public class Asegurado
    {
        [Key]
        public int id { get; set; }
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(100, ErrorMessage = "The field {0} can contain maximun {1} and minimum {2} characters", MinimumLength = 7)]
        public string nombre { get; set; }
        [Display(Name = "Apellido")]
        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(100, ErrorMessage = "The field {0} can contain maximun {1} and minimum {2} characters", MinimumLength = 7)]
        public string apellido { get; set; }
        [Display(Name = "Telefono")]
        [Required(ErrorMessage = "The field {0} is required")]
        
        public int telefono { get; set; }

        [Display(Name = "Fecha_Nacimiento")]
        [Required(ErrorMessage = "The field {0} is required")]
        
        public DateTime fecha_nacimiento { get; set; }
    }
}