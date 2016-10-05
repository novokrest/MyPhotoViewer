using System.Data.Entity;

namespace MyPhotoViewer.Models
{
    public interface IPhotoCollectionEnitites
    {
        DbSet<PhotoCollection> PhotoCollections { get; }
        DbSet<Photo> Photos { get; }
        DbSet<Place> Places { get; }
    }

    public class PhotoCollectionEntities : DbContext, IPhotoCollectionEnitites
    {
        public DbSet<PhotoCollection> PhotoCollections { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Place> Places { get; set; }
    }
}