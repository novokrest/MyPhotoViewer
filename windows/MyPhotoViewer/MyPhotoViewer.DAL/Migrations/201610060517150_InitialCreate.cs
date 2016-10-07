namespace MyPhotoViewer.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PhotoCollection",
                c => new
                    {
                        PhotoCollectionId = c.Int(nullable: false, identity: true),
                        PlaceId = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        Period_From = c.DateTime(nullable: false),
                        Period_To = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PhotoCollectionId)
                .ForeignKey("dbo.Place", t => t.PlaceId)
                .Index(t => t.PlaceId);
            
            CreateTable(
                "dbo.Photo",
                c => new
                    {
                        PhotoId = c.Int(nullable: false, identity: true),
                        PhotoCollectionId = c.Int(nullable: false),
                        PlaceId = c.Int(nullable: false),
                        Title = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        Path = c.String(),
                    })
                .PrimaryKey(t => t.PhotoId)
                .ForeignKey("dbo.PhotoCollection", t => t.PhotoCollectionId, cascadeDelete: true)
                .ForeignKey("dbo.Place", t => t.PlaceId, cascadeDelete: true)
                .Index(t => t.PhotoCollectionId)
                .Index(t => t.PlaceId);
            
            CreateTable(
                "dbo.Place",
                c => new
                    {
                        PlaceId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        City = c.String(),
                        Country = c.String(),
                    })
                .PrimaryKey(t => t.PlaceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PhotoCollection", "PlaceId", "dbo.Place");
            DropForeignKey("dbo.Photo", "PlaceId", "dbo.Place");
            DropForeignKey("dbo.Photo", "PhotoCollectionId", "dbo.PhotoCollection");
            DropIndex("dbo.Photo", new[] { "PlaceId" });
            DropIndex("dbo.Photo", new[] { "PhotoCollectionId" });
            DropIndex("dbo.PhotoCollection", new[] { "PlaceId" });
            DropTable("dbo.Place");
            DropTable("dbo.Photo");
            DropTable("dbo.PhotoCollection");
        }
    }
}
