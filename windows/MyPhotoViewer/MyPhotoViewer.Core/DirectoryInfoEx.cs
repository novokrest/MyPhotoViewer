using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MyPhotoViewer.Core
{
    public static class DirectoryInfoEx
    {
        public static IEnumerable<string> GetChildDirectories(string directoryPath)
        {
            return new DirectoryInfo(directoryPath).GetDirectories()
                                                   .Select(directoryInfo => directoryInfo.FullName);
        }
    }
}
