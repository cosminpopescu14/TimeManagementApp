namespace ProiectLicenta.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TasksXUser : DbMigration
    {
        public override void Up()
        {
            /*CreateTable(
                "dbo.Echipa_Eveniment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Id_Eveniment = c.Int(),
                        Id_Utilizator = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Eveniment", t => t.Id_Eveniment)
                .ForeignKey("dbo.Utilizatori", t => t.Id_Utilizator)
                .Index(t => t.Id_Eveniment)
                .Index(t => t.Id_Utilizator);
            
            CreateTable(
                "dbo.Eveniment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Denumire = c.String(maxLength: 500, unicode: false),
                        Data_Start = c.DateTime(),
                        Data_Sfarsit = c.DateTime(),
                        Activ = c.Boolean(),
                        Id_MO = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Id_Tip_Task = c.Int(),
                        Id_Eveniment = c.Int(),
                        Descriere_Suplimentara = c.String(unicode: false),
                        Termen = c.DateTime(),
                        Data_Creare_Task = c.DateTime(),
                        Data_Sfarsit_Task = c.DateTime(),
                        Stare_Task = c.String(maxLength: 20, unicode: false),
                        Activ = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tip_Task", t => t.Id_Tip_Task)
                .ForeignKey("dbo.Eveniment", t => t.Id_Eveniment)
                .Index(t => t.Id_Tip_Task)
                .Index(t => t.Id_Eveniment);
            
            CreateTable(
                "dbo.Tip_Task",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Denumire = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Task_Functie",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Id_Functie = c.Int(),
                        Id_Tip_Task = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Functie", t => t.Id_Functie)
                .ForeignKey("dbo.Tip_Task", t => t.Id_Tip_Task)
                .Index(t => t.Id_Functie)
                .Index(t => t.Id_Tip_Task);
            
            CreateTable(
                "dbo.Functie",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Denumire = c.String(maxLength: 40, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Utilizatori",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nume_Utilizator = c.String(maxLength: 50, unicode: false),
                        Email = c.String(maxLength: 20, unicode: false),
                        Parola = c.String(maxLength: 50, unicode: false),
                        Id_Functie = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Functie", t => t.Id_Functie)
                .Index(t => t.Id_Functie);
            
            CreateTable(
                "dbo.MembruEchipaXTasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Id_Task = c.Int(nullable: false),
                        Id_Utilizator = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.sysdiagrams",
                c => new
                    {
                        diagram_id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 128),
                        principal_id = c.Int(nullable: false),
                        version = c.Int(),
                        definition = c.Binary(),
                    })
                .PrimaryKey(t => t.diagram_id);*/
            
            CreateTable(
                "dbo.MembruEchipaXTask",
                c => new
                    {
                        Id_Task = c.Int(nullable: false),
                        Id_Utilizator = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id_Task, t.Id_Utilizator })
                .ForeignKey("dbo.Tasks", t => t.Id_Task, cascadeDelete: true)
                .ForeignKey("dbo.Utilizatori", t => t.Id_Utilizator, cascadeDelete: true)
                .Index(t => t.Id_Task)
                .Index(t => t.Id_Utilizator);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tasks", "Id_Eveniment", "dbo.Eveniment");
            DropForeignKey("dbo.MembruEchipaXTask", "Id_Utilizator", "dbo.Utilizatori");
            DropForeignKey("dbo.MembruEchipaXTask", "Id_Task", "dbo.Tasks");
            DropForeignKey("dbo.Tasks", "Id_Tip_Task", "dbo.Tip_Task");
            DropForeignKey("dbo.Task_Functie", "Id_Tip_Task", "dbo.Tip_Task");
            DropForeignKey("dbo.Utilizatori", "Id_Functie", "dbo.Functie");
            DropForeignKey("dbo.Echipa_Eveniment", "Id_Utilizator", "dbo.Utilizatori");
            DropForeignKey("dbo.Task_Functie", "Id_Functie", "dbo.Functie");
            DropForeignKey("dbo.Echipa_Eveniment", "Id_Eveniment", "dbo.Eveniment");
            DropIndex("dbo.MembruEchipaXTask", new[] { "Id_Utilizator" });
            DropIndex("dbo.MembruEchipaXTask", new[] { "Id_Task" });
            DropIndex("dbo.Utilizatori", new[] { "Id_Functie" });
            DropIndex("dbo.Task_Functie", new[] { "Id_Tip_Task" });
            DropIndex("dbo.Task_Functie", new[] { "Id_Functie" });
            DropIndex("dbo.Tasks", new[] { "Id_Eveniment" });
            DropIndex("dbo.Tasks", new[] { "Id_Tip_Task" });
            DropIndex("dbo.Echipa_Eveniment", new[] { "Id_Utilizator" });
            DropIndex("dbo.Echipa_Eveniment", new[] { "Id_Eveniment" });
            DropTable("dbo.MembruEchipaXTask");
            DropTable("dbo.sysdiagrams");
            DropTable("dbo.MembruEchipaXTasks");
            DropTable("dbo.Utilizatori");
            DropTable("dbo.Functie");
            DropTable("dbo.Task_Functie");
            DropTable("dbo.Tip_Task");
            DropTable("dbo.Tasks");
            DropTable("dbo.Eveniment");
            DropTable("dbo.Echipa_Eveniment");
        }
    }
}
