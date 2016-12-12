using System.ComponentModel.DataAnnotations;

namespace MyPhotoViewer.ViewModels.Album
{
    public class EditAlbumViewModel : BaseAlbumViewModel
    {
        [Display(Name = "Photos")]
        public int PhotosCount { get; set; }
    }
}