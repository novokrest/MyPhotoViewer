using MyPhotoViewer.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPhotoViewer.DAL.Entity
{
    [MetadataType(typeof(PlaceMetadata))]
    public class PlaceEntity : IPlace
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }

    [Table("Place")]
    public class PlaceMetadata
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(30)]
        public string City { get; set; }

        [MaxLength(30)]
        public string Country { get; set; }
    }
}