using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelReservaSalas.modelo
{
    public class Sala
    {
        public int SalaID { get; set; }

        [Required]
        public String Nombre { get; set; }
        [Required]
        public String Ubicacion { get; set; }

        [Required]
        [Range(1,int.MaxValue,ErrorMessage ="La capacidad debe ser un valor mayor a cero.")]
        public int Capacidad { get; set; }

        [Required]
        public int TipoSalaID { get; set; }
        [ForeignKey("TipoSalaID")]
        public virtual TipoSala TipoSala{ get; set; }
    }
}
