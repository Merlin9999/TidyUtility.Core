 #nullable disable
 using System;
 using System.Collections.Generic;
 using System.Globalization;

 namespace TidyUtility.Core.Extensions
{
    public static class EnumExtensions
    {
        // Adapted bit-fu from: http://realtimecollisiondetection.net/blog/?p=78
        public static IEnumerable<T> AsSeparatedFlags<T>(this T value)
            where T : struct, Enum
        {
            long valueLong = Convert.ToInt64(value, CultureInfo.InvariantCulture);
            while (valueLong != 0)
            {
                yield return (T)Enum.ToObject(typeof(T), valueLong & -valueLong); // extract lowest bit set
                valueLong &= (valueLong - 1); // strip off lowest bit set
            }
        }

        //// Adapted from answers on: https://stackoverflow.com/q/4171140/677612
        //public static IEnumerable<T> GetFlags<T>(this T value) 
        //    where T : struct, Enum
        //{
        //    return Enum.GetValues<T>()
        //        .Where(member => (ulong)(object)member != 0)
        //        .Where(member => value.HasFlag(member));
        //}

        //// Adapted from answers on: https://stackoverflow.com/q/4171140/677612
        //public static IEnumerable<T> GetIndividualFlags<T>(this T value)
        //    where T : struct, Enum
        //{
        //    ulong valueLong = Convert.ToUInt64(value, CultureInfo.InvariantCulture);
        //    foreach (var enumValue in value.GetType().GetEnumValues())
        //    {
        //        if (
        //            enumValue is T flag // cast enumValue to T
        //            && Convert.ToUInt64(flag, CultureInfo.InvariantCulture) is var bitValue // convert flag to ulong
        //            && (bitValue & (bitValue - 1)) == 0 // is this a single-bit value?
        //            && (valueLong & bitValue) != 0 // is the bit set?
        //        )
        //        {
        //            yield return flag;
        //        }
        //    }
        //}
    }
}
