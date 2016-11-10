using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MyPhotoViewer.DAL
{
    [MetadataType(typeof(PhotoMetadata))]
    public class Photo
    {
        public int PhotoId { get; set; }
        public byte[] Timestamp { get; set; }

        public string Title { get; set; }
        public byte[] Image { get; set; }
        public DateTime? CreationDate { get; set; }

        [ForeignKey("Place")]
        public int? PlaceId { get; set; }
        public virtual Place Place { get; set; }

        [ForeignKey("PhotoAlbum")]
        public int? PhotoAlbumId { get; set; }
        public virtual PhotoAlbum PhotoAlbum { get; set; }

        [NotMapped]
        public string Path { get; set; }
    }

    public class PhotoMetadata
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PhotoId { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [Column(TypeName = "image")]
        public byte[] Image { get; set; }
    }

    public class PhotoConfiguration : EntityTypeConfiguration<Photo>
    {
        public PhotoConfiguration()
        {
            this.HasKey(p => p.PhotoId);
            this.Property(p => p.PhotoId).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(p => p.Timestamp).IsRowVersion();
            this.Property(p => p.Title).IsRequired().HasMaxLength(50);
            this.Property(p => p.Image).IsRequired().HasColumnType("image");

            HasOptional(p => p.Place);
        }
    }
}
