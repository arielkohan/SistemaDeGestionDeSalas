using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelReservaSalas.modelo
{
    public class Respuesta
    {
        public int RespuestaID { get; set; }

        
        public string respuesta { get; set; }

        public int PreguntaID { get; set; }

        [ForeignKey("PreguntaID")]
        public virtual Pregunta pregunta { get; set; }
        
    }
}