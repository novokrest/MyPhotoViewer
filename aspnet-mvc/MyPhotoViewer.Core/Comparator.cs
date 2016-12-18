using System;

namespace MyPhotoViewer.Core
{
    public static class Comparator
    {
        public static TComparable Min<TComparable>(TComparable x, TComparable y)
            where TComparable : IComparable<TComparable>
        {
            return x.CompareTo(y) < 0 ? x : y;
        }

        public static TComparable Min<TComparable>(TComparable x, TComparable y, TComparable z)
            where TComparable : IComparable<TComparable>
        {
            return x.CompareTo(y) < 0 ? Min(x, z) : Min(y, z);
        }
    }
}