using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelReservaSalas.modelo;
using ModelReservaSalas.repositorios;

namespace RepositoriosMock
{
    public class MockRepositorioEmpleado : IEmpleadoRepositorio
    {
        public void create(Empleado empleado)
        {
            throw new NotImplementedException();
        }

        public void delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Empleado> findAll()
        {
            throw new NotImplementedException();
        }

        public Empleado findById(int id)
        {
            throw new NotImplementedException();
        }

        public void update(Empleado empleado)
        {
            throw new NotImplementedException();
        }
    }
}
