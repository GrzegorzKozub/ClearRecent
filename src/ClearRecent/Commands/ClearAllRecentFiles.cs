using Microsoft.VisualStudio.Shell;

namespace ClearRecent.Commands
{
    internal sealed class ClearAllRecentFiles : Command
    {
        internal ClearAllRecentFiles(Package package) :
            base(
                package,
                0x0100,
                "Remove all Recent Files from File menu?")
        { }

        protected override bool Enabled() =>
            fileMenuRecents.FilesFound();

        protected override void Execute() =>
            fileMenuRecents.ClearAllFiles();
    }
}
