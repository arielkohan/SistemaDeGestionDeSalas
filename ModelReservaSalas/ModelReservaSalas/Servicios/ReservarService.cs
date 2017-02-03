using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelReservaSalas.repositorios;
using ModelReservaSalas.modelo;

namespace ModelReservaSalas.Servicios
{
    public class ReservarService
    {
        private IReservaRepository ReservaRepository ;
        private ISalaRepositorio SalaRepository;
        private IEmpleadoRepositorio EmpleadoRepository;
        private ValidarReservaService ValidarReservaService;

        public ReservarService(IReservaRepository repository)
        {
            this.ReservaRepository = repository;
        }

        public ReservarService(IReservaRepository reservaRepository, ISalaRepositorio salaRepository) 
            : this(reservaRepository)
        {
            this.SalaRepository = salaRepository;
        }

        public ReservarService(IReservaRepository reservaRepository, ISalaRepositorio salaRepository, IEmpleadoRepositorio empleadoRepository)
            : this(reservaRepository, salaRepository)
        {
            this.EmpleadoRepository = empleadoRepository;

            this.ValidarReservaService = new ValidarReservaService(empleadoRepository, salaRepository, reservaRepository);
        }


        public void getReservas(int empleadoID, out IEnumerable<Reserva> reservasDelEmpleado, out IEnumerable<Reserva> reservasDeOtrosEmpleados)
        {
            //Se verifica que el empleado existe.
            Empleado empleado = EmpleadoRepository.findById(empleadoID);
            if (empleado == null)
                throw new Exception("No se encontró el empleado");
            //Se buscan todas las reservas
            IEnumerable<Reserva> reservas = ReservaRepository.findAll();
            //se asigna cada a reserva a la lista que corresponde
            reservasDelEmpleado = new List<Reserva>();
            reservasDeOtrosEmpleados = new List<Reserva>();
            foreach (Reserva r in reservas)
            {
                if(r.FechaFin >= DateTime.Now){
                    if (r.ResponsableID == empleadoID)
                        ((List<Reserva>)reservasDelEmpleado).Add(r);
                    else
                        ((List<Reserva>)reservasDeOtrosEmpleados).Add(r);
                }
            }

        }

        //Operaciones

        public Reserva GetReservaByID(int id)
        {
            return ReservaRepository.findById(id);
        }


        public void editarReserva(Reserva r, int empleadoID)
        {
            Empleado e = EmpleadoRepository.findById(empleadoID);
            Reserva temp = ReservaRepository.findById(r.ReservaID);
            if (e == null)
                throw new ApplicationException("No hay empleado para el usuario logueado"); //Necesario para obtener la informacion faltante (responsable) en el objeto que llega del cliente
            else if (e.EmpleadoID != temp.ResponsableID)
                throw new ApplicationException("El empleado encontrado no coincide con el responsable de la reserva");


            this.ValidarReservaService.validarDatos(r);
            
            ReservaRepository.update(r);

        }

        public Reserva Reservar(Reserva r,int empleadoID)
        {
            Empleado e = EmpleadoRepository.findById(empleadoID);
            if (e == null)
                throw new ApplicationException("No hay empleado para el usuario logueado");
            else
                r.ResponsableID = empleadoID;

            this.ValidarReservaService.validarDatos(r);
            return ReservaRepository.create(r);
        }

        public void eliminarReserva(int reservaID, int empleadoID)
        {
            Empleado e = EmpleadoRepository.findById(empleadoID);
            Reserva r = ReservaRepository.findById(reservaID);
            if (e == null)
                throw new ApplicationException("No hay empleado para el usuario logueado");
            if (r == null)
                throw new ArgumentException("No existe reserva con el ID buscado.");
            if (e.EmpleadoID != r.ResponsableID)
                throw new ApplicationException("El empleado encontrado no coincide con el responsable de la reserva");

            ReservaRepository.delete(reservaID);
        }



        /**
         * La aplicación Cliente podrá usar este servicio para obtener que salas pueden reservarse según algunas propiedades/filtros que se 
         * eligieron previamente.
         */
        public IEnumerable<Sala> getSalasParaReservar(DateTime ingresoSala, DateTime egresoSala, int tipoSalaID, int cantidadPersonas)
        {
            this.ValidarReservaService.validarDatos(ingresoSala, egresoSala, tipoSalaID, cantidadPersonas);
            List<Sala> resultadoSalas = new List<Sala>();
            obtenerSalasDelTipoYCapacidad(ref resultadoSalas, tipoSalaID, cantidadPersonas);
            obtenerSalasLibresEnPeriodo(ref resultadoSalas, ingresoSala, egresoSala);
            return resultadoSalas;
        }


        private void obtenerSalasDelTipoYCapacidad(ref List<Sala> resultadoSalas, int tipoID, int cantidadPersonas)
        {
            resultadoSalas = SalaRepository.findAll().ToList<Sala>();
            if (resultadoSalas == null || resultadoSalas.ToList<Sala>().Count == 0)
                throw new NullReferenceException("No hay salas en la base de datos.");
            resultadoSalas = resultadoSalas.Where(s => s.TipoSalaID == tipoID && s.Capacidad >= cantidadPersonas).ToList<Sala>();
            if (resultadoSalas == null || resultadoSalas.ToList<Sala>().Count == 0)
                throw new NullReferenceException("No hay salas para el tipo y la cantidad de personas requeridas.");
        }

        private void obtenerSalasLibresEnPeriodo(ref List<Sala> resultadoSalas, DateTime ingresoSala, DateTime egresoSala)
        {
            IEnumerable<Reserva> reservasActuales = ReservaRepository.findAll();

            for (int i = resultadoSalas.Count - 1; i >= 0; i--)
            {
                Sala s = resultadoSalas.ElementAt(i);
                if (reservasActuales.Where(reserva =>
                        this.ValidarReservaService.reservaOcupaSalaEnPeriodo(reserva, s, ingresoSala, egresoSala)
                    )
                    .ToList().Count > 0
                )
                {
                    resultadoSalas.RemoveAt(i);
                }
            }
        }


      
    }
}
