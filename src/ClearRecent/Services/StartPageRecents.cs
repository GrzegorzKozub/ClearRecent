using Microsoft.VisualStudio.Settings;
using Microsoft.VisualStudio.Shell.CodeContainerManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace ClearRecent.Services
{
    internal class StartPageRecents
    {
        private readonly IServiceProvider serviceProvider;
        private readonly Files files;

        internal StartPageRecents(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            files = new Files();
        }

        internal void ClearAllProjects() => Clear();
        internal void ClearMissingProjects() => Clear(onlyMissing: true);

        private void Clear(bool onlyMissing = false)
        {
            var manager = GetManager();
            var recents = GetRecents(manager);

            if (recents.Count == 0) { return; }

            var registry = GetRegistry(manager);

            foreach (var path in recents)
            {
                if (!onlyMissing || files.Missing(path))
                {
                    registry.RemoveAsync(path);
                }
            }
        }

        private ISettingsManager GetManager() =>
            serviceProvider.GetService(typeof(SVsSettingsPersistenceManager)) as ISettingsManager;

        private IList<string> GetRecents(ISettingsManager manager) =>
            manager
                .GetOrCreateList(
                    "CodeContainers.Offline",
                    isMachineLocal: true)
                .Keys
                .ToList();

        private static CodeContainerRegistry GetRegistry(ISettingsManager manager) =>
            new CodeContainerRegistry(manager);

        [Guid("9b164e40-c3a2-4363-9bc5-eb4039def653")]
        private class SVsSettingsPersistenceManager
        {
        }
    }
}
