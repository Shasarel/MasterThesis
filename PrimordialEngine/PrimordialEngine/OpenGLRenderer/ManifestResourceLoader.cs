using System.IO;

namespace PrimordialEngine
{
    public static class ManifestResourceLoader
    {
        public static string LoadTextFile(string textFileName)
        {
            using var sr = new StreamReader(textFileName);
            return sr.ReadToEnd(); ;
        }
    }
}