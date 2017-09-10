using Microsoft.VisualStudio.Shell;

namespace ClearRecent.Commands
{
    internal sealed class ClearMissingRecentProjects : Command
    {
        internal ClearMissingRecentProjects(Package package) :
            base(
                package,
                0x0103,
                "Remove Recent Projects and Solutions not found on disk from File menu and Start Page?")
        { }

        protected override void Execute()
        {
            fileMenuRecents.ClearMissingProjects();
            startPageRecents.ClearMissingProjects();
        }
    }
}
