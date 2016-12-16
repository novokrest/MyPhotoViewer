using MyPhotoViewer.Models;
using System;

namespace MyPhotoViewer.ViewModels.User
{
    public class UserViewModelCreator
    {
        public static T Create<T>(Func<T> userViewModelFactory, ApplicationUser user, Action<T> initializer)
            where T : BaseUserViewModel
        {
            var userViewModel = userViewModelFactory();

            userViewModel.UserId = user.Id;
            userViewModel.UserName = user.UserName;
            userViewModel.Email = user.Email;
            userViewModel.FirstName = user.FirstName;
            userViewModel.LastName = user.LastName;

            initializer(userViewModel);

            return userViewModel;
        }
    }
}