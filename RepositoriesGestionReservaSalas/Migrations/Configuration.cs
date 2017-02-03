namespace RepositoriesGestionReservaSalas.Migrations
{
    using ModelReservaSalas.modelo;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<RepositoriesGestionReservaSalas.GestionReservasContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(RepositoriesGestionReservaSalas.GestionReservasContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            List<Opcion> opcionesPreparacionYLimpieza = new List<Opcion>()
            {
                new Opcion("Muy Bien"),
                new Opcion("Bien"),
                new Opcion("Regular"),
            };
            List<Opcion> opcionesMaterialesSolicitados = new List<Opcion>()
            {
                new Opcion("Contamos con los materiales solicitados."),
                new Opcion("No contamos con los materiales solcitados."),
            };

            List<Opcion> opcionesAlmuerzoYAtencion = new List<Opcion>()
            {
                new Opcion("Se sirvió a tiempo."),
                new Opcion("No se sirvió a tiempo."),
                new Opcion("No se solicitó."),
            };
            List<Opcion> opcionesGustoYCantidadAlmuerzo= new List<Opcion>()
            {
                new Opcion("Gustó en calidad y cantidad."),
                new Opcion("No gustó en calidad y cantidad."),
                new Opcion("No se solicitó."),
            };




            context.PreguntasCheckbox.AddOrUpdate(
                new PreguntaCheckbox("Preparación y Limpieza", opcionesPreparacionYLimpieza, true),
                new PreguntaCheckbox("Materiales solicitados", opcionesMaterialesSolicitados, true),
                new PreguntaCheckbox("Atención del almuerzo", opcionesAlmuerzoYAtencion, true),
                new PreguntaCheckbox("Gusto y cantidad de almuerzo", opcionesGustoYCantidadAlmuerzo, true)
            );

            context.PreguntasDesarrollo.AddOrUpdate(
                new PreguntaDesarrollo("Observaciones", false)
            );
        }
    }
}
