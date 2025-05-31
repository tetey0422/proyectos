using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class AgendaCitas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime Fecha { get; set; }

        public string PacienteDocumento { get; set; }

        public string DoctorDocumento { get; set; }

        public string Consultorio { get; set; }

        [ForeignKey("PacienteDocumento")]
        public virtual  Usuarios CitasPaciente {  get; set; }

        [ForeignKey("DoctorDocumento")]
        public virtual Usuarios CitasDoctor { get; set; }
    }
}
