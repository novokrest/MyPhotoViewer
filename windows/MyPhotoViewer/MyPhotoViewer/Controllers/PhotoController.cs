using MyPhotoViewer.DAL;
using MyPhotoViewer.ViewModels;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace MyPhotoViewer.Controllers
{
    public class PhotoController : Controller
    {
        private readonly IPhotoRepository _photoRepository = RepositoryServiceLocator.GetPhotoRepository();
        private readonly IPhotoAlbumRepository _photoCollectionRepository = RepositoryServiceLocator.GetPhotoAlbumRepository();

        // GET: Photo
        public ActionResult Index(int? id)
        {
            return RedirectToAction("Details", new { id = id });
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Photo photo = _photoRepository.GetPhotoById(id.Value);
            if (photo == null)
            {
                return HttpNotFound();
            }

            return View(photo);
        }

        public ActionResult Create()
        {
            var newPhotoViewModel = new NewPhotoViewModel
            {
                PhotoCollections = CreatePhotoCollectionSelectList()
            };

            return View(newPhotoViewModel);
        }

        private SelectList CreatePhotoCollectionSelectList()
        {
            var selectItems = _photoCollectionRepository.GetPhotoAlbums().Select(photoCollection => new SelectListItem()
            {
                Text = photoCollection.Title,
                Value = photoCollection.PhotoAlbumId.ToString()
            });

            return new SelectList(selectItems, "Value", "Text");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include ="Title, Path, ChoosedPhotoCollection")]NewPhotoViewModel photoViewModel)
        {
            if (ModelState.IsValid)
            {
                var photo = new Photo
                {
                    Title = photoViewModel.Title,
                    Path = photoViewModel.Path,
                    PhotoAlbumId = photoViewModel.ChoosedPhotoCollection
                };
                _photoRepository.InsertPhoto(photo);
                _photoRepository.Save();
                return RedirectToAction("Index");
            }

            photoViewModel.PhotoCollections = CreatePhotoCollectionSelectList();
            return View(photoViewModel);
        }
    }
}