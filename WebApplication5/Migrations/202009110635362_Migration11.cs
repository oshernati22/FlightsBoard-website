namespace WebApplication5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "bestFlightBoard", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "bestFlightBoard");
        }
    }
}
