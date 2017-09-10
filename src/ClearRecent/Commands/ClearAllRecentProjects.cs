using Microsoft.VisualStudio.Shell;

namespace ClearRecent.Commands
{
    internal sealed class ClearAllRecentProjects : Command
    {
        internal ClearAllRecentProjects(Package package) :
            base(
                package,
                0x0102,
                "Remove all Recent Projects and Solutions from File menu and Start Page?")
        { }

        protected override bool Enabled() =>
            fileMenuRecents.ProjectsFound() || startPageRecents.ProjectsFound();

        protected override void Execute()
        {
            fileMenuRecents.ClearAllProjects();
            startPageRecents.ClearAllProjects();
        }
    }
}
