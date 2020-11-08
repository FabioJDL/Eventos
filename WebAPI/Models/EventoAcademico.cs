using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Eventos.WebAPI.Models
{
    [Table("EventosAcademicos")]
    public class EventoAcademico
    {
        [Key]
        [Required]
        public int Id { get; set; }


        [StringLength(80)]
        [Required]
        public string Titulo { get; set; }


        [StringLength(200)]
        public string Descricao { get; set; }


        [StringLength(80)]
        [Required]
        public string Local { get; set; }


        [Required]
        public DateTime DataHorario { get; set; }


        [Required]
        public int Capacidade { get; set; }
    }
}
