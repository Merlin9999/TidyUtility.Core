 #nullable disable
 using System;
 using System.Collections.Specialized;
 using System.Linq;
 using System.Web;

 namespace TidyUtility.Core.Extensions
{
    public static class UriExtensions
    {
        public static Uri Combine(this Uri uri, params string[] pathAndQueryArray)
        {
            if (pathAndQueryArray.Length == 0)
                return uri;

            return pathAndQueryArray.Aggregate(uri, (u, p) => CombineUriSmart(u, p));
        }

        private static Uri CombineUriSmart(this Uri uri, string pathAndQuery)
        {
            return uri.LocalPath.EndsWith("/")
                ? (pathAndQuery.StartsWith("/")
                    ? CombineUri(uri, pathAndQuery.Substring(1))
                    : CombineUri(uri, pathAndQuery))
                : (pathAndQuery.StartsWith("/")
                    ? CombineUri(uri, pathAndQuery)
                    : CombineUri(uri, "/" + pathAndQuery));
        }

        private static Uri CombineUri(Uri uri, string pathAndQuery)
        {
            var builder = new UriBuilder(uri);
            int idxOfQuerySeparator = pathAndQuery.IndexOf('?');
            string pathToAppend = idxOfQuerySeparator > 0 ? pathAndQuery.Substring(0, idxOfQuerySeparator) : pathAndQuery;
            string queryToAppend = idxOfQuerySeparator > 0 ? pathAndQuery.Substring(idxOfQuerySeparator + 1) : string.Empty;

            builder.Path += pathToAppend;

            NameValueCollection queryParams = HttpUtility.ParseQueryString(builder.Query);
            NameValueCollection queryParamsToAppend = HttpUtility.ParseQueryString(queryToAppend);
            queryParams.Add(queryParamsToAppend);
            builder.Query = queryParams.ToString();

            return builder.Uri;
        }
    }
}
