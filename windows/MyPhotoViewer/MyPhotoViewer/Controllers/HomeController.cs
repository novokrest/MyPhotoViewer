using System.Web.Mvc;
using MyPhotoViewer.DAL;
using MyPhotoViewer.DAL.Extensions;
using MyPhotoViewer.ViewModels;
using System.Collections.Generic;

namespace MyPhotoViewer.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPhotoAlbumRepository _photoAlbumRepository = RepositoryServiceLocator.GetPhotoAlbumRepository();

        public ActionResult Index()
        {
            var photoAlbumThumbnails = GetRandomPhotoAlbumsThumbnails(3);

            return View(photoAlbumThumbnails);
        }

        private IReadOnlyList<IPhotoAlbumThumbnail> GetRandomPhotoAlbumsThumbnails(int count)
        {
            var photoAlbums = _photoAlbumRepository.GetRandomPhotoAlbums(count);
            var photoAlbumThumbnailCreator = new PhotoAlbumThumbnailCreator(photoAlbums);

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