using MyPhotoViewer.Core;
using System;

namespace MyPhotoViewer.ViewModels.Album
{
    public class AlbumViewModelCreator
    {
        public static EditAlbumViewModel CreateEditViewModel(IAlbum album)
        {
            return CreateViewModel(() => new EditAlbumViewModel(), album);
        }

        public static T CreateViewModel<T>(Func<T> albumViewModelCreator, IAlbum album, Action<T> initializer = null) 
            where T : BaseAlbumViewModel
        {
            var albumViewModel = albumViewModelCreator.Invoke();
            FillViewModel(albumViewModel, album);
            if (initializer != null)
            {
                initializer.Invoke(albumViewModel);
            }

            return albumViewModel;
        }

        public static void FillViewModel(BaseAlbumViewModel albumViewModel, IAlbum album)
        {
            albumViewModel.Id = album.Id;
            albumViewModel.Title = album.Title;
            albumViewModel.Description = album.Description;
            albumViewModel.Place = album.Place.Name;
            albumViewModel.City = album.Place.City;
            albumViewModel.Country = album.Place.Country;
            albumViewModel.From = album.Period.From;
            albumViewModel.To = album.Period.To;
        }
    }
}