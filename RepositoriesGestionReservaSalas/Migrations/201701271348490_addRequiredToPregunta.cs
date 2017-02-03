namespace RepositoriesGestionReservaSalas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addRequiredToPregunta : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Preguntas", "Required", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Preguntas", "Required");
        }
    }
}
