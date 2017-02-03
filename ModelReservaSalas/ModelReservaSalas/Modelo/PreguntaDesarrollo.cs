using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelReservaSalas.modelo;


namespace ModelReservaSalas.modelo
{
    public class PreguntaDesarrollo : Pregunta
    {

        // public String Respuesta { get; set; }
        public PreguntaDesarrollo()
        {
            this.TipoPregunta = Pregunta._DESARROLLO;
        }

        public PreguntaDesarrollo(string enunciado, bool required) : base(enunciado, required)
        {
            this.TipoPregunta = Pregunta._DESARROLLO;
        }

        public override bool esCheckbox()
        {
            return false;
        }
    }
}
