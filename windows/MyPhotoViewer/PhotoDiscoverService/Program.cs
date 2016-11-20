using MyPhotoViewer.DAL.Entity;
using MyPhotoViewer.Models;

namespace PhotoDiscoverService
{
    class Program
    {
        static void Main(string[] args)
        {
            DatabaseInitializer.Initialize<ApplicationDbContext, ApplicationDbInitializer>();
            DatabaseInitializer.Initialize<PhotosContext, PhotosDbInitializer>();
        }
    }
}
