namespace EventsNearMe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixbooking_rating_const_error : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Ratings", "RatingID", "dbo.Bookings");
            DropIndex("dbo.Ratings", new[] { "RatingID" });
            DropPrimaryKey("dbo.Ratings");
            AddColumn("dbo.Ratings", "BookingId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Ratings", "BookingId");
            CreateIndex("dbo.Ratings", "BookingId");
            AddForeignKey("dbo.Ratings", "BookingId", "dbo.Bookings", "BookingID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ratings", "BookingId", "dbo.Bookings");
            DropIndex("dbo.Ratings", new[] { "BookingId" });
            DropPrimaryKey("dbo.Ratings");
            DropColumn("dbo.Ratings", "BookingId");
            AddPrimaryKey("dbo.Ratings", "RatingID");
            CreateIndex("dbo.Ratings", "RatingID");
            AddForeignKey("dbo.Ratings", "RatingID", "dbo.Bookings", "BookingID");
        }
    }
}
