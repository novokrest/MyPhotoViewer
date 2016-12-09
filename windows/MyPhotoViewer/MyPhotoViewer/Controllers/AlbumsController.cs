using MyPhotoViewer.Core;
using MyPhotoViewer.DAL;
using MyPhotoViewer.Extensions;
using MyPhotoViewer.ViewModels;
using System.Web.Mvc;

namespace MyPhotoViewer.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly IPhotoAlbumRepository _photoAlbumRepository = RepositoryServiceLocator.GetPhotoAlbumRepository();

        public ActionResult Index()
        {
            var thumbnailsCreator = new PhotoAlbumThumbnailCreator(_photoAlbumRepository.GetPhotoAlbums());
            var thumbnails = thumbnailsCreator.CreateThumbnails();
            return View(thumbnails);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "Title, Description, Place, City, Country, From, To, Photos")]NewPhotoAlbumViewModel photoAlbumViewModel)
        {
            if (ModelState.IsValid)
            {
                var photoAlbum = photoAlbumViewModel.ToPhotoAlbum();
                _photoAlbumRepository.AddPhotoAlbum(photoAlbum);
                return RedirectToAction("Index");
            }

            return View(photoAlbumViewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int photoAlbumId)
        {
            IPhotoAlbum photoAlbum = null;

            try
            {
                photoAlbum = _photoAlbumRepository.GetPhotoAlbumById(photoAlbumId);
            }
            catch
            {
                ModelState.AddModelError("", "Failed to obtain album");
                return View();
            }

            var photoAlbumViewModel = PhotoAlbumViewModel.FromPhotoAlbum(photoAlbum);
            return View(photoAlbumViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete([Bind(Include = "Id")]PhotoAlbumViewModel photoAlbum)
        {
            try
            {
                _photoAlbumRepository.RemovePhotoAlbumById(photoAlbum.Id);

                TempData["message"] = $"Album '{photoAlbum.Title}' has been deleted successfully";
                return RedirectToAction("Index", "Admin");
            }
            catch
            {
                ModelState.AddModelError("", "Failed to delete album");
                return View();
            }
        }
    }
}
