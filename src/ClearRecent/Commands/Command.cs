using System;
using System.ComponentModel.Design;
using ClearRecent.Services;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace ClearRecent.Commands
{
    internal abstract class Command
    {
        private readonly IServiceProvider serviceProvider;

        protected readonly FileMenuRecents fileMenuRecents;
        protected readonly StartPageRecents startPageRecents;

        protected readonly string confirmation;

        protected Command(
            Package package,
            int commandId,
            string confirmation)
        {
            serviceProvider = package ?? throw new ArgumentNullException(nameof(package));

            fileMenuRecents = new FileMenuRecents(serviceProvider);
            startPageRecents = new StartPageRecents(serviceProvider);

            this.confirmation = confirmation;

            if (serviceProvider.GetService(typeof(IMenuCommandService)) is OleMenuCommandService commandService)
            {
                commandService.AddCommand(
                    new MenuCommand(
                        Handle,
                        new CommandID(Guids.MenuGroup, commandId)));
            }
        }

        protected abstract void Execute();

        private void Handle(object sender, EventArgs e)
        {
            if (VsShellUtilities.ShowMessageBox(
                    serviceProvider,
                    confirmation,
                    null,
                    OLEMSGICON.OLEMSGICON_QUERY,
                    OLEMSGBUTTON.OLEMSGBUTTON_YESNO,
                    OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST) == 6)
            { Execute(); }
        }
    }
}
