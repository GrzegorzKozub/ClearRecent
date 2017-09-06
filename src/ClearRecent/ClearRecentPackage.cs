using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using ClearRecent.Commands;
using Microsoft.VisualStudio.Shell;

namespace ClearRecent
{
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0.0", IconResourceID = 400)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(Guids.Package)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    public sealed class ClearRecentPackage : Package
    {
        protected override void Initialize()
        {
            base.Initialize();

            new ClearAllRecentFiles(this);
            new ClearMissingRecentFiles(this);
            new ClearAllRecentProjects(this);
            new ClearMissingRecentProjects(this);
        }
    }
}
