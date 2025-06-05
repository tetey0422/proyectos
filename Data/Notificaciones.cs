using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Notificacion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NotificacionID { get; set; }

        public int UsuarioID { get; set; }

        [Required]
        [StringLength(1000)]
        public string Mensaje { get; set; }

        public bool Leida { get; set; }

        public DateTime Fecha { get; set; }

        [StringLength(20)]
        public string Estado { get; set; }

        // Navigation properties
        [ForeignKey("UsuarioID")]
        public virtual Usuario Usuario { get; set; }
    }
}