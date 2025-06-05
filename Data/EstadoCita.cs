using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class EstadoCita
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EstadoID { get; set; }

        [Required]
        [StringLength(100)]
        public string Descripcion { get; set; }

        // Navigation properties
        public virtual ICollection<Cita> Citas { get; set; }
    }
}