namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Branch",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
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
                        OrderID = c.String(nullable: false, maxLength: 128),
                        OrderDate = c.DateTime(nullable: false),
                        DeliveryDate = c.DateTime(nullable: false),
                        TotalAmount = c.Int(nullable: false),
                        Comments = c.String(),
                        Status = c.Int(nullable: false),
                        CustomerID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.Customer", t => t.CustomerID)
                .Index(t => t.CustomerID);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        OrderID = c.String(maxLength: 128),
                        pastryID = c.Int(nullable: false),
                        TotalAmount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Order", t => t.OrderID)
                .ForeignKey("dbo.Pastry", t => t.pastryID, cascadeDelete: true)
                .Index(t => t.OrderID)
                .Index(t => t.pastryID);
            
            CreateTable(
                "dbo.Pastry",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Picture = c.Binary(),
                        Type = c.Int(nullable: false),
                        Price = c.Single(nullable: false),
                        Comments = c.String(),
                        Vegan = c.Boolean(nullable: false),
                        glotanFree = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "pastryID", "dbo.Pastry");
            DropForeignKey("dbo.OrderDetails", "OrderID", "dbo.Order");
            DropForeignKey("dbo.Order", "CustomerID", "dbo.Customer");
            DropIndex("dbo.OrderDetails", new[] { "pastryID" });
            DropIndex("dbo.OrderDetails", new[] { "OrderID" });
            DropIndex("dbo.Order", new[] { "CustomerID" });
            DropTable("dbo.Pastry");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Order");
            DropTable("dbo.Customer");
            DropTable("dbo.Branch");
        }
    }
}
