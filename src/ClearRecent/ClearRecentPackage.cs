using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using ClearRecent.Commands;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;

namespace ClearRecent
{
    [Guid(Guids.Package)]
    [InstalledProductRegistration("#110", "#112", "1.0.0", IconResourceID = 400)]
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [ProvideAutoLoad(VSConstants.UICONTEXT.ShellInitialized_string)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
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
