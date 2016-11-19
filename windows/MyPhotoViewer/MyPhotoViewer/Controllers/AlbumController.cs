using MyPhotoViewer.Core;
using MyPhotoViewer.DAL;
using MyPhotoViewer.DAL.Entity;
using MyPhotoViewer.Extensions;
using MyPhotoViewer.ViewModels;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace MyPhotoViewer.Controllers
{
    public class AlbumController : Controller
    {
        private readonly IPhotoAlbumRepository _photoAlbumRepository = RepositoryServiceLocator.GetPhotoAlbumRepository();
        private readonly IPhotoRepository _photoRepository = RepositoryServiceLocator.GetPhotoRepository();

        // GET: PhotoAlbum
        public ActionResult Index(int photoAlbumId)
        {
            var photoAlbum = _photoAlbumRepository.GetPhotoAlbumById(photoAlbumId);
            var photos = photoAlbum.GetPhotoIds().Select(photoId => _photoRepository.GetPhotoById(photoId));

            var photoAlbumWithPhotosViewModel = new PhotoAlbumWithPhotosViewModel
            {
                PhotoAlbum = photoAlbum,
                Photos = photos
            };

            return View(photoAlbumWithPhotosViewModel);
        }

        [HttpGet]
        public ActionResult AddPhoto(int photoAlbumId)
        {
            return RedirectToAction("Create", "Photo", new { photoAlbumId = photoAlbumId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPhoto([Bind(Include = "PhotoAlbumId, Title, Image")]NewPhotoViewModel newPhotoViewModel)
        {
            if (ModelState.IsValid)
            {
                PhotoEntity photoEntity = newPhotoViewModel.ToPhotoEntity();
                _photoRepository.AddPhoto(photoEntity);
                return RedirectToAction("Index", new { photoAlbumId = newPhotoViewModel.PhotoAlbumId });
            }

            return View(newPhotoViewModel);
        }

        public ActionResult Photo(int photoAlbumId, int photoId)
        {
            var photo = _photoRepository.GetPhotoById(photoId);

            if (photo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Image image = photo.GetImage();

            return base.File(image.Data, ImageMimeTypeConverter.ToMimeType(image.Type));
        }

        public ActionResult Thumbnail(int photoAlbumId, int photoId)
        {
            var photoImage = _photoRepository.GetPhotoById(photoId).GetImage();

            return base.File(photoImage.Data, ImageMimeTypeConverter.ToMimeType(photoImage.Type));
        }
    }
}