using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Afiliacion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AfiliacionID { get; set; }

        [Required]
        [StringLength(50)]
        public string TipoDocumento { get; set; }

        [Required]
        [StringLength(20)]
        public string Documento { get; set; }

        public int EPSID { get; set; }

        [StringLength(20)]
        public string Estado { get; set; }

        // Navigation properties
        [ForeignKey("EPSID")]
        public virtual EPS EPS { get; set; }
    }
}