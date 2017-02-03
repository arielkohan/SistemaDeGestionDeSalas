using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelReservaSalas.modelo;

namespace ModelReservaSalas.repositorios
{
    public interface IReservaRepository
    {

        Reserva findById(int id);
        IEnumerable<Reserva> findAll();
        Reserva create(Reserva  reserva);
        void delete(int id);
        Reserva update(Reserva reserva);
    }
}
