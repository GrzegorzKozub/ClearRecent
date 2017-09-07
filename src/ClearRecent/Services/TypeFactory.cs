using System;
using System.Reflection;

namespace ClearRecent.Services
{
    internal class TypeFactory
    {
        internal PropertyInfo GetItemsProp(RecentKind kind) =>
            CreateMruListType(kind).GetProperty("Items");

        internal MethodInfo GetRemoveItemAtMethod(RecentKind kind) =>
            CreateMruListType(kind).GetMethod("RemoveItemAt");

        private Type CreateMruListType(RecentKind kind)
        {
            const string Namespace = "Microsoft.VisualStudio.PlatformUI";
            const string Assembly = "Microsoft.VisualStudio.Shell.UI.Internal";

            return Type.GetType(
                $"{Namespace}.{kind.ToString()}MruList, {Assembly}",
                true);
        }
    }
}
