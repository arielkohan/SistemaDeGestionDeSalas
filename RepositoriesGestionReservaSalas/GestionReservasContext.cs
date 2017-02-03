using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ModelReservaSalas.modelo;
using System.ComponentModel.DataAnnotations;

namespace RepositoriesGestionReservaSalas
{

    public class GestionReservasContext : DbContext
    {
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Sala> Salas { get; set; }
        public DbSet<TipoSala> TiposSalas { get; set; }
        public DbSet<PreguntaCheckbox> PreguntasCheckbox { get; set; }
        public DbSet<PreguntaDesarrollo> PreguntasDesarrollo { get; set; }
        public DbSet<Encuesta> Encuestas { get; set; }
        public DbSet<Respuesta> Respuestas { get; set; }

        public GestionReservasContext() : base("GestionReservasContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

       protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //configurarReserva(modelBuilder);
        }


        //private void configurarReserva(DbModelBuilder modelBuilder)
        //{
        //    //modelBuilder.Entity<Reserva>().Property(r => r.CantidadPersonas).IsRequired();
        //    //modelBuilder.Entity<Reserva>().Property(r => r.FechaInicio).IsRequired();
        //    //modelBuilder.Entity<Reserva>().Property(r => r.FechaFin).IsRequired();
        //    //modelBuilder.Entity<Reserva>().Property(r => r.ResponsableID).IsRequired();
        //    //modelBuilder.Entity<Reserva>().Property(r => r.SalaID).IsRequired();
        //    //modelBuilder.Entity<Reserva>().Property(r => r.Encuesta).IsOptional():
        //}
    }
}
