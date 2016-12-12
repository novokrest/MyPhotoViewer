using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPhotoViewer.Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool IsBetween(this DateTime current, DateTime leftBoundary, DateTime rightBoundary)
        {
            return current >= leftBoundary && current <= rightBoundary;
        }
    }
}
