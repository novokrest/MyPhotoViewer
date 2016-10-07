//using System.Data.Entity;
//using System.Linq;
//using System.Net;
//using System.Web.Mvc;
//using MyPhotoViewer.DAL;

//namespace MyPhotoViewer.Controllers
//{
//    public class PhotoManagerController : Controller
//    {
//        private readonly IPhotoCollectionRepository _photoCollectionRepository;

//        public PhotoManagerController()
//        {
//            _photoCollectionRepository = PhotoCollectionRepositoryCreator.CreatePhotoCollectionRepository();
//        }

//        // GET: PhotoManager
//        public ActionResult Index()
//        {
//            return View(_photoCollectionRepository.Photos.ToList());
//        }

//        // GET: PhotoManager/Details/5
//        public ActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Photo photo = _photoCollectionRepository.Photos.Find(id);
//            if (photo == null)
//            {
//                return HttpNotFound();
//            }
//            return View(photo);
//        }

//        // GET: PhotoManager/Create
//        public ActionResult Create()
//        {
//            ViewBag.PlaceId = new SelectList(_photoCollectionRepository.Places, "Id", "Name");
//            return View();
//        }

//        // POST: PhotoManager/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create([Bind(Include = "Id,Title,Date")] Photo photo)
//        {
//            if (ModelState.IsValid)
//            {
//                _photoCollectionRepository.Photos.Add(photo);
//                _photoCollectionRepository.SaveChanges();
//                return RedirectToAction("Index");
//            }

//            ViewBag.PlaceId = new SelectList(_photoCollectionRepository.Places, "Id", "Name");
//            return View(photo);
//        }

//        // GET: PhotoManager/Edit/5
//        public ActionResult Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Photo photo = _photoCollectionRepository.Photos.Find(id);
//            if (photo == null)
//            {
//                return HttpNotFound();
//            }
//            return View(photo);
//        }

//        // POST: PhotoManager/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit([Bind(Include = "Id,Title,Date")] Photo photo)
//        {
//            if (ModelState.IsValid)
//            {
//                _photoCollectionRepository.Entry(photo).State = EntityState.Modified;
//                _photoCollectionRepository.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            return View(photo);
//        }

//        // GET: PhotoManager/Delete/5
//        public ActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Photo photo = _photoCollectionRepository.Photos.Find(id);
//            if (photo == null)
//            {
//                return HttpNotFound();
//            }
//            return View(photo);
//        }

//        // POST: PhotoManager/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            Photo photo = _photoCollectionRepository.Photos.Find(id);
//            _photoCollectionRepository.Photos.Remove(photo);
//            _photoCollectionRepository.SaveChanges();
//            return RedirectToAction("Index");
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                _photoCollectionRepository.Dispose();
//            }
//            base.Dispose(disposing);
//        }
//    }
//}
