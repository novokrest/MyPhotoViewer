using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using MyPhotoViewer.Core;
using MyPhotoViewer.ModelBinders;
using System.Collections.Generic;

namespace MyPhotoViewer.ViewModels
{
    public class PhotoViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int PhotoId { get; set; }

        [Required]
        [Display(Name = "Album")]
        [HiddenInput(DisplayValue = false)]
        public int PhotoAlbumId { get; set; }

        public IEnumerable<SelectListItem> PhotoAlbums { get; set; }

        [Required]
        [Display(Name = "Title")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Photo's title must be between 2 and 50 characters")]
        public string Title { get; set; }

        [Required]
        [UploadedImage]
        public Image Image { get; set; }
    }
}