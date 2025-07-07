 #nullable disable
 using System;
 using System.Collections.Generic;
 using System.Linq;

 namespace TidyUtility.Core.Extensions
{
    public static class LinqExtensions
    {
        public static bool IsEmpty<TSource>(this IEnumerable<TSource> source)
        {
            return !source.Any();
        }

        public static bool IsEmpty<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            return !source.Any(predicate);
        }
    }
}
