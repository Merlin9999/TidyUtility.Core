 #nullable disable
 using System;
 using System.Collections.Generic;

 namespace TidyUtility.Core.Extensions
{
    // Adapted from: https://stackoverflow.com/a/4870407/677612
    public static class ComparisonExtensions
    {
        public static IComparer<T> AsComparer<T>(this Comparison<T> comp)
        {
            if (comp == null)
                throw new System.ArgumentNullException(nameof(comp));
            return new ComparisonComparer<T>(comp);
        }

        public static IComparer<T> AsComparer<T>(this Func<T, T, int> func)
        {
            if (func == null)
                throw new System.ArgumentNullException(nameof(func));
            return new ComparisonComparer<T>((x, y) => func(x, y));
        }

        private class ComparisonComparer<T> : IComparer<T>
        {
            private Comparison<T> Comp { get; set; }

            public ComparisonComparer(Comparison<T> comp)
            {
                this.Comp = comp ?? throw new System.ArgumentNullException(nameof(comp));
            }

            public int Compare(T x, T y)
            {
                return this.Comp(x, y);
            }
        }
    }
}
