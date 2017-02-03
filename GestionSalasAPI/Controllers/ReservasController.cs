using GestionSalasAPI.Models;
using ModelReservaSalas.modelo;
using ModelReservaSalas.Servicios;
using RepositoriesGestionReservaSalas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using System.Security.Principal;
using System.Web;

namespace GestionSalasAPI.Controllers
{
    public class ReservasController : ApiController
    {
        private ApplicationDbContext ApplicationDbContext;
        private ReservaRepository reservaRepository;
        private ReservarService reservarService;

        public ReservasController()
        {
            this.ApplicationDbContext = new ApplicationDbContext();
            this.reservaRepository = new ReservaRepository();
            this.reservarService = new ReservarService(reservaRepository, new SalaRepository(), new EmpleadoRepository());
        }

        // GET: api/Reservas
        [Authorize]
        [Route("api/Reservas")]
        [HttpGet]
        public ReservasBindingModels Get() 
        {
            IEnumerable<Reserva> reservasDelUsuario;
            IEnumerable<Reserva> reservasDeOtrosUsuarios;
            string userID = User.Identity.GetUserId();
            ApplicationUser user = ApplicationDbContext.Users.Find(userID);
            int empleadoID = user.EmpleadoID;

            reservarService.getReservas(empleadoID, out reservasDelUsuario, out reservasDeOtrosUsuarios);
            ReservasBindingModels result = new ReservasBindingModels(reservasDelUsuario, reservasDeOtrosUsuarios);
            return result;
        }
         
        // GET: api/Reservas/5
        [Authorize]
        [Route("api/Reservas/{id}")]
        public Reserva Get(int id)
        {
            //TODO: Decidir si hay que chequear si pertenece al empleado con la sesion actual o no
            string userID = User.Identity.GetUserId();
            ApplicationUser user = ApplicationDbContext.Users.Find(userID);
            int empleadoID = user.EmpleadoID;
            Reserva r = reservaRepository.findById(id);
            if (r == null) throw new ArgumentException("No existe reserva con la ID solicitada.");
            if (r.ResponsableID != empleadoID)  throw new ArgumentException("El id del responsable de la reserva no coincide con el empleado que inicio sesión.");
            return r;
        }

        [Route("api/Reservas/salas-disponibles")]
        [HttpGet]
        public IEnumerable<Sala> GetSalasParaReservar(DateTime ingresoSala, DateTime egresoSala, int tipoSalaID, int cantidadPersonas)
        {
            return reservarService.getSalasParaReservar(ingresoSala, egresoSala, tipoSalaID, cantidadPersonas);
        }

        // POST: api/Reservas
        [Authorize]
        [Route("api/Reservas")]
        [HttpPost]
        public void Post([FromBody]Reserva r)
        {

            string userID = User.Identity.GetUserId(); 
            ApplicationUser user = ApplicationDbContext.Users.Find(userID);
            int empleadoID = user.EmpleadoID;
            reservarService.Reservar(r, empleadoID);
        }

        // PUT: api/Reservas/5
        // GET: api/Reservas
        [Authorize]
        [Route("api/Reservas/{id}")]
        [HttpPut]
        public void Put(int id, [FromBody]Reserva r)
        {
            string userID = User.Identity.GetUserId();
            ApplicationUser user = ApplicationDbContext.Users.Find(userID);
            int empleadoID = user.EmpleadoID;
            reservarService.editarReserva(r, empleadoID);
        }

        // DELETE: api/Reservas/5
        [Authorize]
        [Route("api/Reservas/{reservaID}")]
        [HttpDelete]
        public void Delete(int reservaID)
        {
            string userID = User.Identity.GetUserId();
            ApplicationUser user = ApplicationDbContext.Users.Find(userID);
            int empleadoID = user.EmpleadoID;
            reservarService.eliminarReserva(reservaID, empleadoID);
        }
    }
}
