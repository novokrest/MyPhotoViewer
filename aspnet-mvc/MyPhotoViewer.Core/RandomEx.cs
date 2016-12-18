using System;
using System.Collections.Generic;
using System.Linq;


namespace MyPhotoViewer.Core
{
    public static class RandomEx
    {
        public static int Next(int maxValue)
        {
            return new Random().Next(maxValue);
        }

        public static IEnumerable<int> NonRepeatableRandoms(int minValue, int maxValue, int count)
        {
            var result = new List<int>();

            var values = Enumerable.Range(minValue, maxValue - minValue).ToList();
            count = Math.Min(maxValue - minValue, count);
            
            for (int i = 0; i < count; ++i)
            {
                int index = RandomEx.Next(values.Count);
                result.Add(values[index]);
                values.RemoveAt(index);
            }

            return result;
        }
    }
}
