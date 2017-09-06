using System;
using System.ComponentModel.Design;
using Microsoft.VisualStudio.Shell;

namespace ClearRecent.Commands
{
    internal abstract class Command
    {
        private readonly IServiceProvider serviceProvider;

        protected Command(Package package, int commandId)
        {
            serviceProvider = package ?? throw new ArgumentNullException(nameof(package));
            if (serviceProvider.GetService(typeof(IMenuCommandService)) is OleMenuCommandService commandService)
            {
                commandService.AddCommand(
                    new MenuCommand(
                        MenuItemCallback,
                        new CommandID(Guids.MenuGroup, commandId)));
            }
        }

        protected abstract void MenuItemCallback(object sender, EventArgs e);
    }
}
