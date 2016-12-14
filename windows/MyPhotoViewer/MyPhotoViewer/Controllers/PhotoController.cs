using MyPhotoViewer.Core;
using MyPhotoViewer.DAL;
using MyPhotoViewer.Extensions;
using MyPhotoViewer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MyPhotoViewer.Controllers
{
    public class PhotoController : Controller
    {
        private readonly IPhotoRepository _photoRepository;
        private readonly IAlbumRepository _albumRepository;

        public PhotoController(IPhotoRepository photoRepository, IAlbumRepository albumRepository)
        {
            _photoRepository = photoRepository;
            _albumRepository = albumRepository;
        }

        [HttpGet]
        public ActionResult Create(int albumId = 0)
        {
            var photoViewModel = CreateEmptyPhotoViewModel();
            photoViewModel.AlbumId = albumId;

            return View(photoViewModel);
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "AlbumId, Title, Image, CreationDate")]NewPhotoViewModel photoViewModel)
        {
            if (ModelState.IsValid)
            {
                _photoRepository.AddPhoto(photoViewModel.ToPhotoEntity());
                return RedirectToAlbumBrowse(photoViewModel.AlbumId);
            }

            photoViewModel.Albums = CreateAlbumSelectList();
            return View(photoViewModel);
        }

        [HttpGet]
        public ActionResult Edit(int photoId)
        {
            var photoViewModel = CreatePhotoViewModel(photoId);

            return View(photoViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "PhotoId, AlbumId, Title, CreationDate")]PhotoViewModel photoViewModel)
        {
            if (ModelState.IsValid)
            {
                var updatablePhoto = _photoRepository.GetUpdatablePhotoById(photoViewModel.PhotoId);

                updatablePhoto.Title = photoViewModel.Title;
                updatablePhoto.CreationDate = photoViewModel.CreationDate;
                updatablePhoto.AlbumId = photoViewModel.AlbumId;

                _photoRepository.UpdatePhoto(updatablePhoto);

                return RedirectToAction("Photos", "Admin");
            }

            photoViewModel.Albums = CreateAlbumSelectList();
            return View(photoViewModel);
        }

        [HttpGet]
        public ActionResult Delete(int photoId)
        {
            var photoViewModel = CreatePhotoViewModel(photoId);

            return View(photoViewModel);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeletePost([Bind(Include = "PhotoId, Title")]PhotoViewModel photoViewModel)
        {
            if (ModelState.IsValid)
            {
                _photoRepository.DeletePhoto(photoViewModel.PhotoId);
                TempData["message-success"] = $"Photo '{photoViewModel.Title}' was deleted successfully";
                return RedirectToAction("Photos", "Admin");
            }

            TempData["message-alert"] = "Something goes wrong";
            return View(CreatePhotoViewModel(photoViewModel.PhotoId));
        }

        private ActionResult RedirectToAlbumBrowse(int albumId)
        {
            return RedirectToAction("Browse", "Album", new { albumId = albumId });
        }

        private PhotoViewModel CreatePhotoViewModel(int photoId)
        {
            var photo = _photoRepository.GetPhotoById(photoId);
            var photoViewModel = CreatePhotoViewModel(photo);

            return photoViewModel;
        }

        private PhotoViewModel CreatePhotoViewModel(IPhoto photo)
        {
            var photoViewModel = CreateEmptyPhotoViewModel();

            photoViewModel.PhotoId = photo.Id;
            photoViewModel.Title = photo.Title;
            photoViewModel.CreationDate = photo.CreationDate;
            photoViewModel.AlbumId = photo.AlbumId;
            photoViewModel.Albums = CreateAlbumSelectList();

            return photoViewModel;
        }

        private NewPhotoViewModel CreateEmptyPhotoViewModel()
        {
            return new NewPhotoViewModel
            {
                CreationDate = DateTime.Now,
                Albums = CreateAlbumSelectList()
            };
        }

        private IEnumerable<SelectListItem> CreateAlbumSelectList()
        {
            return _albumRepository.LoadAlbums()
                                        .Select(album => new SelectListItem
                                        { Text = album.Title, Value = album.Id.ToString() });
        }

        [HttpGet]
        public ActionResult Image(int photoId)
        {
            var image = _photoRepository.GetPhotoImage(photoId);

            return File(image.Data, ImageMimeTypeConverter.ToMimeType(image.Type));
        }
    }
}