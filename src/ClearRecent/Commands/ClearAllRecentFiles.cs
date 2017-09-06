using System;
using Microsoft.VisualStudio.Shell;

namespace ClearRecent.Commands
{
    internal sealed class ClearAllRecentFiles : Command
    {
        internal ClearAllRecentFiles(Package package) :
            base(package, 0x0100)
        { }

        protected override void MenuItemCallback(object sender, EventArgs e)
        { }
    }
}
