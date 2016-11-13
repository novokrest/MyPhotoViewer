using MyPhotoViewer.Core;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MyPhotoViewer.DAL.Entity
{
    [MetadataType(typeof(PhotoEntityMetadata))]
    public class PhotoEntity
    {
        public int Id { get; set; }

        public byte[] Timestamp { get; set; }

        public string Title { get; set; }

        public byte[] Image { get; set; }
        public ImageType? ImageType { get; set; }

        public DateTime? CreationDate { get; set; }

        [ForeignKey("Place")]
        public int? PlaceId { get; set; }
        public virtual PlaceEntity Place { get; set; }

        [ForeignKey("PhotoAlbum")]
        public int PhotoAlbumId { get; set; }
        public virtual PhotoAlbumEntity PhotoAlbum { get; set; }
    }

    [Table("Photo")]
    public class PhotoEntityMetadata
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [Column(TypeName = "image")]
        public byte[] Image { get; set; }

        [Required]
        [Column("Type")]
        public ImageType? ImageType { get; set; }
    }

    public class PhotoEntityConfiguration : EntityTypeConfiguration<PhotoEntity>
    {
        public PhotoEntityConfiguration()
        {
            this.HasKey(p => p.Id);
            this.Property(p => p.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(p => p.Timestamp).IsRowVersion();
            this.Property(p => p.Title).IsRequired().HasMaxLength(50);
            this.Property(p => p.Image).IsRequired();
            this.Property(p => p.ImageType).IsRequired();

            this.HasOptional(p => p.Place)
                .WithMany()
                .HasForeignKey(p => p.PlaceId);
            this.HasRequired(p => p.PhotoAlbum)
                .WithMany(pa => pa.Photos)
                .HasForeignKey(p => p.PhotoAlbumId);
        }
    }
}
