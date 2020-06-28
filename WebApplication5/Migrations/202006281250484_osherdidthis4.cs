namespace WebApplication5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class osherdidthis4 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.FlightBoards", "flightID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FlightBoards", "flightID", c => c.Int(nullable: false));
        }
    }
}
