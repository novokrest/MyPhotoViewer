﻿using MyPhotoViewer.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MyPhotoViewer.Controllers
{
    [Authorize]
    [RoutePrefix("Admin/Albums")]
    public class AdminController : Controller
    {
        private readonly IPhotoAlbumRepository _photoAlbumRepository;

        public AdminController()
            : this(RepositoryServiceLocator.GetPhotoAlbumRepository())
        {

        }

        public AdminController(IPhotoAlbumRepository photoAlbumRepository)
        {
            _photoAlbumRepository = photoAlbumRepository;
        }

        [Route]
        public ActionResult Index()
        {
            var photoAlbums = _photoAlbumRepository.GetPhotoAlbums();

            return View(photoAlbums);
        }

        [Route("Edit/{photoAlbumId:int}")]
        public ActionResult Edit(int photoAlbumId)
        {
            var photoAlbum = _photoAlbumRepository.GetPhotoAlbumById(photoAlbumId);

            if (photoAlbum == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(photoAlbum);
        }

        [HttpPost]
        [Route("Edit/{photoAlbumId:int}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PhotoAlbumId, Title, Description, Period, Place")] PhotoAlbum photoAlbum)
        {
            if (ModelState.IsValid)
            {
                _photoAlbumRepository.SavePhotoAlbum(photoAlbum);
                TempData["message"] = $"Album '{photoAlbum.Title}' has been edited successfully";
                return RedirectToAction("Index");
            }

            return View(photoAlbum);
        }

        //TODO: /Admin/Albums/Details
        public ActionResult Details(int photoAlbumId)
        {
            return View();
        }

        //TODO: /Admin/Albums/Delete
        public ActionResult Delete(int photoAlbumId)
        {
            return View();
        }

        //TODO: /Admin/Albums/Delete
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeletePost(int photoAlbumId)
        {
            return View();
        }
    }
}