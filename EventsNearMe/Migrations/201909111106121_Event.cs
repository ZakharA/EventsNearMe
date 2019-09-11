namespace EventsNearMe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Event : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EventCategories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.EventLocations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Address = c.String(),
                        City = c.String(),
                        PostCode = c.Int(nullable: false),
                        latitude = c.Double(nullable: false),
                        longtitude = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Events", "StartingDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Events", "eventLength", c => c.Int(nullable: false));
            AddColumn("dbo.Events", "EventCategory_ID", c => c.Int());
            AddColumn("dbo.Events", "Location_ID", c => c.Int());
            CreateIndex("dbo.Events", "EventCategory_ID");
            CreateIndex("dbo.Events", "Location_ID");
            AddForeignKey("dbo.Events", "EventCategory_ID", "dbo.EventCategories", "ID");
            AddForeignKey("dbo.Events", "Location_ID", "dbo.EventLocations", "ID");
            DropColumn("dbo.Events", "EventCategory");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "EventCategory", c => c.Int());
            DropForeignKey("dbo.Events", "Location_ID", "dbo.EventLocations");
            DropForeignKey("dbo.Events", "EventCategory_ID", "dbo.EventCategories");
            DropIndex("dbo.Events", new[] { "Location_ID" });
            DropIndex("dbo.Events", new[] { "EventCategory_ID" });
            DropColumn("dbo.Events", "Location_ID");
            DropColumn("dbo.Events", "EventCategory_ID");
            DropColumn("dbo.Events", "eventLength");
            DropColumn("dbo.Events", "StartingDate");
            DropTable("dbo.EventLocations");
            DropTable("dbo.EventCategories");
        }
    }
}
