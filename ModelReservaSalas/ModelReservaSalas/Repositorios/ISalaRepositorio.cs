using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelReservaSalas.modelo;

namespace ModelReservaSalas.repositorios
{
    public interface ISalaRepositorio
    {
        Sala findById(int id);
        IEnumerable<Sala> findAll();
        Sala create(Sala sala);
        void delete(int id);
        Sala update(Sala sala);
        IEnumerable<TipoSala> getTipos();
        TipoSala getTipoByID(int tipoSalaID);
    }
}
