using MyPhotoViewer.Core;
using System.Web;

namespace MyPhotoViewer.Converters
{
    public class HttpFileCreator
    {
        public IHttpFile Create(HttpPostedFileBase file)
        {
            return new HttpFile
            {
                FileName = file.FileName,
                ContentType = file.ContentType,
                Data = file.InputStream.ReadData(file.ContentLength)
            };
        }
    }
}