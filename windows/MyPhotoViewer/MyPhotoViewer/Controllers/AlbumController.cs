using MyPhotoViewer.DAL;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace MyPhotoViewer.Controllers
{
    public class AlbumController : Controller
    {
        private readonly IPhotoAlbumRepository _photoAlbumRepository = RepositoryServiceLocator.GetPhotoAlbumRepository();
        private readonly IPhotoRepository _photoRepository = RepositoryServiceLocator.GetPhotoRepository();

        // GET: PhotoAlbum
        public ActionResult Index(int photoAlbumId)
        {
            var photoAlbum = _photoAlbumRepository.GetPhotoAlbumById(photoAlbumId);

            if (photoAlbum == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(photoAlbum);
        }

        public ActionResult Photo(int photoAlbumId, int photoId)
        {
            var photo = _photoRepository.GetPhotoById(photoId);

            if (photo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return base.File(photo.Image, "image/jpeg");
        }

        public ActionResult Thumbnail(int photoAlbumId)
        {
            var photo = _photoAlbumRepository.GetPhotoAlbumById(photoAlbumId)?.Photos.FirstOrDefault();

            if (photo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return base.File(photo.Image, "image/jpeg");
        }
    }
}