using MyPhotoViewer.DAL;
using MyPhotoViewer.Extensions;
using MyPhotoViewer.ViewModels;
using System.Web.Mvc;

namespace MyPhotoViewer.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly IPhotoAlbumRepository _photoAlbumRepository = RepositoryServiceLocator.GetPhotoAlbumRepository();

        // GET: Albums
        public ActionResult Index()
        {
            var thumbnailsCreator = new PhotoAlbumThumnailCreator(_photoAlbumRepository.GetPhotoAlbums());
            var thumbnails = thumbnailsCreator.CreateThumbnails();
            return View(thumbnails);
        }

        // GET: Albums/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Albums/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Albums/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Title, Description, Place, City, Country, From, To, Photos")]NewPhotoAlbumViewModel photoAlbumViewModel)
        {
            if (ModelState.IsValid)
            {
                var photoAlbum = photoAlbumViewModel.ToPhotoAlbum();
                _photoAlbumRepository.AddPhotoAlbum(photoAlbum);
                return RedirectToAction("Index");
            }

            return View(photoAlbumViewModel);
        }

        // GET: Albums/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Albums/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Albums/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Albums/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
