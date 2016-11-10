using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPhotoViewer.DAL
{
    class PhotosContextInitializer : DropCreateDatabaseIfModelChanges<PhotosContext>
    {
        protected override void Seed(PhotosContext context)
        {
            var photoAlbums = new PhotoAlbum[]
            {
                new PhotoAlbum
                {
                    Title = "PhotoAlbum#1",
                    Description = "I am test photo album #1",
                    Period = new DateTimePeriod
                    {
                        From = DateTime.Now.AddDays(-3),
                        To = DateTime.Now.AddDays(-2)
                    },
                    Place =
                    {
                        Name = "Place#1",
                        City = "New-York",
                        Country = "USA"
                    },
                    Photos = new Photo[]
                    {
                        new Photo
                        {
                            Title = "Photo#1",
                            CreationDate = DateTime.Now.AddDays(-2),
                            Image = new byte[] { 1 }
                        }
                    }
                }
            };

            base.Seed(context);
        }
    }
}
