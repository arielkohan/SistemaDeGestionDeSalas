namespace RepositoriesGestionReservaSalas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcapacidadSalas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Salas", "Capacidad", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Salas", "Capacidad");
        }
    }
}
