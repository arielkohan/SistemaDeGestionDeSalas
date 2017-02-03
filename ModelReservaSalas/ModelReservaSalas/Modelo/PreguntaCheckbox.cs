using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelReservaSalas.modelo;
using System.ComponentModel.DataAnnotations;

namespace ModelReservaSalas.modelo
{
    public class PreguntaCheckbox : Pregunta
    {
        [Required]
        public virtual List<Opcion> Opciones { get; set; }
        //public int OpcionEscogida { get; set; }

        public PreguntaCheckbox() {
            this.TipoPregunta = Pregunta._CHECKBOX;
        }

        public PreguntaCheckbox(string enunciado, List<Opcion> opciones, bool required) : base(enunciado, required)
        {
            this.TipoPregunta = Pregunta._CHECKBOX;
            this.Opciones = opciones;
        }

        public override bool esCheckbox() { return true; }
    }
}
