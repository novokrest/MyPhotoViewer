using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace MyPhotoViewer.DAL.Entity
{
    public interface IPhotosDbContext
    {
        DbSet<AlbumEntity> Albums { get; }
        DbSet<PhotoEntity> Photos { get; }
        DbSet<PlaceEntity> Places { get; }

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        int SaveChanges();
    }

    public class PhotosDbContext : DbContext, IPhotosDbContext
    {
        public DbSet<AlbumEntity> Albums { get; set; }
        public DbSet<PhotoEntity> Photos { get; set; }
        public DbSet<PlaceEntity> Places { get; set; }

        public PhotosDbContext()
        {
        }

        public PhotosDbContext(string nameOrConnectionString)
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
