using System.ComponentModel.DataAnnotations;

namespace MyPhotoViewer.DAL
{
    [MetadataType(typeof(PlaceMetadata))]
    public class Place
    {
        public int PlaceId { get; set; }

        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }

    public class PlaceMetadata
    {
        [Key]
        public int PlaceId { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(30)]
        public string City { get; set; }

        [MaxLength(30)]
        public string Country { get; set; }
    }
}