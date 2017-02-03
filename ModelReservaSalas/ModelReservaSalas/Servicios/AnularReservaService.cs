using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelReservaSalas.repositorios;

namespace ModelReservaSalas.Servicios
{
    public class AnularReservaService
    {
        private IReservaRepository ReservaRepository;

        public AnularReservaService(IReservaRepository repository)
        {
            this.ReservaRepository = repository;
        }


        //OPERACIONES


    }
}
