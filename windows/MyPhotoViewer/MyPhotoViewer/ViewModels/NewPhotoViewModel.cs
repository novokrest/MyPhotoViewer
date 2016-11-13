using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using MyPhotoViewer.Core;
using MyPhotoViewer.ModelBinders;

namespace MyPhotoViewer.ViewModels
{
    public class NewPhotoViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int PhotoAlbumId { get; set; }

        [Display(Name = "Title")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Photo's title must be between 2 and 50 characters")]
        public string Title { get; set; }

        [UploadedImage]
        public Image Image { get; set; }
    }
}