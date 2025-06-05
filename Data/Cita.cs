using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Cita
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CitaID { get; set; }

        public int PacienteID { get; set; }

        public int ExamenID { get; set; }

        public DateTime FechaHora { get; set; }

        public int EstadoID { get; set; }

        [StringLength(20)]
        public string Estado { get; set; }

        // Navigation properties
        [ForeignKey("PacienteID")]
        public virtual Paciente Paciente { get; set; }

        [ForeignKey("ExamenID")]
        public virtual Examen Examen { get; set; }

        [ForeignKey("EstadoID")]
        public virtual EstadoCita EstadoCita { get; set; }

        public virtual ICollection<Reprogramacion> Reprogramaciones { get; set; }
        public virtual ICollection<Resultado> Resultados { get; set; }
        public virtual ICollection<HistorialCita> HistorialCitas { get; set; }
    }
}