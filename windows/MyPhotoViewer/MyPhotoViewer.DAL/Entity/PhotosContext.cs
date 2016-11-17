﻿using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace MyPhotoViewer.DAL.Entity
{
    public interface IPhotosContext
    {
        DbSet<PhotoAlbumEntity> PhotoAlbums { get; }
        DbSet<PhotoEntity> Photos { get; }
    }

    public class PhotosContext : DbContext, IPhotosContext
    {
        public DbSet<PhotoAlbumEntity> PhotoAlbums { get; set; }
        public DbSet<PhotoEntity> Photos { get; set; }
        public DbSet<PlaceEntity> Places { get; set; }

        public PhotosContext()
        {
            Initialize();
        }

        public PhotosContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            Initialize();
        }

        private void Initialize()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<PhotosContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new PhotoEntityConfiguration());
            modelBuilder.Configurations.Add(new PhotoAlbumEntityConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
