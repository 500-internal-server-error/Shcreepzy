using System;
using System.Collections.Generic;

namespace Shcreepzy
{
    public static class Utils
    {
        // Fisher-Yates Shuffle
        // C#: https://stackoverflow.com/a/1262619
        // JS: https://stackoverflow.com/a/2450976
        public static void Shuffle<T>(this IList<T> list, Random rng)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        public static T Pop<T>(this IList<T> list)
        {
            T element = list[list.Count - 1];
            list.RemoveAt(list.Count - 1);
            return element;
        }
    }
}
