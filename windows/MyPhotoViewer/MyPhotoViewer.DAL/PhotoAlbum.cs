using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace MyPhotoViewer.DAL
{
    [MetadataType(typeof(PhotoAlbumMetadata))]
    public partial class PhotoAlbum
    {
        public int PhotoAlbumId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTimePeriod Period { get; set; }

        public int? PlaceId { get; set; }
        public virtual Place Place { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }
    }

    public class PhotoAlbumMetadata
    {
        [Required(ErrorMessage = "Please enter a photo album name")]
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Please enter a description")]
        public string Description { get; set; }
    }

    public class PhotoAlbumConfiguration : EntityTypeConfiguration<PhotoAlbum>
    {
        public PhotoAlbumConfiguration()
        {
            Property(pa => pa.Period.From).HasColumnName("From")
                                          .HasColumnType("datetime2")
                                          .HasPrecision(0);
            Property(pa => pa.Period.To).HasColumnName("To")
                                        .HasColumnType("datetime2")
                                        .HasPrecision(0);
        }
    }
}
