namespace RepositoriesGestionReservaSalas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullableEncuestaFK : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reservas", "EncuestaID", "dbo.Encuestas");
            DropIndex("dbo.Reservas", new[] { "EncuestaID" });
            AlterColumn("dbo.Reservas", "EncuestaID", c => c.Int());
            CreateIndex("dbo.Reservas", "EncuestaID");
            AddForeignKey("dbo.Reservas", "EncuestaID", "dbo.Encuestas", "EncuestaId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservas", "EncuestaID", "dbo.Encuestas");
            DropIndex("dbo.Reservas", new[] { "EncuestaID" });
            AlterColumn("dbo.Reservas", "EncuestaID", c => c.Int(nullable: false));
            CreateIndex("dbo.Reservas", "EncuestaID");
            AddForeignKey("dbo.Reservas", "EncuestaID", "dbo.Encuestas", "EncuestaId", cascadeDelete: true);
        }
    }
}
