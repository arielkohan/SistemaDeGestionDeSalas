using ModelReservaSalas.modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionSalasAPI.Models
{
    public class ReservasConMisEncuestasBindingModel
    {
        public IEnumerable<Reserva> ReservasParaResponder { get; set; }
        public IEnumerable<Reserva> ReservasRespondidas { get; set; }

        public ReservasConMisEncuestasBindingModel(IEnumerable<Reserva> reservasParaResponder, IEnumerable<Reserva> reservasRespondidas)
        {
            ReservasParaResponder = reservasParaResponder;
            ReservasRespondidas = reservasRespondidas;
        }

    }
}