using System.ComponentModel.DataAnnotations;

namespace ModelReservaSalas.modelo
{
    public class Opcion
    {
        public int OpcionID { get; set; }

        [Required]
        public string Descripcion { get; set; }

        public Opcion() { }
        public Opcion(string descripcion)
        {
            this.Descripcion = descripcion;
        }
    }
}