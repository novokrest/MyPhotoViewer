using MyPhotoViewer.DAL;
using MyPhotoViewer.DAL.Entity;
using MyPhotoViewer.ViewModels;
using MyPhotoViewer.ViewModels.Album;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace MyPhotoViewer.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly IPhotoRepository _photoRepository;

        public AdminController(IAlbumRepository photoAlbumRepository, IPhotoRepository photoRepository)
        {
            _albumRepository = photoAlbumRepository;
            _photoRepository = photoRepository;
        }

        public AdminController(IAlbumRepository photoAlbumRepository)
        {
            _albumRepository = photoAlbumRepository;
        }

        [Route]
        public ActionResult Index()
        {
            var photoAlbums = _albumRepository.LoadAlbums();

            return View(photoAlbums);
        }

        [HttpGet]
        public ActionResult Albums()
        {
            var albumViewModels = _albumRepository.LoadAlbums()
                                                  .Select(AlbumViewModelCreator.CreateEditViewModel);

            return View(albumViewModels);
        }

        [Route("Edit/{photoAlbumId:int}")]
        public ActionResult Edit(int photoAlbumId)
        {
            var photoAlbum = _albumRepository.GetAlbumById(photoAlbumId);

            if (photoAlbum == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(photoAlbum);
        }

        [HttpPost]
        [Route("Edit/{photoAlbumId:int}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PhotoAlbumId, Title, Description, Period, Place")] AlbumEntity photoAlbum)
        {
            if (ModelState.IsValid)
            {
                _albumRepository.UpdateAlbum(photoAlbum);
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