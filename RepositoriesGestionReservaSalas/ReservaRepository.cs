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
    public class ReservaRepository : IReservaRepository
    {
        public Reserva create(Reserva reserva)
        {
            using (var ctx = new GestionReservasContext())
            {
                ctx.Reservas.Add(reserva);
                ctx.SaveChanges();
                return reserva;
            }
        }

        public void delete(int id)
        {
            using (var ctx = new GestionReservasContext())
            {
                Reserva reserva = ctx.Reservas.Find(id);
                if (reserva == null)
                    throw new KeyNotFoundException();
                ctx.Reservas.Remove(reserva);
                ctx.SaveChanges();
            }
        }

        public IEnumerable<Reserva> findAll()
        {
            using (var ctx = new GestionReservasContext())
            {
                //EAGER-LOADING OF REPSONSABLE INSTEAD OF LAZY-LOADING
                IQueryable<Reserva> reservas = ctx.Reservas.Include((reserva)=>reserva.Sala).Include(r => r.Responsable).Include(r => r.Encuesta).Include(r => r.Encuesta.Respuestas.Select(resp => resp.pregunta));
                //foreach(Reserva r in reservas)
                //{
                //    if(r.Encuesta != null)
                //        foreach(Respuesta respuesta in r.Encuesta.Respuestas)
                //        {
                //            if (respuesta.pregunta.TipoPregunta == Pregunta._CHECKBOX)
                //                //ctx.Entry(respuesta).Reference(resp => ((PreguntaCheckbox)resp.pregunta).Opciones).Load();
                //                ctx.Entry(respuesta).Collection(resp => ((PreguntaCheckbox)resp.pregunta).Opciones).Query().Load();
                //                //reservas.Include(res => res.Encuesta.Respuestas.Select(resp => ((PreguntaCheckbox)resp.pregunta).Opciones));
                //        }
                //}
                    
                return reservas.ToList();
            }
        }

        public Reserva findById(int id)
        {
            using (var ctx = new GestionReservasContext())
            {
                Reserva reserva = ctx.Reservas.Find(id);
                if (reserva == null)
                    throw new KeyNotFoundException();
                //EAGER-LOADING OF REPSONSABLE INSTEAD OF LAZY-LOADING
                ctx.Entry(reserva).Reference(r => r.Responsable).Load();
                ctx.Entry(reserva).Reference(r => r.Sala).Load();
                return reserva;
            }
        }

        public Reserva update(Reserva reserva)
        {
            using(var ctx = new GestionReservasContext())
            {
                var r = ctx.Reservas.Find(reserva.ReservaID);
                if (r != null)
                    ctx.Reservas.Attach(r);
                else
                    throw new ApplicationException();

                ctx.Reservas.Attach(r);

                if (r.Encuesta != null)
                    ctx.Encuestas.Attach(r.Encuesta);
                r.Encuesta = reserva.Encuesta;


                r.SalaID = reserva.SalaID;

                r.transferSimpleDataFrom(reserva);

                ctx.Entry(r).State = EntityState.Modified;
                ctx.SaveChanges();
                return reserva;
            }
        }



    }
}
