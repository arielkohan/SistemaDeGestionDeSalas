using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelReservaSalas.repositorios;
using ModelReservaSalas.modelo;

namespace RepositoriesGestionReservaSalas
{
    public class EmpleadoRepository : IEmpleadoRepositorio
    {
        public void create(Empleado empleado)
        {
            using(var ctx = new GestionReservasContext())
            {
                ctx.Empleados.Add(empleado);
                ctx.SaveChanges();
            }
        }

        public void delete(int id)
        {
            using (var ctx = new GestionReservasContext())
            {
                Empleado empleado = ctx.Empleados.Find(id);
                if (empleado == null)
                    throw new KeyNotFoundException();
                ctx.Empleados.Remove(empleado);
                ctx.SaveChanges();
            }
        }

        public IEnumerable<Empleado> findAll()
        {
            using (var ctx = new GestionReservasContext())
            {
                return ctx.Empleados.ToList();
            }
        }

        public Empleado findById(int id)
        {
            using (var ctx = new GestionReservasContext())
            {
                Empleado empleado = ctx.Empleados.Find(id);
                if (empleado == null)
                    throw new KeyNotFoundException();
                return empleado;
            }
        }

        public void update(Empleado empleado)
        {
            throw new NotImplementedException();
        }
    }
}
