namespace WebApplication5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Flights",
                c => new
                    {
                        flightId = c.Int(nullable: false, identity: true),
                        flightNumber = c.Int(nullable: false),
                        from = c.String(),
                        to = c.String(),
                        price = c.Double(nullable: false),
                        planeID = c.Int(nullable: false),
                        flightAttendantID = c.Int(nullable: false),
                        flightBoardID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.flightId)
                .ForeignKey("dbo.FlightAttendants", t => t.flightAttendantID, cascadeDelete: true)
                .ForeignKey("dbo.FlightBoards", t => t.flightBoardID, cascadeDelete: true)
                .ForeignKey("dbo.Planes", t => t.planeID, cascadeDelete: true)
                .Index(t => t.planeID)
                .Index(t => t.flightAttendantID)
                .Index(t => t.flightBoardID);
            
            CreateTable(
                "dbo.FlightAttendants",
                c => new
                    {
                        flightAttendantID = c.Int(nullable: false, identity: true),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.flightAttendantID);
            
            CreateTable(
                "dbo.FlightBoards",
                c => new
                    {
                        flightBoardID = c.Int(nullable: false, identity: true),
                        boardName = c.String(),
                        flightID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.flightBoardID);
            
            CreateTable(
                "dbo.Planes",
                c => new
                    {
                        planeID = c.Int(nullable: false, identity: true),
                        company = c.String(),
                        flightID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.planeID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        userID = c.Int(nullable: false, identity: true),
                        type = c.Boolean(nullable: false),
                        name = c.String(),
                        password = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.userID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Flights", "planeID", "dbo.Planes");
            DropForeignKey("dbo.Flights", "flightBoardID", "dbo.FlightBoards");
            DropForeignKey("dbo.Flights", "flightAttendantID", "dbo.FlightAttendants");
            DropIndex("dbo.Flights", new[] { "flightBoardID" });
            DropIndex("dbo.Flights", new[] { "flightAttendantID" });
            DropIndex("dbo.Flights", new[] { "planeID" });
            DropTable("dbo.Users");
            DropTable("dbo.Planes");
            DropTable("dbo.FlightBoards");
            DropTable("dbo.FlightAttendants");
            DropTable("dbo.Flights");
        }
    }
}
