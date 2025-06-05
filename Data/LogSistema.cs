using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class LogSistema
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LogID { get; set; }

        public int UsuarioID { get; set; }

        [Required]
        [StringLength(500)]
        public string Accion { get; set; }

        public DateTime FechaHora { get; set; }

        [StringLength(20)]
        public string Estado { get; set; }

        // Navigation properties
        [ForeignKey("UsuarioID")]
        public virtual Usuario Usuario { get; set; }
    }
}