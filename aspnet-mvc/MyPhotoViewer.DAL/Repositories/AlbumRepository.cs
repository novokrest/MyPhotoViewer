using MyPhotoViewer.Core;
using MyPhotoViewer.DAL.Entity;
using MyPhotoViewer.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyPhotoViewer.DAL
{
    public class AlbumRepository : IAlbumRepository, IDisposable
    {
        private readonly PhotosDbContext _context;

        public AlbumRepository(PhotosDbContext photosContext)
        {
            Verifiers.ArgNullVerify(photosContext, nameof(photosContext));
            _context = photosContext;
        }

        public IAlbum GetAlbumById(int albumId)
        {
            return new Album(_context, albumId);
        }

        public void RemoveAlbumById(int albumId)
        {
            var albumEntity = _context.Albums.Single(album => album.Id == albumId);
            _context.Albums.Remove(albumEntity);
            _context.SaveChanges();
        }

        public IEnumerable<IAlbum> LoadAlbums()
        {
            IEnumerable<int> albumIds = _context.Albums.Select(album => album.Id);
            return albumIds.Select(albumId => new Album(_context, albumId)).OrderBy(album => album.Id);
        }

        public void AddAlbum(AlbumEntity album)
        {
            _context.Albums.Add(album);
            _context.SaveChanges();
        }

        public void UpdateAlbum(AlbumEntity album)
        {
            ProvidePlaceExistence(album);
            _context.Entry(album).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }

        private void ProvidePlaceExistence(AlbumEntity album)
        {
            PlaceEntity place = _context.Places.FirstOrDefault(p => p.Id == album.PlaceId);
            if (place != null && PlaceEqualityComparer.AreEqual(album.Place, place))
            {
                return;
            }

            place = _context.Places.AsEnumerable()
                                   .Where(p => PlaceEqualityComparer.AreEqual(p, album.Place))
                                   .FirstOrDefault();
            if (place != null)
            {
                album.PlaceId = place.Id;
                album.Place = place;
                return;
            }

            PlaceEntity newPlace = new PlaceEntity { Name = album.Place.Name, City = album.Place.City, Country = album.Place.Country };
            newPlace = _context.Places.Add(newPlace);
            _context.SaveChanges();

            album.PlaceId = newPlace.Id;
            album.Place = null;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        private class PlaceEqualityComparer : IEqualityComparer<PlaceEntity>
        {
            public static bool AreEqual(PlaceEntity x, PlaceEntity y)
            {
                return new PlaceEqualityComparer().Equals(x, y);
            }

            public bool Equals(PlaceEntity x, PlaceEntity y)
            {
                return x.Name == y.Name && x.City == y.City && x.Country == y.Country;
            }

            public int GetHashCode(PlaceEntity place)
            {
                return string.Join(",", new[] { place.Name, place.City, place.Country }).GetHashCode();
            }
        }
    }
}
