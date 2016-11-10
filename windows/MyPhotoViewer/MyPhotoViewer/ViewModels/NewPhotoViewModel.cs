using MyPhotoViewer.Core;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MyPhotoViewer.ViewModels
{
    public class NewPhotoViewModel
    {
        [Display(Name = "Title")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Photo's title must be between 2 and 50 characters")]
        public string Title { get; set; }

        [Display(Name = "Path on disk")]
        [RegularExpression(KnownRegex.WindowsAbsoluteLocalFilePath)]
        public string Path { get; set; }

        [Display(Name = "Photo Album")]
        public int ChoosedPhotoCollection { get; set; }        

        public SelectList PhotoCollections { get; set; }
        public SelectList Places { get; set; }
    }
}