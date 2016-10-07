using MyPhotoViewer.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MyPhotoViewer.Controllers
{
    public class PhotoController : Controller
    {
        private readonly IPhotoRepository _photoRepository = RepositoryServiceLocator.GetPhotoRepository();

        // GET: Photo
        public ActionResult Index(int? id)
        {
            return RedirectToAction("Details");
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
    }
}