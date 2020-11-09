using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Eventos.WebAPI.Models
{
    [Table("Usuarios")]
    public class Usuario
    {
        
        [StringLength(50)]
        [Required]
        public string Nome { get; set; }


        [Key]
        [Required]
        public string CPF { get; set; }

    }
}
