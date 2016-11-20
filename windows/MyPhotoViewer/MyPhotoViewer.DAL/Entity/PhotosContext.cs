using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace MyPhotoViewer.DAL.Entity
{
    public interface IPhotosContext
    {
        DbSet<PhotoAlbumEntity> PhotoAlbums { get; }
        DbSet<PhotoEntity> Photos { get; }

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        int SaveChanges();
    }

    public class PhotosContext : DbContext, IPhotosContext
    {
        public DbSet<PhotoAlbumEntity> PhotoAlbums { get; set; }
        public DbSet<PhotoEntity> Photos { get; set; }
        public DbSet<PlaceEntity> Places { get; set; }

        public PhotosContext()
        {
        }

        public PhotosContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
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
