using System;
using System.ComponentModel.Design;
using ClearRecent.Services;
using Microsoft.VisualStudio.Shell;

namespace ClearRecent.Commands
{
    internal abstract class Command
    {
        private readonly IServiceProvider serviceProvider;
        protected readonly FileMenuRecents fileMenuRecents;

        protected Command(Package package, int commandId)
        {
            serviceProvider = package ?? throw new ArgumentNullException(nameof(package));
            fileMenuRecents = new FileMenuRecents(serviceProvider);

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
