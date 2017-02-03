using GestionSalasAPI.Models;
using Microsoft.AspNet.Identity;
using ModelReservaSalas.modelo;
using ModelReservaSalas.Servicios;
using RepositoriesGestionReservaSalas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GestionSalasAPI.Controllers
{
    [Authorize]
    public class EncuestasController : ApiController
    {
        private ObtencionEncuestasService ObtencionDeEncuestasService;
        private CompletarEncuestaService CompletarEncuestaService;
        private ApplicationDbContext ApplicationDbContext;


        public EncuestasController()
        {
            this.ApplicationDbContext = new ApplicationDbContext();
            var empRep = new EmpleadoRepository();
            var resRep = new ReservaRepository();
            var encRep = new EncuestaRepository();
            this.ObtencionDeEncuestasService = new ObtencionEncuestasService(empRep, resRep, encRep);
            this.CompletarEncuestaService = new CompletarEncuestaService(empRep, resRep, encRep);
        }

        // GET: api/Encuestas
        [Route("api/Encuestas")]
        [HttpGet]
        public IEnumerable<Encuesta> Get()
        {
            string userID = User.Identity.GetUserId();
            ApplicationUser user = ApplicationDbContext.Users.Find(userID);
            int empleadoID = user.EmpleadoID;
            return this.ObtencionDeEncuestasService.getEncuestasFromEmpleado(empleadoID) ;
        }

        
        [Route("api/Encuestas/reservas")]
        [HttpGet]
        public ReservasConMisEncuestasBindingModel GetReservasParaReponder()
        {
            string userID = User.Identity.GetUserId();
            ApplicationUser user = ApplicationDbContext.Users.Find(userID);
            int empleadoID = user.EmpleadoID;

            IEnumerable<Reserva> reservasParaResponder;
            IEnumerable<Reserva> reservasRespondidas;

            this.ObtencionDeEncuestasService.getReservasAResponderYRespondidas(empleadoID, out reservasParaResponder, out reservasRespondidas);
            return new ReservasConMisEncuestasBindingModel(reservasParaResponder, reservasRespondidas);
        }

        // GET: api/Encuestas/5
        // GET: api/Encuestas
        [Route("api/Encuestas/{encuestaID}")]
        [HttpGet]
        public Encuesta Get(int encuestaID)
        {
            string userID = User.Identity.GetUserId();
            ApplicationUser user = ApplicationDbContext.Users.Find(userID);
            int empleadoID = user.EmpleadoID;
            return this.ObtencionDeEncuestasService.getOneEncuestaFromEmpleado(empleadoID, encuestaID) ;
        }

        // GET: api/Encuestas
        [Route("api/Encuestas/obtener-preguntas")]
        [HttpGet]
        public IEnumerable<Pregunta> GetPreguntas()
        {
            return ObtencionDeEncuestasService.getPreguntas();
        }

        // POST: api/Encuestas
        // GET: api/Encuestas
        [Route("api/Encuestas/{reservaID}")]
        [HttpPost]
        public void Post(int reservaID, [FromBody]Encuesta encuesta)
        {
            string userID = User.Identity.GetUserId();
            ApplicationUser user = ApplicationDbContext.Users.Find(userID);
            int empleadoID = user.EmpleadoID;
            CompletarEncuestaService.completar(encuesta, reservaID, empleadoID);
        }

        // PUT: api/Encuestas/5
        // GET: api/Encuestas
        [Route("api/Encuestas/{id}")]
        [HttpPut]
        public void Put(int id, [FromBody]Encuesta value)
        {
        }

        // DELETE: api/Encuestas/5
        // GET: api/Encuestas
        [Route("api/Encuestas/{id}")]
        [HttpDelete]
        public void Delete(int id)
        {
        }
    }
}
