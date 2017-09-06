using System;
using Microsoft.VisualStudio.Shell;

namespace ClearRecent.Commands
{
    internal sealed class ClearMissingRecentFiles : Command
    {
        internal ClearMissingRecentFiles(Package package) :
            base(package, 0x0101)
        { }

        protected override void MenuItemCallback(object sender, EventArgs e)
        { }
    }
}
