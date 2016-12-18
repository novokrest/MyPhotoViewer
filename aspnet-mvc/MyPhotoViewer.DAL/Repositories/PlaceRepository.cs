using MyPhotoViewer.Core;
using MyPhotoViewer.DAL.Entity;
using System.Collections.Generic;

namespace MyPhotoViewer.DAL.Repositories
{
    public interface IPlaceRepository
    {
        IEnumerable<IPlace> GetPlaces();
    }

    public class PlaceRepository : IPlaceRepository
    {
        private readonly PhotosDbContext _context;

        public PlaceRepository(PhotosDbContext context)
        {
            _context = context;
        }

        public IEnumerable<IPlace> GetPlaces()
        {
            return _context.Places;
        }
    }
}
