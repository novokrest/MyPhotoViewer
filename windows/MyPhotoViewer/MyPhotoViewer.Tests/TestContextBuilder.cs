using MyPhotoViewer.DAL;
using MyPhotoViewer.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MyPhotoViewer.Tests
{
    public class TestContextBuilder
    {
        private readonly List<AlbumEntity> _albums = new List<AlbumEntity>();

        public TestContextBuilder AddDefaultAlbum(int id, int minPhotoId = 0, int maxPhotoId = 1)
        {
            var albumEntity = new AlbumEntity
            {
                Id = id,
                Title = $"Album{id}",
                Description = $"Description{id}",
                Period = new DateTimePeriod { From = DateTime.Now, To = DateTime.Now.AddDays(1) },
                PlaceId = id,
                Place = new PlaceEntity { Id = id, Name = $"Place{id}", City = $"City{id}", Country = $"Country{id}" },
                Photos = Enumerable.Range(minPhotoId, maxPhotoId - minPhotoId)
                                   .Select(photoId =>
                                    new PhotoEntity
                                    {
                                        Id = photoId,
                                        Title = $"Title{photoId}",
                                        Image = new byte[0],
                                        ImageType = Core.ImageType.Jpeg,
                                        CreationDate = DateTime.Now.AddHours(1)
                                    })
                                   .ToList()
            };

            return AddAlbum(albumEntity);
        }

        public TestContextBuilder AddAlbum(AlbumEntity album)
        {
            _albums.Add(album);
            return this;
        }

        public TestContext Build()
        {
            var dbInitializer = new TestPhotosDbInitializer(_albums);
            Database.SetInitializer(dbInitializer);

            PhotosDbContext context = null;
            try
            {
                context = new PhotosDbContext();
                context.Database.Initialize(force: true);
                return new TestContext(context);
            }
            catch(Exception)
            {
                context?.Dispose();
                throw;
            }
        }

        private class TestPhotosDbInitializer : DropCreateDatabaseAlways<PhotosDbContext>
        {
            private readonly IEnumerable<AlbumEntity> _albums;

            public TestPhotosDbInitializer(IEnumerable<AlbumEntity> albums)
            {
                _albums = albums;
            }

            protected override void Seed(PhotosDbContext context)
            {
                context.Albums.AddRange(_albums);
                context.SaveChanges();
            }
        }
    }
}
