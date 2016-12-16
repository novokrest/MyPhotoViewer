using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace MyPhotoViewer.ViewModels.User
{
    public class EditUserViewModel : BaseUserViewModel
    {
        [Display(Name = "Roles")]
        public IList<SelectItemViewModel> Roles { get; set; }
    }
}