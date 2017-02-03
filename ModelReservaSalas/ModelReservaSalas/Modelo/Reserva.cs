using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelReservaSalas.modelo
{
    public class Reserva
    {
        public int ReservaID { get; set; }

        //FechaInicio y FechaFin deben compartir el dia y variar en la hora y min.
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime FechaInicio{ get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime FechaFin{ get; set; }

        [Required]
        public int CantidadPersonas { get; set; }
        [Required]
        public String Motivo { get; set; }
        [Required]
        public Boolean Servicio { get; set; }
        [Required]
        public Boolean Almuerzo { get; set; }
        [Required]
        public Boolean Proyector { get; set; }

        [Required]
        public int SalaID { get; set; }
        [ForeignKey("SalaID")]
        public virtual Sala Sala { get; set; }

        [Required]
        public int ResponsableID { get; set; }
        [ForeignKey("ResponsableID")]
        public virtual Empleado Responsable { get; set; }

        
        //public int? EncuestaID { get; set; }
        
        public virtual Encuesta Encuesta  { get; set; }

        public void transferSimpleDataFrom(Reserva reserva)
        {
            this.Almuerzo = reserva.Almuerzo;
            this.CantidadPersonas = reserva.CantidadPersonas;
            this.FechaFin = reserva.FechaFin;
            this.FechaInicio = reserva.FechaInicio;
            this.Motivo = reserva.Motivo;
            this.Proyector = reserva.Proyector;
            this.Servicio = reserva.Servicio;
        }
    }
}
