using MyPhotoViewer.Core;
using MyPhotoViewer.DAL;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MyPhotoViewer.ViewModels
{
    public class PhotoAlbumWithPhotosViewModel
    {
        public IPhotoAlbum PhotoAlbum { get; set; }
        public IEnumerable<IPhoto> Photos { get; set; }
    }

    public class PhotoAlbumViewModel
    {
        public static PhotoAlbumViewModel FromPhotoAlbum(IPhotoAlbum photoAlbum)
        {
            return new PhotoAlbumViewModel
            {
                Id = photoAlbum.Id,
                Title = photoAlbum.Title,
                Description = photoAlbum.Description,
                Period = new DateTimePeriod(photoAlbum.Period),
                PlaceId = photoAlbum.Place.Id,
                Place = photoAlbum.Place,
                PhotosCount = photoAlbum.GetPhotoIds().Count
            };
        }

        [Required]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Title { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [Display(Name = "Period")]
        public DateTimePeriod Period { get; set; }

        [Display(Name = "Photos Count")]
        public int PhotosCount { get; set; }

        [Display(Name = "Place")]
        public int PlaceId { get; set; }
        public IPlace Place { get; set; }
        public SelectList Places { get; set; }
    }
}