using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelReservaSalas.modelo;
using ModelReservaSalas.repositorios;

namespace ModelReservaSalas.Servicios
{
    public class ValidarReservaService
    {
        ////////////////////// VALIDACIONES //////////////////////
        private IEmpleadoRepositorio EmpleadoRepository;
        private ISalaRepositorio SalaRepository;
        private IReservaRepository ReservaRepository;

        public ValidarReservaService(IEmpleadoRepositorio empleadoRepositorio, ISalaRepositorio salaRepositorio, IReservaRepository reservaRepositorio)
        {
            this.EmpleadoRepository = empleadoRepositorio;
            this.SalaRepository = salaRepositorio;
            this.ReservaRepository = reservaRepositorio;
        }

        /**
         * Validar que los datos de la reserva son correctos para ser guardados en la base de datos. 
         * 
         */
        public void validarDatos(Reserva r)
        {
            validarIngresoEgreso(r.FechaInicio, r.FechaFin);
            if (r.ReservaID == null || r.ReservaID == 0) //POST
                validarSalaParaReserva(0, r.SalaID, r.CantidadPersonas, r.FechaInicio, r.FechaFin);
            else  //PUT 
                validarSalaParaReserva(r.ReservaID, r.SalaID, r.CantidadPersonas, r.FechaInicio, r.FechaFin);
        }


        /**
         * Validar que el empleado existe en la base de datos.
         * 
         */
        private void validarEmpleadoExistente(int responsableID)
        {
            if (EmpleadoRepository.findById(responsableID) == null)
                throw new ArgumentException("No existe empleado con la ID de responsable solicitada.");
        }
        /**
         * Validar que los datos de la reserva y la sala que se quiere reservar son congruentes.
         * 
         */
        private void validarSalaParaReserva(int reservaID, int salaID, int cantidadPersonasNecesarias, DateTime fechaInicio, DateTime fechaFin)
        {
            Sala sala = SalaRepository.findById(salaID);
            if (sala == null)
                throw new ArgumentNullException("NO EXISTE SALA CON LA ID BUSCADA.");
            if (sala.Capacidad < cantidadPersonasNecesarias)
                throw new ArgumentException("La sala requerida no tiene suficiente espacio para la cantidad de personas solicitada.");
            validarSalaDisponible(reservaID, sala, fechaInicio, fechaFin);
        }

        /**
         * Validar que la sala a reservar esta disponible en el momento solicitado.
         * 
         */
        private void validarSalaDisponible(int reservaID, Sala sala, DateTime fechaInicio, DateTime fechaFin)
        {
            IEnumerable<Reserva> reservasActuales = ReservaRepository.findAll();
            foreach (Reserva r in reservasActuales)
            {
                if (reservaID != r.ReservaID && reservaOcupaSalaEnPeriodo(r, sala, fechaInicio, fechaFin))
                    throw new ArgumentException("La sala esta reservada para el momento solicitado.");
            }
        }

        /**
         * Validar que el empleado existen salas para ser reservadas segun algunos filtros.
         * 
         */
        public void validarDatos(DateTime ingresoSala, DateTime egresoSala, int tipoSalaID, int cantidadPersonas)
        {
            if (cantidadPersonas <= 0)
                throw new ArgumentOutOfRangeException("La cantidad de personas para la reserva debe ser un número mayor a cero.");
            if (tipoSalaID == 0 || SalaRepository.getTipoByID(tipoSalaID) == null)
                throw new ArgumentException("Debe buscar un tipo de sala existente.");

            validarIngresoEgreso(ingresoSala, egresoSala);

        }

        /**
         * Validar que el ingreso y egreso son adecuados: no se fija con respecto a una sala especifica sino solo mira los valores
         * de los dos parametros para que cumpla que sean el mismo dia y tengan por lo menos una hora de diferencia.
         * 
         */
        private static void validarIngresoEgreso(DateTime ingresoSala, DateTime egresoSala)
        {
            if (ingresoSala == null || egresoSala == null || ingresoSala.CompareTo(egresoSala) >= 0 || egresoSala.Subtract(ingresoSala).TotalMinutes < 60)
                throw new ArgumentException("La hora de egreso buscada debe ser por lo menos una hora mas tarde que la de ingreso.");
            if (ingresoSala.Date != egresoSala.Date)
                throw new ArgumentException("El momento de ingreso y de egreso debe ser en el mismo día.");
        }

        /**
         * Verifica si una reserva ocupa la sala en un determinado periodo y retorna un booleano. 
         * 
         */
        public bool reservaOcupaSalaEnPeriodo(Reserva r, Sala s, DateTime ingresoSala, DateTime egresoSala)
        {
            return r.SalaID == s.SalaID && //la reserva es para la sala que estamos viendo
                                    ( //la reserva ocupa un tiempo en el que queremos reservar por lo que la sala no esta disponible.
                                        r.FechaInicio < egresoSala && ingresoSala < r.FechaFin
                                    );
        }
    }
                                      //(r.FechaInicio.CompareTo(ingresoSala) <= 0 && r.FechaFin.CompareTo(ingresoSala) > 0) ||
                                      //(r.FechaInicio.CompareTo(egresoSala) < 0 && r.FechaFin.CompareTo(egresoSala) >= 0)

}
