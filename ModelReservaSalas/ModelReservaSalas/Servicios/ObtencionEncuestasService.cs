using ModelReservaSalas.repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelReservaSalas.modelo;
using ModelReservaSalas.Repositorios;

namespace ModelReservaSalas.Servicios
{
    public class ObtencionEncuestasService
    {
        private IEmpleadoRepositorio EmpleadoRepository;
        private IReservaRepository ReservaRepository;
        private IEncuestaRepository EncuestaRepository;

        public ObtencionEncuestasService(IEmpleadoRepositorio empleadoRepository, IReservaRepository reservaRepository, IEncuestaRepository encuestaRepository)
        {
            this.EmpleadoRepository = empleadoRepository;
            this.ReservaRepository = reservaRepository;
            this.EncuestaRepository = encuestaRepository;
        }

        public IEnumerable<Encuesta> getEncuestasFromEmpleado(int empleadoID)
        {
            empleadoExiste(empleadoID);

            IEnumerable<Reserva> reservasDelEmpleado = ReservaRepository.findAll().Where(r => r.ResponsableID == empleadoID);

            List<Encuesta> result = new List<Encuesta>();
            foreach (Reserva r in reservasDelEmpleado)
            {
                //if (r.EncuestaID != null)
                //    result.Add(EncuestaRepository.findById(r.EncuestaID.Value));
                if(r.Encuesta != null)
                    result.Add(r.Encuesta);
            }

            return result;
        }

        

        public Encuesta getOneEncuestaFromEmpleado(int empleadoID, int encuestaID)
        {
            empleadoExiste(empleadoID);

            //IEnumerable<Reserva> reservas = ReservaRepository.findAll().Where(r => r.EncuestaID == encuestaID && r.ResponsableID == empleadoID);
            //if (reservas.ToList().Count() <= 0)
            //    throw new ArgumentException("No existe la encuesta con el ID buscado");
            //return EncuestaRepository.findById(encuestaID);

            IEnumerable<Reserva> reservas = ReservaRepository.findAll().Where(r => r.ResponsableID == empleadoID);
            foreach(Reserva r in reservas)
            {
                if (r.Encuesta != null && r.Encuesta.EncuestaId == encuestaID)
                    return r.Encuesta;
            }
            throw new ArgumentException("No existe la encuesta con el ID buscado");
        }

        public void getReservasAResponderYRespondidas(int empleadoID, out IEnumerable<Reserva> reservasParaResponder, out IEnumerable<Reserva> reservasRespondidas)
        {
            reservasParaResponder = new List<Reserva>();
            reservasRespondidas= new List<Reserva>();

            empleadoExiste(empleadoID);
            IEnumerable<Reserva> reservasDelEmpleado = ReservaRepository.findAll().Where(res => res.ResponsableID == empleadoID);
            if (reservasDelEmpleado == null || reservasDelEmpleado.ToList().Count == 0)
                return ;

            reservasParaResponder = reservasDelEmpleado.Where(r => r.Encuesta == null);
            reservasRespondidas = reservasDelEmpleado.Where(r => r.Encuesta != null);
        }

        public IEnumerable<Pregunta> getPreguntas()
        {
            
            
            List<Pregunta> preguntas = EncuestaRepository.getPreguntasCheckbox();
            foreach (Pregunta p in EncuestaRepository.getPreguntasDesarrollo())
            {
                preguntas.Add(p);
            }
            return preguntas;
        }

        private void empleadoExiste(int empleadoID)
        {
            Empleado e = EmpleadoRepository.findById(empleadoID);
            if (e == null)
                throw new ApplicationException("No se encontró el empleado autenticado.");
        }


    }
}
