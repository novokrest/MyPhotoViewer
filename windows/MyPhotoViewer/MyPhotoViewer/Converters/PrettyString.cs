using MyPhotoViewer.Core;
using System.Linq;

namespace MyPhotoViewer.Converters
{
    public static class PrettyString
    {
        public static string ToString(IPlace place)
        {
            return string.Join(", ", new[] { place.Name, place.City, place.Country }.Where(str => !string.IsNullOrEmpty(str)));
        }
    }
}