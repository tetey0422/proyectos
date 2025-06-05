using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Reprogramacion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReprogramacionID { get; set; }

        public int CitaID { get; set; }

        public DateTime NuevaFecha { get; set; }

        [StringLength(500)]
        public string Motivo { get; set; }

        [StringLength(20)]
        public string Estado { get; set; }

        // Navigation properties
        [ForeignKey("CitaID")]
        public virtual Cita Cita { get; set; }
    }
}