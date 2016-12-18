using System.ComponentModel.DataAnnotations;
using MyPhotoViewer.Core;
using MyPhotoViewer.ModelBinders;

namespace MyPhotoViewer.ViewModels
{
    public class NewPhotoViewModel : PhotoViewModel
    {
        [Required]
        [UploadedImage]
        public Image Image { get; set; }
    }
}