namespace CFMigration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterNameKeyProperty1 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Products");
            AddPrimaryKey("dbo.Products", new[] { "Id", "Name" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Products");
            AddPrimaryKey("dbo.Products", "Name");
        }
    }
}
