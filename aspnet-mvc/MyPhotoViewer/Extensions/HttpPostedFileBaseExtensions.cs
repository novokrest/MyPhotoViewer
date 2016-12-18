using MyPhotoViewer.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MyPhotoViewer.Extensions
{
    public static class HttpPostedFileBaseExtensions
    {
        public static byte[] ReadData(this HttpPostedFileBase file)
        {
            Stream inputStream = file.InputStream;
            int length = file.ContentLength;
            return inputStream.ReadData(length);
        }
    }
}