 #nullable disable
 using System;

 namespace TidyUtility.Core.Extensions
{
    public static class StringExtensions
    {
        // Adapted from: https://stackoverflow.com/a/4405876/677612
        public static string FirstCharToUpper(this string input, bool allowEmptyString = false) =>
            input switch
            {
                null => throw new ArgumentNullException(nameof(input)),
                "" => allowEmptyString
                    ? ""
                    : throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
                _ => string.Concat(input[0].ToString().ToUpper(), input.AsSpan(1))
            };
    }
}
