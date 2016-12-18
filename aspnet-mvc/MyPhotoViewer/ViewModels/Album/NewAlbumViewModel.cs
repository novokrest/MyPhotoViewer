using MyPhotoViewer.Core;
using MyPhotoViewer.ModelBinders;
using MyPhotoViewer.Validators;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyPhotoViewer.ViewModels.Album
{
    public class NewAlbumViewModel : BaseAlbumViewModel
    {
        [UploadedFiles]
        [Display(Name = "Photos")]
        [Images(ErrorMessage = "Select only valid images")]
        public ICollection<IHttpFile> Photos { get; set; }
    }
}
