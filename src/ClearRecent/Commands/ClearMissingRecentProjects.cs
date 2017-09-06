using System;
using Microsoft.VisualStudio.Shell;

namespace ClearRecent.Commands
{
    internal sealed class ClearMissingRecentProjects : Command
    {
        internal ClearMissingRecentProjects(Package package) :
            base(package, 0x0103)
        { }

        protected override void MenuItemCallback(object sender, EventArgs e)
        { }
    }
}
