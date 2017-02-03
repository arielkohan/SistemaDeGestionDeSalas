using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ModelReservaSalas.modelo;
using ModelReservaSalas.repositorios;

namespace RepositoriosMock
{
    public class MockRepositorioReserva : IReservaRepository
    {

        public MockRepositorioReserva() { }

        public Reserva create(Reserva sala)
        {
            throw new NotImplementedException();
        }

        public void delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Reserva> findAll()
        {
            throw new NotImplementedException();
        }

        public Reserva findById(int id)
        {
            Reserva reserva = new Reserva();
            reserva.Almuerzo = true;
            reserva.CantidadPersonas = 5;
            reserva.Encuesta = null;
            reserva.FechaInicio = new DateTime();
            reserva.FechaFin = new DateTime();
            reserva.Motivo = "Charlar.";
            reserva.Proyector = false;
            reserva.Responsable = new Empleado();
            reserva.Sala = new Sala();
            reserva.Servicio = true;
            return reserva;
        }

        public Reserva update(Reserva sala)
        {
            throw new NotImplementedException();
        }
    }
}