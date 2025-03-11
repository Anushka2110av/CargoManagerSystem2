namespace CargoManagerSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fifth : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "Password", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.Employees", "Password");
        }
    }
}
