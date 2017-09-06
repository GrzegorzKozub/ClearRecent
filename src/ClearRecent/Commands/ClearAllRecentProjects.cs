using System;
using Microsoft.VisualStudio.Shell;

namespace ClearRecent.Commands
{
    internal sealed class ClearAllRecentProjects : Command
    {
        internal ClearAllRecentProjects(Package package) :
            base(package, 0x0102)
        { }

        protected override void MenuItemCallback(object sender, EventArgs e)
        { }
    }
}
