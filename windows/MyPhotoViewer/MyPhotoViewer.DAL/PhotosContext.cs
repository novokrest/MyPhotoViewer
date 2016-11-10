using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace MyPhotoViewer.DAL
{
    public class PhotosContext : DbContext
    {
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<PhotoAlbum> PhotoAlbums { get; set; }

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
            Database.SetInitializer(new CreateDatabaseIfNotExists<PhotosContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new PhotoConfiguration());
            modelBuilder.Configurations.Add(new PhotoAlbumConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
