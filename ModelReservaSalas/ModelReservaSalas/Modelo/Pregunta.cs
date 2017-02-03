using System.ComponentModel.DataAnnotations;

namespace ModelReservaSalas.modelo
{
    public abstract class Pregunta
    {
        public static readonly int _DESARROLLO = 1;
        public static readonly int _CHECKBOX = 2;

        public Pregunta(string enunciado, bool required)
        {
            Enunciado = enunciado;
            Required = required;
        }
        public Pregunta() { }

        abstract public bool esCheckbox();

        public int PreguntaID { get; set; }

        [Required]
        public string Enunciado { get; set; }

        public bool Required { get; set; }

        public int TipoPregunta { get; set; }


    }
}