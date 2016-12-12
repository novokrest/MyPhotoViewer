using MyPhotoViewer.DAL;
using MyPhotoViewer.DAL.Entity;

namespace MyPhotoViewer.Tests
{
    public class TestContext
    {
        private readonly PhotosDbContext _context;
        private readonly AlbumRepository _albumRepository;
        private readonly PhotoRepository _photoRepository;

        public TestContext(PhotosDbContext context)
        {
            _context = context;
            _albumRepository = new AlbumRepository(context);
            _photoRepository = new PhotoRepository(context);
        }

        public PhotosDbContext Context => _context;
        public IAlbumRepository AlbumRepository => _albumRepository;
        public IPhotoRepository PhotoRepository => _photoRepository;
    }
}
