using MyPhotoViewer.DAL;
using MyPhotoViewer.DAL.Entity;
using PhotoDiscoverService.Data;
using System;

namespace PhotoDiscoverService
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", "");

            var photoAlbums = PhotoAlbumsLoader.LoadPhotoAlbums();

            using (var photosContext = new PhotosContext())
            {
                photosContext.Configuration.AutoDetectChangesEnabled = false;
                photosContext.Configuration.ValidateOnSaveEnabled = false;

                photosContext.PhotoAlbums.AddRange(photoAlbums);
                photosContext.SaveChanges();
            }
        }
    }
}
