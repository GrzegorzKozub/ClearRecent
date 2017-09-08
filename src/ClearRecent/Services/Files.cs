using System.IO;

namespace ClearRecent.Services
{
    internal class Files
    {
        internal bool Missing(string path)
        {
            return !File.Exists(path);
        }
    }
}
