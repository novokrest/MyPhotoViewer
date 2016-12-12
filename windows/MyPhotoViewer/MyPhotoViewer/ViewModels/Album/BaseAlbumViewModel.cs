using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPhotoViewer.ViewModels.Album
{
    public class BaseAlbumViewModel : IValidatableObject
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
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
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? From { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? To { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (From > To)
            {
                yield return new ValidationResult("Incorrect period");
            }
        }
    }
}