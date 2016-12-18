using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MyPhotoViewer.ViewModels
{
    public class PhotoViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int PhotoId { get; set; }

        [Required]
        [Display(Name = "Album")]
        [HiddenInput(DisplayValue = false)]
        public int AlbumId { get; set; }

        [Required]
        [Display(Name = "Title")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Photo's title must be between 2 and 50 characters")]
        public string Title { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? CreationDate { get; set; }

        public IEnumerable<SelectListItem> Albums { get; set; }
    }
}