using System.IO;

namespace ClearRecent.Services
{
    internal class Files
    {
        internal bool Missing(string path) =>
            !File.Exists(path) && !Directory.Exists(path);
    }
}
