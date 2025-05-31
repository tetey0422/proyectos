using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Usuarios
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Documento { get; set; }

        public bool Activo { get; set; }

        public string Celular { get; set; }

        public string Ciudad { get; set; }

        public string Correo { get; set; }

        public string Direccion { get; set; }

        public int IdTipoUsuario { get; set; }
        
        [ForeignKey("IdTipoUsuario")]
        public virtual TipoUsuario Usuario { get; set; }

        public virtual ICollection<AgendaCitas> CitasUsuario { get; set; }

    }
}
