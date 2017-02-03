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
    
    public class SalasController : ApiController
    {
        private GestionSalasService salasService;

        public SalasController()
        {
            this.salasService = new GestionSalasService(new SalaRepository());
        }

        // GET: api/Salas
        [Route("api/Salas")]
        public IEnumerable<Sala> Get()
        {
            return salasService.getSalas();
        }

        // GET: api/Salas/5
        [Route("api/Salas/{id}")]
        public Sala Get(int id)
        {
            return salasService.getSalaByID(id);
        }

        // POST: api/Salas
        //[Authorize]
        [Route("api/Salas")]
        public Sala Post([FromBody]Sala sala)
        {
            return salasService.create(sala);
        }

        // PUT: api/Salas/5
        [Route("api/Salas/{id}")]
        public Sala Put(int id, [FromBody]Sala sala)
        {
            return salasService.update(sala);
        }

        // DELETE: api/Salas/5
        [Route("api/Salas/{id}")]
        public void Delete(int id)
        {
            salasService.delete(id);
        }
        
        [Route("api/tipos")]
        public IEnumerable<TipoSala> GetTiposSala()
        {
            return salasService.getTiposSala();
        }
    }
}
