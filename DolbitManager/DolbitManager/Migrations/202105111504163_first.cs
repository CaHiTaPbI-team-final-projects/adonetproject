namespace DolbitManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Records",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        GroupId = c.Int(nullable: false),
                        ProducerId = c.Int(nullable: false),
                        TrackCount = c.Int(nullable: false),
                        GenreId = c.Int(nullable: false),
                        StorageId = c.Int(nullable: false),
                        RecordDate = c.DateTime(nullable: false),
                        BasicPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SalePrice = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Genres", t => t.GenreId, cascadeDelete: true)
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("dbo.Producers", t => t.ProducerId, cascadeDelete: true)
                .ForeignKey("dbo.Storages", t => t.StorageId, cascadeDelete: true)
                .Index(t => t.GroupId)
                .Index(t => t.ProducerId)
                .Index(t => t.GenreId)
                .Index(t => t.StorageId);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Producers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RecordId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Records", t => t.RecordId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RecordId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        Phone = c.String(),
                        FirstName = c.String(),
                        SecondName = c.String(),
                        ThirdName = c.String(),
                        MoneySpent = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Storages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Records", "StorageId", "dbo.Storages");
            DropForeignKey("dbo.Sales", "UserId", "dbo.Users");
            DropForeignKey("dbo.Sales", "RecordId", "dbo.Records");
            DropForeignKey("dbo.Records", "ProducerId", "dbo.Producers");
            DropForeignKey("dbo.Records", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.Records", "GenreId", "dbo.Genres");
            DropIndex("dbo.Sales", new[] { "RecordId" });
            DropIndex("dbo.Sales", new[] { "UserId" });
            DropIndex("dbo.Records", new[] { "StorageId" });
            DropIndex("dbo.Records", new[] { "GenreId" });
            DropIndex("dbo.Records", new[] { "ProducerId" });
            DropIndex("dbo.Records", new[] { "GroupId" });
            DropTable("dbo.Storages");
            DropTable("dbo.Users");
            DropTable("dbo.Sales");
            DropTable("dbo.Producers");
            DropTable("dbo.Groups");
            DropTable("dbo.Records");
            DropTable("dbo.Genres");
        }
    }
}
