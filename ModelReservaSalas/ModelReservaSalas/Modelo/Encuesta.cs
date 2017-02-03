using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelReservaSalas.modelo
{
    public class Encuesta
    {
        public int EncuestaId { get; set; }

        //public List<Pregunta> Preguntas{ get; set; } 
        public List<Respuesta> Respuestas { get; set; }
    }
}
