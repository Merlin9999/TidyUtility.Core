 #nullable disable
  namespace TidyUtility.Core.Extensions
{
    public static class IntegerExtensions
    {
        public static string ToOrdinalSuffix(this long value)
        {
            return ToOrdinalSuffixImpl((int) value % 100);
        }

        public static string ToOrdinalSuffix(this ulong value)
        {
            return ToOrdinalSuffixImpl((int)value % 100);
        }
        public static string ToOrdinalSuffix(this uint value)
        {
            return ToOrdinalSuffixImpl((int)value % 100);
        }

        public static string ToOrdinalSuffix(this int value)
        {
            return ToOrdinalSuffixImpl((int)value % 100);
        }

        // Adapted from: http://csharphelper.com/blog/2014/11/convert-an-integer-into-an-ordinal-in-c/
        private static string ToOrdinalSuffixImpl(this int lastDigits)
        {
            // Start with the most common extension.
            string extension = "th";

            // If the last digits are 11, 12, or 13, use th. Otherwise:
            if (lastDigits < 11 || lastDigits > 13)
            {
                // Check the last digit.
                switch (lastDigits % 10)
                {
                    case 1:
                        extension = "st";
                        break;
                    case 2:
                        extension = "nd";
                        break;
                    case 3:
                        extension = "rd";
                        break;
                }
            }

            return extension;
        }
    }
}
