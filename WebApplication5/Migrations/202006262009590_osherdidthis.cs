namespace WebApplication5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class osherdidthis : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "confirmpassword", c => c.Int(nullable: false));
            AlterColumn("dbo.Users", "name", c => c.String(nullable: false));
            DropColumn("dbo.Planes", "flightID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Planes", "flightID", c => c.Int(nullable: false));
            AlterColumn("dbo.Users", "name", c => c.String());
            DropColumn("dbo.Users", "confirmpassword");
        }
    }
}
