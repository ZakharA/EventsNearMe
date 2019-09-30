namespace EventsNearMe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Event1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "CoverImage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "CoverImage");
        }
    }
}
