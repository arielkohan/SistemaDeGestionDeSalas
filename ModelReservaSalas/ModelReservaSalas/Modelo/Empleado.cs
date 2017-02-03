using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelReservaSalas.modelo
{
    public class Empleado 
    {
        public int EmpleadoID { get; set; }

        [Required]
        public long DNI { get; set; }

        //[Required]
        //[DataType(DataType.Password)]
        //public string Password { get; set; }

        [Required]
        public int Legajo { get; set; }

        [Required]
        public String NombreYApellido { get; set; }
    }
}
