using MyPhotoViewer.DAL;
using MyPhotoViewer.Models;
using MyPhotoViewer.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace MyPhotoViewer.Controllers
{
    public class PhotosController : Controller
    {
        private readonly IPhotoAlbumRepository _photoCollectionRepository = RepositoryServiceLocator.GetPhotoAlbumRepository();
        private readonly IPhotoRepository _photoRepository = RepositoryServiceLocator.GetPhotoRepository();
        private readonly PhotoAlbumThumnailCreator _thumbnailCreator;

        public PhotosController()
        {
            _thumbnailCreator = new PhotoAlbumThumnailCreator(_photoCollectionRepository.GetPhotoAlbums());
        }

        // GET: Store
        public ActionResult Index()
        {
            var photoCollectionThumbnails = _thumbnailCreator.CreateThumbnails();

            return View(photoCollectionThumbnails);
        }

        public ActionResult Image(string id)
        {
            int photoId = int.Parse(id);
            var path = _photoRepository.GetPhotos().Where(photo => photo.PhotoId == photoId).Single().Path;
            return base.File(path, "image/jpeg");
        }

        public ActionResult Browse(int id = 0)
        {
            var photoCollection = _photoCollectionRepository.GetPhotoAlbums()
                                                            .Where(collection => collection.PhotoAlbumId == id)
                                                            .SingleOrDefault();
            if (photoCollection == null)
            {
                return RedirectToAction("Index");
            }

            return View(photoCollection);
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