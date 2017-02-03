namespace RepositoriesGestionReservaSalas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initRepo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Empleadoes",
                c => new
                    {
                        EmpleadoID = c.Int(nullable: false, identity: true),
                        DNI = c.Long(nullable: false),
                        Legajo = c.Int(nullable: false),
                        NombreYApellido = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.EmpleadoID);
            
            CreateTable(
                "dbo.Reservas",
                c => new
                    {
                        ReservaID = c.Int(nullable: false, identity: true),
                        FechaInicio = c.DateTime(nullable: false),
                        FechaFin = c.DateTime(nullable: false),
                        CantidadPersonas = c.Int(nullable: false),
                        Motivo = c.String(nullable: false),
                        Servicio = c.Boolean(nullable: false),
                        Almuerzo = c.Boolean(nullable: false),
                        Proyector = c.Boolean(nullable: false),
                        SalaID = c.Int(nullable: false),
                        ResponsableID = c.Int(nullable: false),
                        EncuestaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReservaID)
                .ForeignKey("dbo.Encuestas", t => t.EncuestaID, cascadeDelete: true)
                .ForeignKey("dbo.Empleadoes", t => t.ResponsableID, cascadeDelete: true)
                .ForeignKey("dbo.Salas", t => t.SalaID, cascadeDelete: true)
                .Index(t => t.SalaID)
                .Index(t => t.ResponsableID)
                .Index(t => t.EncuestaID);
            
            CreateTable(
                "dbo.Encuestas",
                c => new
                    {
                        EncuestaId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.EncuestaId);
            
            CreateTable(
                "dbo.Preguntas",
                c => new
                    {
                        PreguntaID = c.Int(nullable: false, identity: true),
                        Enunciado = c.String(nullable: false),
                        TipoPregunta = c.Int(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Encuesta_EncuestaId = c.Int(),
                    })
                .PrimaryKey(t => t.PreguntaID)
                .ForeignKey("dbo.Encuestas", t => t.Encuesta_EncuestaId)
                .Index(t => t.Encuesta_EncuestaId);
            
            CreateTable(
                "dbo.Opcions",
                c => new
                    {
                        OpcionID = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false),
                        PreguntaCheckbox_PreguntaID = c.Int(),
                    })
                .PrimaryKey(t => t.OpcionID)
                .ForeignKey("dbo.Preguntas", t => t.PreguntaCheckbox_PreguntaID)
                .Index(t => t.PreguntaCheckbox_PreguntaID);
            
            CreateTable(
                "dbo.Respuestas",
                c => new
                    {
                        RespuestaID = c.Int(nullable: false, identity: true),
                        respuesta = c.String(nullable: false),
                        PreguntaID = c.Int(nullable: false),
                        Encuesta_EncuestaId = c.Int(),
                    })
                .PrimaryKey(t => t.RespuestaID)
                .ForeignKey("dbo.Preguntas", t => t.PreguntaID, cascadeDelete: true)
                .ForeignKey("dbo.Encuestas", t => t.Encuesta_EncuestaId)
                .Index(t => t.PreguntaID)
                .Index(t => t.Encuesta_EncuestaId);
            
            CreateTable(
                "dbo.Salas",
                c => new
                    {
                        SalaID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        Ubicacion = c.String(nullable: false),
                        TipoSalaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SalaID)
                .ForeignKey("dbo.TipoSalas", t => t.TipoSalaID, cascadeDelete: true)
                .Index(t => t.TipoSalaID);
            
            CreateTable(
                "dbo.TipoSalas",
                c => new
                    {
                        TipoID = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false, maxLength: 25),
                    })
                .PrimaryKey(t => t.TipoID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservas", "SalaID", "dbo.Salas");
            DropForeignKey("dbo.Salas", "TipoSalaID", "dbo.TipoSalas");
            DropForeignKey("dbo.Reservas", "ResponsableID", "dbo.Empleadoes");
            DropForeignKey("dbo.Reservas", "EncuestaID", "dbo.Encuestas");
            DropForeignKey("dbo.Respuestas", "Encuesta_EncuestaId", "dbo.Encuestas");
            DropForeignKey("dbo.Respuestas", "PreguntaID", "dbo.Preguntas");
            DropForeignKey("dbo.Preguntas", "Encuesta_EncuestaId", "dbo.Encuestas");
            DropForeignKey("dbo.Opcions", "PreguntaCheckbox_PreguntaID", "dbo.Preguntas");
            DropIndex("dbo.Salas", new[] { "TipoSalaID" });
            DropIndex("dbo.Respuestas", new[] { "Encuesta_EncuestaId" });
            DropIndex("dbo.Respuestas", new[] { "PreguntaID" });
            DropIndex("dbo.Opcions", new[] { "PreguntaCheckbox_PreguntaID" });
            DropIndex("dbo.Preguntas", new[] { "Encuesta_EncuestaId" });
            DropIndex("dbo.Reservas", new[] { "EncuestaID" });
            DropIndex("dbo.Reservas", new[] { "ResponsableID" });
            DropIndex("dbo.Reservas", new[] { "SalaID" });
            DropTable("dbo.TipoSalas");
            DropTable("dbo.Salas");
            DropTable("dbo.Respuestas");
            DropTable("dbo.Opcions");
            DropTable("dbo.Preguntas");
            DropTable("dbo.Encuestas");
            DropTable("dbo.Reservas");
            DropTable("dbo.Empleadoes");
        }
    }
}
