using MyPhotoViewer.Core;
using MyPhotoViewer.ModelBinders;
using MyPhotoViewer.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace MyPhotoViewer.ViewModels
{
    public class NewPhotoAlbumViewModel
    {
        [Required]
        [StringLength(30, MinimumLength = 5)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Place")]
        public string Place { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }

        [DataType(DataType.Date)]
        public DateTime? From { get; set; }

        [DataType(DataType.Date)]
        public DateTime? To { get; set; }

        [Images(ErrorMessage = "Select only valid images")]
        [UploadedFiles]
        [Display(Name = "Photos")]
        public ICollection<IHttpFile> Photos { get; set; }
    }
}
