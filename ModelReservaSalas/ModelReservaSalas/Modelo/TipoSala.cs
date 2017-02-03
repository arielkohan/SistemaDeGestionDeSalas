using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ModelReservaSalas.modelo
{
    public class TipoSala
    {
        public static string[] _Tipos = { "Reunion", "Capacitacion", "Auditorio"};
        //TODO: ver como hacerlo si usar enum o qué.
        [Key]
        public int TipoID { get; set; }
        [Required]
        [StringLength(25)]
        public String Descripcion { get; set; }
    }
}
