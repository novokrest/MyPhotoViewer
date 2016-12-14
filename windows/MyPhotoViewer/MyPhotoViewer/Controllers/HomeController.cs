using System.Web.Mvc;
using MyPhotoViewer.DAL;
using MyPhotoViewer.DAL.Extensions;
using System.Collections.Generic;
using MyPhotoViewer.ViewModels.Album;

namespace MyPhotoViewer.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        private readonly IAlbumRepository _albumRepository;

        public HomeController(IAlbumRepository albumRepository)
        {
            _albumRepository = albumRepository;
        }

        public ActionResult Index()
        {
            var albumThumbnails = GetRandomAlbumsThumbnails(3);

            return View(albumThumbnails);
        }

        private IReadOnlyList<IAlbumThumbnail> GetRandomAlbumsThumbnails(int count)
        {
            var albums = _albumRepository.GetRandomAlbums(count);
            var albumThumbnailCreator = new AlbumThumbnailCreator(albums);

            return albumThumbnailCreator.CreateThumbnails();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}