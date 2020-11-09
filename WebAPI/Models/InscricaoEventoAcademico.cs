using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Eventos.WebAPI.Models
{
    [Table("InscricoesEventosAcademicos")]
    public class InscricaoEventoAcademico
    {
        [Key]
        [Required]
        public int Id { get; set; }


        [ForeignKey("EventosAcademicos")]
        [Required]
        public int EventoAcademicoId { get; set; }


        [ForeignKey("Usuarios")]
        [StringLength(11)]
        [Required]
        public string UsuarioCPF { get; set; }


        [DefaultValue(0)]
        public bool ListaDePresenca { get; set; }
    }
}
