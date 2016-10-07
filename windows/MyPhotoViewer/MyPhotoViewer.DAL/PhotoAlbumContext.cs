using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace MyPhotoViewer.DAL
{
    class PhotoAlbumContext : DbContext
    {
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<PhotoCollection> PhotoCollections { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<PhotoCollection>()
                        .HasRequired<Place>(p => p.Place)
                        .WithMany()
                        .WillCascadeOnDelete(false);
        }
    }
}
