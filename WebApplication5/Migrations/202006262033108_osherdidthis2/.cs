namespace WebApplication5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class osherdidthis2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "password", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "confirmpassword", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "confirmpassword", c => c.Int(nullable: false));
            AlterColumn("dbo.Users", "password", c => c.Int(nullable: false));
        }
    }
}
