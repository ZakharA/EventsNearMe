namespace EventsNearMe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Bookingupdate : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Bookings", name: "UserId", newName: "User_Id");
            RenameIndex(table: "dbo.Bookings", name: "IX_UserId", newName: "IX_User_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Bookings", name: "IX_User_Id", newName: "IX_UserId");
            RenameColumn(table: "dbo.Bookings", name: "User_Id", newName: "UserId");
        }
    }
}
