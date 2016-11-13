using System.Collections.Generic;

namespace MyPhotoViewer.Core
{
    public interface IPhotoAlbum
    {
        int Id { get; }
        string Title { get; }
        string Description { get; }
        IDateTimePeriod Period { get; }
        IPlace Place { get; }

        IReadOnlyCollection<int> GetPhotoIds();
        Image GetPhotoImage(int photoId);
    }
}
