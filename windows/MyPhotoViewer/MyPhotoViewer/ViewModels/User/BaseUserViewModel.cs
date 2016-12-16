using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MyPhotoViewer.ViewModels.User
{
    public class BaseUserViewModel
    {
        [Required]
        [HiddenInput(DisplayValue = true)]
        public string UserId { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string UserName { get; set; }

        [Display(Name = "E-mail")]
        [EmailAddress(ErrorMessage = "Provide valid e-mail")]
        public string Email { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

    }
}