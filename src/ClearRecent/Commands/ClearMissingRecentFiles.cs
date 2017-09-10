using Microsoft.VisualStudio.Shell;

namespace ClearRecent.Commands
{
    internal sealed class ClearMissingRecentFiles : Command
    {
        internal ClearMissingRecentFiles(Package package) :
            base(
                package,
                0x0101,
                "Remove Recent Files not found on disk from File menu?")
        { }

        protected override void Execute() =>
            fileMenuRecents.ClearMissingFiles();
    }
}
