using System;
using System.Collections;
using Microsoft.VisualStudio.Shell.Interop;

namespace ClearRecent.Services
{
    internal class FileMenuRecents
    {
        private readonly IServiceProvider serviceProvider;
        private readonly Types types;
        private readonly Files files;

        internal FileMenuRecents(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            types = new Types();
            files = new Files();
        }

        internal void ClearAllFiles() => Clear(Kind.File);
        internal void ClearAllProjects() => Clear(Kind.Project);

        internal void ClearMissingFiles() =>
            Clear(Kind.File, onlyMissing: true);

        internal void ClearMissingProjects() =>
            Clear(Kind.Project, onlyMissing: true);

        private void Clear(Kind kind, bool onlyMissing = false)
        {
            var dataSource = GetDataSource(kind);
            var recents = GetRecents(dataSource, kind);

            if (recents.Count == 0) { return; }

            var remove = types.GetRemoveItemAtMethod(kind);

            for (var i = recents.Count - 1; i > -1; i--)
            {
                if (!onlyMissing
                    || files.Missing(GetPath(recents[i])))
                {
                    remove.Invoke(dataSource, new object[] { i });
                }
            }
        }

        private IVsUIDataSource GetDataSource(Kind kind)
        {
            var factory = serviceProvider.GetService(typeof(SVsDataSourceFactory)) as IVsDataSourceFactory;

            factory.GetDataSource(
                new Guid("9099ad98-3136-4aca-a9ac-7eeeaee51dca"),
                (uint)kind,
                out IVsUIDataSource dataSource);

            return dataSource;
        }

        private IList GetRecents(IVsUIDataSource dataSource, Kind kind) =>
            types
                .GetItemsProp(kind)
                .GetValue(dataSource, null) as IList;

        private string GetPath(object recent) =>
            types
                .GetPathProp()
                .GetValue(recent, null) as string;
    }
}
