using System.Collections.Generic;


namespace MyPhotoViewer.ViewModels.User
{
    public class DeleteUserViewModel : BaseUserViewModel
    {
        public ICollection<string> Roles { get; set; }
    }
}