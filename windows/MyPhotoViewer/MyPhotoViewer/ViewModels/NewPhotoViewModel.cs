using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using MyPhotoViewer.Core;
using MyPhotoViewer.ModelBinders;
using System.Collections.Generic;

namespace MyPhotoViewer.ViewModels
{
    public class NewPhotoViewModel : BasePhotoViewModel
    {
        [Required]
        [UploadedImage]
        public Image Image { get; set; }
    }
}