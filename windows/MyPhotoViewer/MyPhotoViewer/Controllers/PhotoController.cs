using MyPhotoViewer.Core;
using MyPhotoViewer.DAL;
using MyPhotoViewer.Extensions;
using MyPhotoViewer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace MyPhotoViewer.Controllers
{
    public class PhotoController : Controller
    {
        private readonly IPhotoRepository _photoRepository;
        private readonly IAlbumRepository _photoAlbumRepository;

        public PhotoController(IPhotoRepository photoRepository, IAlbumRepository photoAlbumRepository)
        {
            _photoRepository = photoRepository;
            _photoAlbumRepository = photoAlbumRepository;
        }

        [HttpGet]
        public ActionResult Create(int photoAlbumId = 0)
        {
            var photoViewModel = CreateEmptyPhotoViewModel();
            photoViewModel.PhotoAlbumId = photoAlbumId;

            return View(photoViewModel);
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "PhotoAlbumId, Title, Image, CreationDate")]NewPhotoViewModel photoViewModel)
        {
            if (ModelState.IsValid)
            {
                _photoRepository.AddPhoto(photoViewModel.ToPhotoEntity());
                return RedirectToAlbumIndex(photoViewModel.PhotoAlbumId);
            }

            photoViewModel.PhotoAlbums = CreatePhotoAlbumSelectList();
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
        public ActionResult Edit([Bind(Include = "PhotoId, PhotoAlbumId, Title, CreationDate")]PhotoViewModel photoViewModel)
        {
            if (ModelState.IsValid)
            {
                var updatablePhoto = _photoRepository.GetUpdatablePhotoById(photoViewModel.PhotoId);

                updatablePhoto.Title = photoViewModel.Title;
                updatablePhoto.CreationDate = photoViewModel.CreationDate;
                updatablePhoto.PhotoAlbumId = photoViewModel.PhotoAlbumId;

                _photoRepository.UpdatePhoto(updatablePhoto);

                return RedirectToAction("Photos", "Admin");
            }

            photoViewModel.PhotoAlbums = CreatePhotoAlbumSelectList();
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

        private ActionResult RedirectToAlbumIndex(int photoAlbumId)
        {
            return RedirectToAction("Index", "Album", new { photoAlbumId = photoAlbumId });
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
            photoViewModel.PhotoAlbumId = photo.PhotoAlbumId;
            photoViewModel.PhotoAlbums = CreatePhotoAlbumSelectList();

            return photoViewModel;
        }

        private NewPhotoViewModel CreateEmptyPhotoViewModel()
        {
            return new NewPhotoViewModel
            {
                CreationDate = DateTime.Now,
                PhotoAlbums = CreatePhotoAlbumSelectList()
            };
        }

        private IEnumerable<SelectListItem> CreatePhotoAlbumSelectList()
        {
            return _photoAlbumRepository.LoadAlbums()
                                        .Select(photoAlbum => new SelectListItem
                                        { Text = photoAlbum.Title, Value = photoAlbum.Id.ToString() });
        }

        [HttpGet]
        public ActionResult Image(int photoId)
        {
            var image = _photoRepository.GetPhotoImage(photoId);

            return File(image.Data, ImageMimeTypeConverter.ToMimeType(image.Type));
        }
    }
}