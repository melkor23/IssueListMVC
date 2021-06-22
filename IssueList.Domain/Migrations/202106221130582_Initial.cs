namespace IssueList.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Issues",
                c => new
                    {
                        IssueId = c.Int(nullable: false, identity: true),
                        title = c.String(maxLength: 50),
                        description = c.String(maxLength: 50),
                        severity = c.Int(nullable: false),
                        status = c.Int(nullable: false),
                        asignee = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IssueId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Issues");
        }
    }
}
