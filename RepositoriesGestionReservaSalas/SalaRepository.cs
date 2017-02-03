using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelReservaSalas.repositorios;
using ModelReservaSalas.modelo;
using System.Data.Entity;

namespace RepositoriesGestionReservaSalas
{
    public class SalaRepository : ISalaRepositorio
    {
        public Sala create(Sala sala)
        {
            using (var ctx = new GestionReservasContext())
            {
                ctx.Salas.Add(sala);
                ctx.SaveChanges();
                return sala;
            }
        }

        public void delete(int id)
        {
            using (var ctx = new GestionReservasContext())
            {
                Sala sala = ctx.Salas.Find(id);
                if (sala == null)
                    throw new KeyNotFoundException();
                ctx.Salas.Remove(sala);
                ctx.SaveChanges();
            }
        }

        public IEnumerable<Sala> findAll()
        {
            using (var ctx = new GestionReservasContext())
            {

                //EAGER-LOADING OF TIPOSALA INSTEAD OF LAZY-LOADING
                IQueryable<Sala> salas = ctx.Salas.Include(sala => sala.TipoSala);

                return salas.ToList();
                
                //return ctx.Salas.ToList();
            }
        }

        public Sala findById(int id)
        {
            using (var ctx = new GestionReservasContext())
            {
                Sala sala = ctx.Salas.Find(id);
                if (sala== null)
                    throw new KeyNotFoundException();
                ctx.Entry(sala).Reference(s => s.TipoSala).Load();
                return sala;
            }
        }

        public Sala update(Sala sala)
        {
            using (var ctx = new GestionReservasContext())
            {
                ctx.Salas.Attach(sala);
                ctx.Entry(sala).State = EntityState.Modified;
                ctx.SaveChanges();
                return sala;
            }
        }


        public IEnumerable<TipoSala> getTipos()
        {
            using (var ctx = new GestionReservasContext())
            {
                return ctx.TiposSalas.ToList();
            }
        }

        public TipoSala getTipoByID(int id)
        {
            using (var ctx = new GestionReservasContext())
            {
                return ctx.TiposSalas.Find(id);
            }
        }



    }
}
