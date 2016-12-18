using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPhotoViewer.Core
{
    public static class KnownRegex
    {
        public const string WindowsAbsoluteLocalFilePath = @"^[a-zA-Z]:(\\[a-zA-Z0-9_.-]+)+$";
    }
}
