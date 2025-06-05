using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Enfermero
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EnfermeroID { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombres { get; set; }

        [Required]
        [StringLength(100)]
        public string Apellidos { get; set; }

        [StringLength(150)]
        [EmailAddress]
        public string Correo { get; set; }

        [StringLength(20)]
        public string Telefono { get; set; }

        public int UsuarioFK { get; set; }

        [StringLength(20)]
        public string Estado { get; set; }

        // Navigation properties
        [ForeignKey("UsuarioFK")]
        public virtual Usuario Usuario { get; set; }
    }
}