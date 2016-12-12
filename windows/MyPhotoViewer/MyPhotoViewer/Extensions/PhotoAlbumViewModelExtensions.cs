using MyPhotoViewer.Converters;
using MyPhotoViewer.DAL.Entity;
using MyPhotoViewer.ViewModels.Album;

namespace MyPhotoViewer.Extensions
{
    public static class AlbumViewModelExtensions
    {
        public static AlbumEntity ToAlbumEntity(this NewAlbumViewModel albumViewModel)
        {
            return AlbumEntityCreator.FromNewAlbumViewModel(albumViewModel);
        }

        public static AlbumEntity ToAlbumEntity(this EditAlbumViewModel albumViewModel)
        {
            return AlbumEntityCreator.FromEditAlbumViewModel(albumViewModel);
        }
    }
}
