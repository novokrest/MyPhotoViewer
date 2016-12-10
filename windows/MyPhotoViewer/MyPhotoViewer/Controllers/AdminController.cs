using MyPhotoViewer.DAL;
using MyPhotoViewer.DAL.Entity;
using MyPhotoViewer.ViewModels;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace MyPhotoViewer.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IPhotoAlbumRepository _photoAlbumRepository;
        private readonly IPhotoRepository _photoRepository;

        public AdminController(IPhotoAlbumRepository photoAlbumRepository, IPhotoRepository photoRepository)
        {
            _photoAlbumRepository = photoAlbumRepository;
            _photoRepository = photoRepository;
        }

        public AdminController(IPhotoAlbumRepository photoAlbumRepository)
        {
            _photoAlbumRepository = photoAlbumRepository;
        }

        [Route]
        public ActionResult Index()
        {
            var photoAlbums = _photoAlbumRepository.GetPhotoAlbums();

            return View(photoAlbums);
        }

        [HttpGet]
        public ActionResult Albums()
        {
            var photoAlbums = _photoAlbumRepository.GetPhotoAlbums().Select(PhotoAlbumViewModel.FromPhotoAlbum);

            return View(photoAlbums);
        }

        [Route("Edit/{photoAlbumId:int}")]
        public ActionResult Edit(int photoAlbumId)
        {
            var photoAlbum = _photoAlbumRepository.GetPhotoAlbumById(photoAlbumId);

            if (photoAlbum == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(photoAlbum);
        }

        [HttpPost]
        [Route("Edit/{photoAlbumId:int}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PhotoAlbumId, Title, Description, Period, Place")] PhotoAlbumEntity photoAlbum)
        {
            if (ModelState.IsValid)
            {
                _photoAlbumRepository.SavePhotoAlbum(photoAlbum);
                TempData["message"] = $"Album '{photoAlbum.Title}' has been edited successfully";
                return RedirectToAction("Index");
            }

            return View(photoAlbum);
        }

        [HttpGet]
        public ActionResult Photos()
        {
            var photos = _photoRepository.GetPhotos();
            return View(photos);
        }
    }
}