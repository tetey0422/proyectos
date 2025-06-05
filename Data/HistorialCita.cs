using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class HistorialCita
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HistorialID { get; set; }

        public int CitaID { get; set; }

        public int EstadoAnterior { get; set; }

        public int EstadoNuevo { get; set; }

        public DateTime FechaCambio { get; set; }

        // Navigation properties
        [ForeignKey("CitaID")]
        public virtual Cita Cita { get; set; }
    }
}