namespace CargoManagerSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Administrators",
                c => new
                    {
                        AdminId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.AdminId);
            
            CreateTable(
                "dbo.CargoFares",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CargoType = c.String(),
                        Fare = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CargoOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderNumber = c.String(nullable: false),
                        CustomerName = c.String(),
                        Destination = c.String(),
                        PickupLocation = c.String(nullable: false),
                        DropLocation = c.String(),
                        PickupDate = c.DateTime(nullable: false),
                        EstimatedDeliveryDate = c.DateTime(nullable: false),
                        Status = c.String(),
                        CargoType = c.String(nullable: false),
                        Weight = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Volume = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CustomerId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false),
                        Phone = c.String(nullable: false),
                        Address = c.String(nullable: false, maxLength: 250),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CargoTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        BasePrice = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        PricePerKm = c.Double(nullable: false),
                        PickupDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Role = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.EmployeeId);
            
            CreateTable(
                "dbo.Pricings",
                c => new
                    {
                        PricingId = c.Int(nullable: false, identity: true),
                        CityId = c.Int(nullable: false),
                        CargoTypeId = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.PricingId)
                .ForeignKey("dbo.CargoTypes", t => t.CargoTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .Index(t => t.CityId)
                .Index(t => t.CargoTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pricings", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Pricings", "CargoTypeId", "dbo.CargoTypes");
            DropForeignKey("dbo.CargoOrders", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Pricings", new[] { "CargoTypeId" });
            DropIndex("dbo.Pricings", new[] { "CityId" });
            DropIndex("dbo.CargoOrders", new[] { "CustomerId" });
            DropTable("dbo.Pricings");
            DropTable("dbo.Employees");
            DropTable("dbo.Cities");
            DropTable("dbo.CargoTypes");
            DropTable("dbo.Customers");
            DropTable("dbo.CargoOrders");
            DropTable("dbo.CargoFares");
            DropTable("dbo.Administrators");
        }
    }
}
