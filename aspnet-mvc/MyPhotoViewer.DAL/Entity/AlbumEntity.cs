using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MyPhotoViewer.DAL.Entity
{
    [MetadataType(typeof(AlbumEntityMetadata))]
    public partial class AlbumEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTimePeriod Period { get; set; }

        [ForeignKey("Place")]
        public int? PlaceId { get; set; }
        public virtual PlaceEntity Place { get; set; }

        public virtual ICollection<PhotoEntity> Photos { get; set; }
    }

    [Table("Album")]
    public class AlbumEntityMetadata
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a photo album name")]
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Please enter a description")]
        public string Description { get; set; }
    }

    public class AlbumEntityConfiguration : EntityTypeConfiguration<AlbumEntity>
    {
        public AlbumEntityConfiguration()
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
