using ModelReservaSalas.modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelReservaSalas.Repositorios
{
    public interface IEncuestaRepository
    {
        Encuesta findById(int id);
        IEnumerable<Encuesta> findAll();
        void create(Encuesta Encuesta);
        void delete(int id);
        void update(Encuesta Encuesta);
        List<Pregunta> getPreguntasDesarrollo();
        List<Pregunta> getPreguntasCheckbox();
    }
}
