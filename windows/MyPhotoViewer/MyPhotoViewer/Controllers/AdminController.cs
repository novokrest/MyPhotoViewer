using Microsoft.AspNet.Identity.Owin;
using MyPhotoViewer.Core;
using MyPhotoViewer.DAL;
using MyPhotoViewer.ViewModels.Album;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPhotoViewer.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly IPhotoRepository _photoRepository;
        private ApplicationSignInManager _signInManager;

        public AdminController(IAlbumRepository photoAlbumRepository, IPhotoRepository photoRepository)
        {
            _albumRepository = photoAlbumRepository;
            _photoRepository = photoRepository;
        }

        private ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? (_signInManager = HttpContext.GetOwinContext().Get<ApplicationSignInManager>());
            }
            set { _signInManager = value; }
        }

        [Route]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        public ActionResult Albums()
        {
            var albumViewModels = _albumRepository.LoadAlbums()
                                                  .Select(AlbumViewModelCreator.CreateEditViewModel);

            return View(albumViewModels);
        }

        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        public ActionResult Users()
        {
            var users = SignInManager.UserManager.Users.AsEnumerable();

            return View(users);
        }

        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        public ActionResult Photos()
        {
            var photos = _photoRepository.GetPhotos();
            return View(photos);
        }
    }
}