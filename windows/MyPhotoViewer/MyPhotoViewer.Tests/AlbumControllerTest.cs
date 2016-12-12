using MyPhotoViewer.Core.Extensions;
using MyPhotoViewer.Controllers;
using MyPhotoViewer.ViewModels;
using MyPhotoViewer.ViewModels.Album;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MyPhotoViewer.Core;
using MyPhotoViewer.DAL.Entity;

namespace MyPhotoViewer.Tests
{
    [TestFixture]
    public class AlbumControllerTest
    {
        [Test]
        public void TestIndexGet_Given_ContextWithAlbums_Should_CreateThumbnails()
        {
            var testContext = new TestContextBuilder().AddDefaultAlbum(1, 1, 5)
                                                      .AddDefaultAlbum(2, 5, 10)
                                                      .Build();
            var albumController = new AlbumController(testContext.AlbumRepository, testContext.PhotoRepository, null);

            var result = albumController.Index() as ViewResult;

            Assert.NotNull(result);
            Assert.IsEmpty(result.ViewName);

            var model = result.Model as IReadOnlyList<IAlbumThumbnail>;

            Assert.NotNull(model);
            Assert.IsTrue(model.Count == 2);

            Assert.IsTrue(model[0].CoverPhotoId < 5 && model[0].CoverPhotoId >= 1);
            Assert.IsTrue(model[1].CoverPhotoId < 10 && model[1].CoverPhotoId >= 5);
        }

        [Test]
        public void TestBrowseGet_Given_AlbumWithPhotos_Should_CreateViewModelWithPhotos()
        {
            var testContext = new TestContextBuilder().AddDefaultAlbum(1, 1, 5).Build();
            var albumController = new AlbumController(testContext.AlbumRepository, testContext.PhotoRepository, null);

            var result = albumController.Browse(albumId: 1) as ViewResult;

            Assert.NotNull(result);
            Assert.IsEmpty(result.ViewName);

            var model = result.Model as AlbumWithPhotosViewModel;
            Assert.IsNotNull(model);
            Assert.IsTrue(model.Photos.Count() == 4);
        }

        [Test]
        public void TestCreateGet_Should_ReturnNewViewModelWithDefaultPeriod()
        {
            var testContext = new TestContextBuilder().Build();
            var albumController = new AlbumController(testContext.AlbumRepository, testContext.PhotoRepository, null);

            DateTime beforeCreate = DateTime.Now;
            var result = albumController.Create() as ViewResult;
            DateTime afterCreate = DateTime.Now;

            Assert.NotNull(result);
            Assert.IsEmpty(result.ViewName);

            var model = result.Model as NewAlbumViewModel;

            Assert.NotNull(model);
            Assert.IsTrue(model.From.HasValue && model.To.HasValue 
                       && model.From.Value == model.To.Value
                       && model.From.Value.IsBetween(beforeCreate, afterCreate)
                       && model.To.Value.IsBetween(beforeCreate, afterCreate));
        }

        [Test]
        public void TestCreatePost_GivenFilledNewNiewModel_Should_CreateAndSaveNewAlbum()
        {
            TestContext testContext = new TestContextBuilder().Build();
            var albumController = new AlbumController(testContext.AlbumRepository, testContext.PhotoRepository, null);
            var newAlbumViewModel = new NewAlbumViewModel
            {
                Id = 1,
                Title = $"Title1",
                Description = "Description1",
                From = DateTime.Now, To = DateTime.Now,
                Place = "Place1", City = "City1", Country = "Country1",
                Photos = new HttpFile[0]
            };

            var result = albumController.Create(newAlbumViewModel) as RedirectToRouteResult;

            Assert.NotNull(result);
            Assert.AreEqual(result.RouteValues["action"], "Index");
            Assert.IsTrue(testContext.Context.Albums.Count() == 1);
        }

        [Test]
        public void TestAddPhotoGet_Should_RedirectToPhotoController()
        {
            var albumController = new AlbumController(null, null, null);

            var result = albumController.AddPhoto(1) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.RouteValues["action"], "Create");
            Assert.AreEqual(result.RouteValues["controller"], "Photo");
            Assert.AreEqual(result.RouteValues["albumId"], 1);
        }

        [Test]
        public void TestEditGet_Should_CreateEditViewModelWithValidPhotosCount()
        {
            TestContext testContext = new TestContextBuilder().AddDefaultAlbum(1, 1, 5).Build();
            var albumController = new AlbumController(testContext.AlbumRepository, testContext.PhotoRepository, null);

            var result = albumController.Edit(1) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsEmpty(result.ViewName);

            var model = result.Model as EditAlbumViewModel;

            Assert.IsNotNull(model);
            Assert.IsTrue(model.Id == 1);
            Assert.IsTrue(model.PhotosCount == 4);
        }

        [Test]
        public void TestEditPost_Given_EditViewModelWithChanges_Should_UpdateRelatedAlbum()
        {
            int albumId = 1;
            TestContext testContext = new TestContextBuilder().AddDefaultAlbum(albumId, 1, 5).Build();
            var albumController = new AlbumController(testContext.AlbumRepository, testContext.PhotoRepository, null);
            string newTitle = "NewTitle1", newDescription = "NewDescription1", newPlace = "NewPlace1", newCity = "NewCity1", newCountry = "newCountry1";
            DateTime newFrom = new DateTime(2016, 1, 1), newTo = new DateTime(2016, 1, 2);
            var editAlbumViewModel = new EditAlbumViewModel
            {
                Id = 1,
                Title = newTitle,
                Description = newDescription,
                From = newFrom, To = newTo,
                Place = newPlace, City = newCity, Country = newCountry,
            };

            var result = albumController.Edit(editAlbumViewModel) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.RouteValues["action"], "Index");
            Assert.AreEqual(result.RouteValues["controller"], "Admin");

            AlbumEntity album = testContext.Context.Albums.First(a => a.Id == albumId);
            Assert.AreEqual(album.Title, newTitle);
            Assert.AreEqual(album.Description, newDescription);
            Assert.AreEqual(album.Place.Name, newPlace);
            Assert.AreEqual(album.Place.City, newCity);
            Assert.AreEqual(album.Place.Country, newCountry);
            Assert.AreEqual(album.Period.From, newFrom);
            Assert.AreEqual(album.Period.To, newTo);
        }

        [Test]
        public void TestDeleteGet_Should_CreateEditViewModel()
        {
            var testContext = new TestContextBuilder().AddDefaultAlbum(1).Build();
            var albumController = new AlbumController(testContext.AlbumRepository, testContext.PhotoRepository, null);

            var result = albumController.Delete(1) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsEmpty(result.ViewName);

            var model = result.Model as EditAlbumViewModel;
            Assert.IsNotNull(model);
            Assert.IsTrue(model.Id == 1);
        }

        [Test]
        public void TestDeletePost_Given_AlbumWithPhotos_Should_DeleteAlbum()
        {
            var testContext = new TestContextBuilder().AddDefaultAlbum(1).AddDefaultAlbum(2).Build();
            var albumController = new AlbumController(testContext.AlbumRepository, testContext.PhotoRepository, null);
            var albumEditViewModel = new EditAlbumViewModel { Id = 1 };

            var result = albumController.Delete(albumEditViewModel) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.RouteValues["action"], "Index");
            Assert.AreEqual(result.RouteValues["controller"], "Admin");
            Assert.IsTrue(testContext.Context.Albums.Count() == 1);
        }
    }
}
