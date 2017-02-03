namespace RepositoriesGestionReservaSalas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeRequiredFromRespuestaRespuesta : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Respuestas", "respuesta", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Respuestas", "respuesta", c => c.String(nullable: false));
        }
    }
}
