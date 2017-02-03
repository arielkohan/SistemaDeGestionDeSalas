using ModelReservaSalas.modelo;
using ModelReservaSalas.repositorios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModelReservaSalas.Servicios
{
    public class GestionSalasService
    {
        private ISalaRepositorio salaRepository;

        public GestionSalasService(ISalaRepositorio repository)
        {
            this.salaRepository = repository;
        }

        public GestionSalasService()
        {
        }

        public Sala getSalaByID(int id)
        {
            return salaRepository.findById(id);
        }
        public IEnumerable<Sala> getSalas()
        {
            return salaRepository.findAll();
        }

        public void delete(int id)
        {
            salaRepository.delete(id);
        }

        public Sala create(Sala sala)
        {
            validarSala(sala);
            return salaRepository.create(sala);
        }

        public Sala update(Sala sala)
        {
            validarSala(sala);
            return salaRepository.update(sala);
        }

        private void validarSala(Sala sala)
        {
            var context = new ValidationContext(sala, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(sala, context, results);
            if (!isValid)
                throw new Exception("Model is not valid because " + string.Join(", ", results.Select(s => s.ErrorMessage).ToArray()));
        }

        public IEnumerable<TipoSala> getTiposSala()
        {
            return salaRepository.getTipos();
        }
    }
}
