namespace TD.OPDT.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DataSet",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FieldId = c.Int(nullable: false),
                        OfficeId = c.Int(nullable: false),
                        AttachmentsRaw = c.String(),
                        LinkApi = c.String(),
                        CreatedAt = c.DateTime(),
                        ModifiedAt = c.DateTime(),
                        Code = c.String(),
                        Description = c.String(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Field", t => t.FieldId, cascadeDelete: true)
                .ForeignKey("dbo.Office", t => t.OfficeId, cascadeDelete: true)
                .Index(t => t.FieldId)
                .Index(t => t.OfficeId);
            
            CreateTable(
                "dbo.Field",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Order = c.Int(nullable: false),
                        CreatedAt = c.DateTime(),
                        ModifiedAt = c.DateTime(),
                        Code = c.String(),
                        Description = c.String(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Office",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentId = c.Int(),
                        CreatedAt = c.DateTime(),
                        ModifiedAt = c.DateTime(),
                        Code = c.String(),
                        Description = c.String(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Office", t => t.ParentId)
                .Index(t => t.ParentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DataSet", "OfficeId", "dbo.Office");
            DropForeignKey("dbo.Office", "ParentId", "dbo.Office");
            DropForeignKey("dbo.DataSet", "FieldId", "dbo.Field");
            DropIndex("dbo.Office", new[] { "ParentId" });
            DropIndex("dbo.DataSet", new[] { "OfficeId" });
            DropIndex("dbo.DataSet", new[] { "FieldId" });
            DropTable("dbo.Office");
            DropTable("dbo.Field");
            DropTable("dbo.DataSet");
        }
    }
}
