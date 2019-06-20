namespace CFMigration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterNameKeyProperty : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Products");
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Products", "Name");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Products");
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: false));
            AddPrimaryKey("dbo.Products", "Id");
        }
    }
}
