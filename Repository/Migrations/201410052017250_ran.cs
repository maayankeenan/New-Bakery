namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ran : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Branch",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Phone = c.String(),
                        X = c.Double(nullable: false),
                        Y = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        LastName = c.String(),
                        Address = c.String(),
                        Phone = c.String(),
                        Prefered = c.Boolean(nullable: false),
                        Password = c.String(),
                        IsAdmin = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OrderDate = c.DateTime(nullable: false),
                        DelieveryDate = c.DateTime(nullable: false),
                        TotalPrice = c.Double(nullable: false),
                        Comments = c.String(),
                        Status = c.Int(nullable: false),
                        CustomerID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Customer", t => t.CustomerID)
                .Index(t => t.CustomerID);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OrderID = c.Int(nullable: false),
                        PasteryId = c.Int(nullable: false),
                        TotalAmount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Order", t => t.OrderID, cascadeDelete: true)
                .ForeignKey("dbo.Pastery", t => t.PasteryId, cascadeDelete: true)
                .Index(t => t.OrderID)
                .Index(t => t.PasteryId);
            
            CreateTable(
                "dbo.Pastery",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ImageLink = c.String(),
                        Type = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        Comments = c.String(),
                        Vegan = c.Boolean(nullable: false),
                        GlotanFree = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "PasteryId", "dbo.Pastery");
            DropForeignKey("dbo.OrderDetails", "OrderID", "dbo.Order");
            DropForeignKey("dbo.Order", "CustomerID", "dbo.Customer");
            DropIndex("dbo.OrderDetails", new[] { "PasteryId" });
            DropIndex("dbo.OrderDetails", new[] { "OrderID" });
            DropIndex("dbo.Order", new[] { "CustomerID" });
            DropTable("dbo.Pastery");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Order");
            DropTable("dbo.Customer");
            DropTable("dbo.Branch");
        }
    }
}
