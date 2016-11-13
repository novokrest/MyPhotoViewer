using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPhotoViewer.Core
{
    public static class RandomEx
    {
        public static int Next(int maxValue)
        {
            return new Random().Next(maxValue);
        }
    }
}
