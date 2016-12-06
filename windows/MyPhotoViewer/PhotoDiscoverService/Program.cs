using MyPhotoViewer.DAL.Entity;
using MyPhotoViewer.Models;
using System;

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

            DatabaseInitializer.Initialize<ApplicationDbContext, ApplicationDbInitializer>();
            DatabaseInitializer.Initialize<PhotosDbContext, PhotosDbInitializer>();
        }
    }
}
