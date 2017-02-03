using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelReservaSalas.repositorios;
using ModelReservaSalas.modelo;

namespace RepositoriosMock
{
    public class MockRepositorioSala : ISalaRepositorio
    {
        public Sala create(Sala sala)
        {
            throw new NotImplementedException();
        }

        public void delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Sala> findAll()
        {
            throw new NotImplementedException();
        }

        public Sala findById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TipoSala> getTipos()
        {
            throw new NotImplementedException();
        }

        public Sala update(Sala sala)
        {
            throw new NotImplementedException();
        }
    }
}
