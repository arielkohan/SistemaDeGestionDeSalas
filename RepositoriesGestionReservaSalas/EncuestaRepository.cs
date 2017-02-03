using ModelReservaSalas.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelReservaSalas.modelo;

namespace RepositoriesGestionReservaSalas
{
    public class EncuestaRepository : IEncuestaRepository
    {
        public void create(Encuesta Encuesta)
        {
            using (var ctx = new GestionReservasContext())
            {
                ctx.Encuestas.Add(Encuesta);
                ctx.SaveChanges();
            }
        }

        public void delete(int id)
        {
            using (var ctx = new GestionReservasContext())
            {
                Encuesta encuesta = ctx.Encuestas.Find(id);
                if (encuesta == null)
                    throw new KeyNotFoundException();
                ctx.Encuestas.Remove(encuesta);
                ctx.SaveChanges();
            }
        }

        public IEnumerable<Encuesta> findAll()
        {
            using (var ctx = new GestionReservasContext())
            {
                return ctx.Encuestas.ToList();
            }
        }

        public Encuesta findById(int id)
        {
            using (var ctx = new GestionReservasContext())
            {
                Encuesta encuesta= ctx.Encuestas.Find(id);
                if (encuesta == null)
                    throw new KeyNotFoundException();
                return encuesta;
            }
        }

        public List<Pregunta> getPreguntasDesarrollo()
        {
            using (var ctx = new GestionReservasContext())
            {
                return ctx.PreguntasDesarrollo.ToList<Pregunta>();
            }
        }

        public List<Pregunta> getPreguntasCheckbox()
        {
            using (var ctx = new GestionReservasContext())
            {
                IQueryable<Pregunta> preguntas = ctx.PreguntasCheckbox.Include("Opciones");
                //foreach (Pregunta p in preguntas)
                //{
                //    if(p.esCheckbox())
                //        ctx.Entry(p).Collection(preg => ((PreguntaCheckbox)preg).Opciones).Load();
                //}
                return preguntas.ToList();
            }
        }

        public void update(Encuesta Encuesta)
        {
            throw new NotImplementedException();
        }

        
    }
}
