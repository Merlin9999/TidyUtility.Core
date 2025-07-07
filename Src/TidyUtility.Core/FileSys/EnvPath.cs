 #nullable disable
 using System;
 using System.IO;

 namespace TidyUtility.Core.FileSys
{
    public class EnvPath
    {
        public static bool FileExists(string fileName)
        {
            return FindFile(fileName) != null;
        }

        public static string FindFile(string fileName)
        {
            if (File.Exists(fileName))
                return Path.GetFullPath(fileName);

            string envPath = Environment.GetEnvironmentVariable("PATH");
            foreach (var path in envPath.Split(Path.PathSeparator))
            {
                var fullPath = Path.Combine(path, fileName);
                if (File.Exists(fullPath))
                    return fullPath;
            }
            return null;
        }
    }
}
