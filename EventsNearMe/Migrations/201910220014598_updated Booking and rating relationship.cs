namespace EventsNearMe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedBookingandratingrelationship : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        RatingID = c.Int(nullable: false, identity: true),
                        number = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RatingID)
                .ForeignKey("dbo.Bookings", t => t.RatingID)
                .Index(t => t.RatingID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ratings", "RatingID", "dbo.Bookings");
            DropIndex("dbo.Ratings", new[] { "RatingID" });
            DropTable("dbo.Ratings");
        }
    }
}
