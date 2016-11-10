using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPhotoViewer.Core
{
    public interface IHttpFile
    {
        string FileName { get; }
        string ContentType { get; }
        byte[] Data { get; }
    }

    public class HttpFile : IHttpFile
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
    }
}
