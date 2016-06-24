namespace ProiectLicenta.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tasks_Users : DbMigration
    {
        public override void Up()
        {
            RenameTable("MembruEchipaXTask", "MembruEchipaXTasks");
        }
        
        public override void Down()
        {
            RenameTable("MembruEchipaXTask", "MembruEchipaXTasks");
        }
    }
}
