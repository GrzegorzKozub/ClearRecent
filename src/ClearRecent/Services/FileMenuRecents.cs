using System;
using System.Collections;
using Microsoft.VisualStudio.Shell.Interop;

namespace ClearRecent.Services
{
    internal class FileMenuRecents
    {
        private readonly IServiceProvider serviceProvider;
        private readonly TypeFactory typeFactory;

        internal FileMenuRecents(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            typeFactory = new TypeFactory();
        }

        internal void ClearAllFiles() => Clear(RecentKind.File);
        internal void ClearAllProjects() => Clear(RecentKind.Project);

        private void Clear(RecentKind kind)
        {
            var dataSource = GetDataSource(kind);
            var recents = GetRecents(dataSource, kind);

            if (recents.Count == 0) { return; }

            var remove = typeFactory.GetRemoveItemAtMethod(kind);

            for (var i = recents.Count - 1; i > -1; i--)
            {
                remove.Invoke(dataSource, new object[] { i });
            }
        }

        private IVsUIDataSource GetDataSource(RecentKind kind)
        {
            var factory = serviceProvider.GetService(typeof(SVsDataSourceFactory)) as IVsDataSourceFactory;

            factory.GetDataSource(
                new Guid("9099ad98-3136-4aca-a9ac-7eeeaee51dca"),
                (uint)kind,
                out IVsUIDataSource dataSource);

            return dataSource;
        }

        private ICollection GetRecents(IVsUIDataSource dataSource, RecentKind kind) =>
            typeFactory
                .GetItemsProp(kind)
                .GetValue(dataSource, null) as ICollection;
    }
}
