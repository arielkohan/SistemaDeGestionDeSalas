using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelReservaSalas.modelo;

namespace ModelReservaSalas.repositorios
{
   public  interface IEmpleadoRepositorio
    {
        Empleado findById(int id);
        IEnumerable<Empleado> findAll();
        void create(Empleado empleado);
        void delete(int id);
        void update(Empleado empleado);
         
    }
}
