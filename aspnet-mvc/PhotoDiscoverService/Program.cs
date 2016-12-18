using MyPhotoViewer.DAL.Entity;
using MyPhotoViewer.Models;

namespace PhotoDiscoverService
{
    class Program
    {
        static void Main(string[] args)
        {
            var argsParser = new ArgsParser(args);

            if (argsParser.UpdateOnlyUsers)
            {
                DatabaseInitializer.Initialize<ApplicationDbContext, ApplicationDbInitializer>();
                return;
            }

            if (argsParser.UpdateOnlyPhotos)
            {
                DatabaseInitializer.Initialize<PhotosDbContext, PhotosDbInitializer>();
                return;
            }

            DatabaseInitializer.Initialize<ApplicationDbContext, ApplicationDbInitializer>();
            DatabaseInitializer.Initialize<PhotosDbContext, PhotosDbInitializer>();
        }
    }
}
