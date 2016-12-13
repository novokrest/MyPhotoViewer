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
        private readonly IAlbumRepository _photoAlbumRepository = RepositoryServiceLocator.GetPhotoAlbumRepository();

        public ActionResult Index()
        {
            var photoAlbumThumbnails = GetRandomPhotoAlbumsThumbnails(3);

            return View(photoAlbumThumbnails);
        }

        private IReadOnlyList<IAlbumThumbnail> GetRandomPhotoAlbumsThumbnails(int count)
        {
            var photoAlbums = _photoAlbumRepository.GetRandomPhotoAlbums(count);
            var photoAlbumThumbnailCreator = new AlbumThumbnailCreator(photoAlbums);

            return photoAlbumThumbnailCreator.CreateThumbnails();
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