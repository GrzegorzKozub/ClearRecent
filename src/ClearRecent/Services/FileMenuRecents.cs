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

        internal bool FilesFound() => Found(Kind.File);
        internal bool ProjectsFound() => Found(Kind.Project);

        internal void ClearAllFiles() => Clear(Kind.File, _ => true);
        internal void ClearAllProjects() => Clear(Kind.Project, _ => true);

        internal void ClearMissingFiles() =>
            Clear(Kind.File, files.Missing);

        internal void ClearMissingProjects() =>
            Clear(Kind.Project, files.Missing);

        private bool Found(Kind kind) =>
            GetCount(GetDataSource(kind), kind) > 0;

        private void Clear(Kind kind, Func<string, bool> shouldDelete)
        {
            var dataSource = GetDataSource(kind);
            var recents = GetRecents(dataSource, kind);

            if (recents.Count == 0) { return; }

            var remove = types.GetRemoveItemAtMethod(kind);

            for (var i = recents.Count - 1; i > -1; i--)
            {
                if (shouldDelete(GetPath(recents[i])))
                { remove.Invoke(dataSource, new object[] { i }); }
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

        private int GetCount(IVsUIDataSource dataSource, Kind kind) =>
            (int)types
                .GetCountProp(kind)
                .GetValue(dataSource, index: null);

        private IList GetRecents(IVsUIDataSource dataSource, Kind kind) =>
            types
                .GetItemsProp(kind)
                .GetValue(dataSource, index: null) as IList;

        private string GetPath(object recent) =>
            types
                .GetPathProp()
                .GetValue(recent, index: null) as string;
    }
}
