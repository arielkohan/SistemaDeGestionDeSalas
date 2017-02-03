namespace RepositoriesGestionReservaSalas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fixmodelfknullable3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reservas", "EncuestaID", "dbo.Encuestas");
            AddForeignKey("dbo.Reservas", "EncuestaID", "dbo.Encuestas", "EncuestaId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservas", "EncuestaID", "dbo.Encuestas");
            AddForeignKey("dbo.Reservas", "EncuestaID", "dbo.Encuestas", "EncuestaId");
        }
    }
}
