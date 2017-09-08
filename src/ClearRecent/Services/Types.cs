using System;
using System.Reflection;

namespace ClearRecent.Services
{
    internal class Types
    {
        internal PropertyInfo GetItemsProp(Kind kind) =>
            CreateMruListType(kind).GetProperty("Items");

        internal MethodInfo GetRemoveItemAtMethod(Kind kind) =>
            CreateMruListType(kind).GetMethod("RemoveItemAt");

        internal PropertyInfo GetPathProp() =>
            CreateType("FileSystemMruItem").GetProperty("Path");

        private static Type CreateMruListType(Kind kind)
        {
            return CreateType($"{kind.ToString()}MruList");
        }

        private static Type CreateType(string name)
        {
            const string Namespace = "Microsoft.VisualStudio.PlatformUI";
            const string Assembly = "Microsoft.VisualStudio.Shell.UI.Internal";

            return Type.GetType($"{Namespace}.{name}, {Assembly}", true);
        }
    }
}
