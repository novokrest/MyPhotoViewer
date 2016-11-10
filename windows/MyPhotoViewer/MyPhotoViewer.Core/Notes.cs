using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPhotoViewer.Core
{
    class Notes
    {
        public string LoadImage(byte[] photo)
        {
            return "data:image/jpg;base64," + Convert.ToBase64String(photo);
        }
    }
}
