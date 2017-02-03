namespace RepositoriesGestionReservaSalas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fixmodelfknullable5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reservas", "EncuestaID", "dbo.Encuestas");
            RenameColumn(table: "dbo.Reservas", name: "EncuestaID", newName: "Encuesta_EncuestaId");
            RenameIndex(table: "dbo.Reservas", name: "IX_EncuestaID", newName: "IX_Encuesta_EncuestaId");
            AddForeignKey("dbo.Reservas", "Encuesta_EncuestaId", "dbo.Encuestas", "EncuestaId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservas", "Encuesta_EncuestaId", "dbo.Encuestas");
            RenameIndex(table: "dbo.Reservas", name: "IX_Encuesta_EncuestaId", newName: "IX_EncuestaID");
            RenameColumn(table: "dbo.Reservas", name: "Encuesta_EncuestaId", newName: "EncuestaID");
            AddForeignKey("dbo.Reservas", "EncuestaID", "dbo.Encuestas", "EncuestaId", cascadeDelete: true);
        }
    }
}
