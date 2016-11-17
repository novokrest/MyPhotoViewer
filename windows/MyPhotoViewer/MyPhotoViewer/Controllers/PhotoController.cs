using MyPhotoViewer.Core;
using MyPhotoViewer.DAL;
using MyPhotoViewer.Extensions;
using MyPhotoViewer.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace MyPhotoViewer.Controllers
{
    public class PhotoController : Controller
    {
        private readonly IPhotoRepository _photoRepository;
        private readonly IPhotoAlbumRepository _photoAlbumRepository;

        public PhotoController(IPhotoRepository photoRepository, IPhotoAlbumRepository photoAlbumRepository)
        {
            _photoRepository = photoRepository;
            _photoAlbumRepository = photoAlbumRepository;
        }

        [HttpGet]
        public ActionResult Create(int? photoAlbumId)
        {
            var photoViewModel = new PhotoViewModel()
            {
                PhotoAlbumId = photoAlbumId ?? default(int),
                PhotoAlbums = CreatePhotoAlbumSelectList()
            };

            return View(photoViewModel);
        }

        private IEnumerable<SelectListItem> CreatePhotoAlbumSelectList()
        {
            return _photoAlbumRepository.GetPhotoAlbums()
                                        .Select(photoAlbum => new SelectListItem
                                        { Text = photoAlbum.Title, Value = photoAlbum.Id.ToString() });
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "PhotoAlbumId, Title, Image")]PhotoViewModel photoViewModel)
        {
            if (ModelState.IsValid)
            {
                _photoRepository.AddPhoto(photoViewModel.ToPhotoEntity());
                return RedirectToAction("Index", "Album", new { photoAlbumId = photoViewModel.PhotoAlbumId });
            }

            photoViewModel.PhotoAlbums = CreatePhotoAlbumSelectList();

            return View(photoViewModel);
        }

        [HttpGet]
        public ActionResult Image(int photoId)
        {
            var photo = _photoRepository.GetPhotoById(photoId);

            if (photo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Image image = photo.GetImage();

            return File(image.Data, ImageMimeTypeConverter.ToMimeType(image.Type));
        }
    }
}