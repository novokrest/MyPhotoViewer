namespace MyPhotoViewer.DAL.Migrations
{
    using Core;
    using Data;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PhotoAlbumContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PhotoAlbumContext context)
        {
            var photoCollections = PhotoCollectionsLoader.LoadPhotoCollections();
            photoCollections.ForEach(photoCollection => context.PhotoCollections.AddOrUpdate(p => p.Name, photoCollection));
            context.SaveChanges();
        }
    }
}
