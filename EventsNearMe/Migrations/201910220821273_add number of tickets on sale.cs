namespace EventsNearMe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnumberofticketsonsale : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "numberOfTickets", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "numberOfTickets");
        }
    }
}
