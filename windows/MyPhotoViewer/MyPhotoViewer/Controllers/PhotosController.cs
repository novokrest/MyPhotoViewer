using MyPhotoViewer.Models;
using MyPhotoViewer.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace MyPhotoViewer.Controllers
{
    public class PhotosController : Controller
    {
        private readonly PhotoCollectionEntities _photosDb = new PhotoCollectionEntities();
        private readonly PhotoCollectionThumnailCreator _thumbnailCreator;

        public PhotosController()
        {
            _thumbnailCreator = new PhotoCollectionThumnailCreator(_photosDb);
        }

        // GET: Store
        public ActionResult Index()
        {
            var photoCollectionThumbnails = _thumbnailCreator.CreateThumbnails();

            return View(photoCollectionThumbnails);
        }

        public ActionResult Image(string id)
        {
            int parsedId = int.Parse(id);
            var path = _photosDb.Photos.Where(photo => photo.Id == parsedId).Single().Path;
            return base.File(path, "image/jpeg");
        }

        public ActionResult Browse(string id)
        {
            int photoCollectionId = int.Parse(id);
            var photoCollection = _photosDb.PhotoCollections.Include("Photos")
                                                            .Include("Place")
                                                            .Where(collection => collection.Id == photoCollectionId).Single();
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