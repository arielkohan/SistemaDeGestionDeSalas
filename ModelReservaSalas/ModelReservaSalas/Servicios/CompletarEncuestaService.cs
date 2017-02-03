using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelReservaSalas.modelo;
using ModelReservaSalas.repositorios;
using ModelReservaSalas.Repositorios;

namespace ModelReservaSalas.Servicios
{
    public class CompletarEncuestaService
    {
        private IEmpleadoRepositorio EmpleadoRepository;
        private IEncuestaRepository EncuestaRepository;
        private IReservaRepository ReservaRepository;

        public CompletarEncuestaService(IEmpleadoRepositorio empRep,IReservaRepository resRep, IEncuestaRepository encRep)
        {
            this.EmpleadoRepository = empRep;
            this.ReservaRepository = resRep;
            this.EncuestaRepository = encRep;
        }

        public void completar(Encuesta encuesta, int reservaID, int empleadoID)
        {
            Empleado e = EmpleadoRepository.findById(empleadoID);
            if (e == null)
                throw new ApplicationException("No existe el empleado para el usuario de la sesión.");
            Reserva reserva = ReservaRepository.findById(reservaID);
            if (reserva == null)
                throw new ArgumentException("No existe reserva con el ID que fue pasado en la URL");
            if (reserva.ResponsableID != empleadoID)
                throw new ArgumentException("No coincide el empleado de la sesión con el responsable de la reserva");

            validarRespuestasRequeridas(encuesta.Respuestas);

            reserva.Encuesta = encuesta;
            ReservaRepository.update(reserva);
        }

        private void validarRespuestasRequeridas(List<Respuesta> respuestas)
        {
            IEnumerable<Pregunta> preguntas = EncuestaRepository.getPreguntasCheckbox().Concat(EncuestaRepository.getPreguntasDesarrollo());
            foreach(Respuesta r in respuestas)
            {
                foreach(Pregunta p in preguntas)
                {
                    if (r.PreguntaID == p.PreguntaID && p.Required && (r.respuesta == null || r.respuesta.Length == 0))
                        throw new ArgumentException("No se respondió una pregunta que debía ser respondida.");
                }
            }
        }

        //OPERACIONES



    }
}
