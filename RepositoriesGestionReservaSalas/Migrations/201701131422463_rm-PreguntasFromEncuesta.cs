namespace RepositoriesGestionReservaSalas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rmPreguntasFromEncuesta : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Preguntas", "Encuesta_EncuestaId", "dbo.Encuestas");
            DropIndex("dbo.Preguntas", new[] { "Encuesta_EncuestaId" });
            DropColumn("dbo.Preguntas", "Encuesta_EncuestaId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Preguntas", "Encuesta_EncuestaId", c => c.Int());
            CreateIndex("dbo.Preguntas", "Encuesta_EncuestaId");
            AddForeignKey("dbo.Preguntas", "Encuesta_EncuestaId", "dbo.Encuestas", "EncuestaId");
        }
    }
}
