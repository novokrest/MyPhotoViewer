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
        private readonly IPhotoAlbumRepository _photoAlbumRepository;
        private readonly IPhotoRepository _photoRepository;
        private readonly IPlaceSelectListCreator _placeListCreator;

        public AlbumController(IPhotoAlbumRepository photoAlbumRepository, 
                               IPhotoRepository photoRepository, 
                               IPlaceSelectListCreator placeListCreator)
        {
            _photoAlbumRepository = photoAlbumRepository;
            _photoRepository = photoRepository;
            _placeListCreator = placeListCreator;
        }

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

        public ActionResult Thumbnail(int photoAlbumId, int photoId)
        {
            var photoImage = _photoRepository.GetPhotoById(photoId).GetImage();

            return base.File(photoImage.Data, ImageMimeTypeConverter.ToMimeType(photoImage.Type));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int photoAlbumId)
        {
            var photoAlbum = _photoAlbumRepository.GetPhotoAlbumById(photoAlbumId);
            var photoAlbumViewModel = PhotoAlbumViewModel.FromPhotoAlbum(photoAlbum);
            photoAlbumViewModel.Places = _placeListCreator.CreatePlaceList();

            return View(photoAlbumViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind]PhotoAlbumViewModel photoAlbumViewModel)
        {
            if (ModelState.IsValid)
            {
                var photoAlbum = photoAlbumViewModel.ToPhotoAlbum();
                _photoAlbumRepository.UpdatePhotoAlbum(photoAlbum);

                TempData["message"] = $"Album '{photoAlbum.Title}' has been edited successfully";
                return RedirectToAction("Index", "Admin");
            }

            photoAlbumViewModel.Places = _placeListCreator.CreatePlaceList();
            return View(photoAlbumViewModel);
        }
    }
}