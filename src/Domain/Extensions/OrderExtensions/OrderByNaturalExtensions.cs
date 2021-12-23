﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace PlexRipper.Domain
{
    public static class OrderByNaturalExtensions
    {
        // From: https://github.com/postworthy/OrderByNatural/blob/master/OrderByNatural/OrderByNaturalExtensions.cs
        public static IEnumerable<T> OrderByNatural<T>(this IEnumerable<T> objects, Func<T, string> func)
        {
            object Convert(string str)
            {
                int x = 0;
                if (int.TryParse(str, out x)) return x;

                return str;
            }

            return objects.OrderBy(x => Regex.Split(func(x), "([0-9]+)").Select((Func<string, object>)Convert), new EnumerableComparer<object>());
        }

        public static IEnumerable<T> OrderByNaturalDesc<T>(this IEnumerable<T> objects, Func<T, string> func)
        {
            return objects.OrderByNatural(func).Reverse();
        }
    }
}