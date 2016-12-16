using MyPhotoViewer.Core;
using MyPhotoViewer.DAL;
using MyPhotoViewer.Extensions;
using MyPhotoViewer.ViewModels;
using MyPhotoViewer.ViewModels.Album;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MyPhotoViewer.Controllers
{
    public class AlbumController : Controller
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly IPhotoRepository _photoRepository;
        private readonly IPlaceSelectListCreator _placeListCreator;

        public AlbumController(IAlbumRepository albumRepository, 
                               IPhotoRepository photoRepository, 
                               IPlaceSelectListCreator placeListCreator)
        {
            _albumRepository = albumRepository;
            _photoRepository = photoRepository;
            _placeListCreator = placeListCreator;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var albums = _albumRepository.LoadAlbums();
            var thumbnailsCreator = new AlbumThumbnailCreator(albums);
            var thumbnails = thumbnailsCreator.CreateThumbnails();
            return View(thumbnails);
        }

        public ActionResult Browse(int albumId)
        {
            var album = _albumRepository.GetAlbumById(albumId);
            var photos = album.GetPhotoIds().Select(photoId => _photoRepository.GetPhotoById(photoId));

            var albumWithPhotosViewModel = AlbumViewModelCreator.CreateViewModel(() => new AlbumWithPhotosViewModel(), 
                                                                                 album, 
                                                                                 viewModel => viewModel.Photos = photos);

            return View(albumWithPhotosViewModel);
        }

        [HttpGet]
        [Authorize(Roles = Roles.UserAndAdmin)]
        public ActionResult Create()
        {
            DateTime now = DateTime.Now;
            var newAlbumViewModel = new NewAlbumViewModel
            {
                From = now,
                To = now
            };

            return View(newAlbumViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.UserAndAdmin)]
        public ActionResult Create([Bind(Include = "Title, Description, Place, City, Country, From, To, Photos")]
                                    NewAlbumViewModel newAlbumViewModel)
        {
            if (ModelState.IsValid)
            {
                var aAlbum = newAlbumViewModel.ToAlbumEntity();
                _albumRepository.AddAlbum(aAlbum);
                return RedirectToAction("Index");
            }

            newAlbumViewModel.Photos = null;
            
            return View(newAlbumViewModel);
        }

        [HttpGet]
        [Authorize(Roles = Roles.UserAndAdmin)]
        public ActionResult AddPhoto(int albumId)
        {
            return RedirectToAction("Create", "Photo", new { albumId = albumId });
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int albumId)
        {
            var album = _albumRepository.GetAlbumById(albumId);
            var viewModel = AlbumViewModelCreator.CreateEditViewModel(album);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind]EditAlbumViewModel albumViewModel)
        {
            if (ModelState.IsValid)
            {
                var albumEntity = albumViewModel.ToAlbumEntity();
                _albumRepository.UpdateAlbum(albumEntity);

                TempData["message"] = $"Album '{albumEntity.Title}' has been edited successfully";
                return RedirectToAction("Albums", "Admin");
            }

            return View(albumViewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int albumId)
        {
            var album = _albumRepository.GetAlbumById(albumId);
            var albumViewModel = AlbumViewModelCreator.CreateEditViewModel(album);

            return View(albumViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete([Bind(Include = "Id, Title, Description, Place, City, Country, From, To")]
                                   EditAlbumViewModel albumViewModel)
        {
            try
            {
                _albumRepository.RemoveAlbumById(albumViewModel.Id);
                TempData["message"] = $"Album '{albumViewModel.Title}' has been deleted successfully";
                return RedirectToAction("Albums", "Admin");
            }
            catch
            {
                ModelState.AddModelError("", "Failed to delete album");
            }

            return View(albumViewModel);
        }

        private T CreateAlbumViewModel<T>(Func<T> albumViewModelCreator, int albumId, Action<T> initializer = null)
            where T : BaseAlbumViewModel
        {
            var album = _albumRepository.GetAlbumById(albumId);
            return AlbumViewModelCreator.CreateViewModel(albumViewModelCreator, album, initializer);
        }
    }
}