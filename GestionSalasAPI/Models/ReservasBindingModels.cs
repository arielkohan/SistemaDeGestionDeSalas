using ModelReservaSalas.modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionSalasAPI.Models
{
    public class ReservasBindingModels
    {
        public ReservasBindingModels(IEnumerable<Reserva> reservasDelUsuario, IEnumerable<Reserva> reservasDeOtrosUsuarios)
        {
            ReservasDelUsuario = parse(reservasDelUsuario);
            ReservasDeOtrosUsuarios = parse(reservasDeOtrosUsuarios);
        }

        private List<ReservaDTO> parse(IEnumerable<Reserva> reservasDeOtrosUsuarios)
        {
            List<ReservaDTO> result = new List<ReservaDTO>();
            foreach(Reserva r in reservasDeOtrosUsuarios)
            {
                ReservaDTO temp = new ReservaDTO();
                temp.Almuerzo = r.Almuerzo;
                temp.CantidadPersonas = r.CantidadPersonas;
                //temp.EncuestaID = r.EncuestaID;
                temp.FechaFin = r.FechaFin;
                temp.FechaInicio = r.FechaInicio;
                temp.Motivo = r.Motivo;
                temp.Proyector = r.Proyector;
                temp.ReservaID = r.ReservaID;
                temp.SalaID = r.SalaID;
                temp.SalaNombre = r.Sala.Nombre;
                temp.SalaUbicacion = r.Sala.Ubicacion;
                temp.Servicio = r.Servicio;
                temp.ResponsableID = r.ResponsableID;
                temp.ResponsableNomYAp = r.Responsable.NombreYApellido;

                result.Add(temp);
            }
            return result;
        }

        public IEnumerable<ReservaDTO> ReservasDelUsuario { get; set; }
        public IEnumerable<ReservaDTO> ReservasDeOtrosUsuarios { get; set; }
    }

   public class ReservaDTO
    {
        public int ReservaID { get; set; }
        
        public DateTime FechaInicio { get; set; }
        
        public DateTime FechaFin { get; set; }
        
        public int CantidadPersonas { get; set; }
        
        public String Motivo { get; set; }
        
        public Boolean Servicio { get; set; }
        
        public Boolean Almuerzo { get; set; }
        
        public Boolean Proyector { get; set; }

        
        public int SalaID { get; set; }
        public string SalaNombre { get; set; }
        public string SalaUbicacion { get; set; }


        public int ResponsableID { get; set; }
        public string ResponsableNomYAp { get; set; }



        public int? EncuestaID { get; set; }
        
    }
}