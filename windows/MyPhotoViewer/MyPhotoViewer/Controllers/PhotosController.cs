using MyPhotoViewer.DAL;
using MyPhotoViewer.Models;
using MyPhotoViewer.ViewModels;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MyPhotoViewer.Controllers
{
    public class PhotosController : Controller
    {
        private readonly IPhotoAlbumRepository _photoAlbumRepository = RepositoryServiceLocator.GetPhotoAlbumRepository();
        private readonly IPhotoRepository _photoRepository = RepositoryServiceLocator.GetPhotoRepository();
        private readonly PhotoAlbumThumbnailCreator _thumbnailCreator;

        public PhotosController()
        {
            _thumbnailCreator = new PhotoAlbumThumbnailCreator(_photoAlbumRepository.GetPhotoAlbums());
        }

        // GET: Store
        public ActionResult Index()
        {
            var photoCollectionThumbnails = _thumbnailCreator.CreateThumbnails();

            return View(photoCollectionThumbnails);
        }

        public ActionResult Image(string id)
        {
            throw new NotImplementedException();
            //int photoId = int.Parse(id);
            //var path = _photoRepository.GetPhotos().Where(photo => photo.PhotoId == photoId).Single().Path;
            //return base.File(path, "image/jpeg");
        }

        public ActionResult Browse(int id = 0)
        {
            var photoAlbum = _photoAlbumRepository.GetPhotoAlbumById(id);

            if (photoAlbum == null)
            {
                return RedirectToAction("Index");
            }

            return View(photoAlbum);
        }

        //public ActionResult BrowseCollection(string collection)
        //{
        //    var browsablePhotoCollection = photosDB.PhotoCollections
        //        .Include("Photos")
        //        .Include("Place")
        //        .Single(photoCollection => photoCollection.Name == collection);

        //    return View(browsablePhotoCollection);
        //}

        //public ActionResult Details(int id)
        //{
        //    var photo = photosDB.Photos.Find(id);

        //    return View(photo);
        //}
    }
}