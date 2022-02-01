using System.IO;
using EnvDTE;
using System.Runtime.CompilerServices;
using System.Linq;

[assembly: InternalsVisibleTo("AutoSaveFile2019Tests")]
namespace AutoSaveFile2019
{
    internal class Helper
    {
        internal string GetFileType(Window window)
        {
            var documentFullName = window.Document?.FullName;

            if (documentFullName == null)
                documentFullName = window.Project?.FullName;

            if (Path.HasExtension(documentFullName))
                return Path.GetExtension(documentFullName).Replace(".", "");

            return "";
        }

        internal bool ShouldSaveDocument(Window window, OptionPageGrid optionsPage)
        {
            var windowType = window.Kind;

            if (windowType == "Document")
            {
                var fileType = GetFileType(window);
                var ignoredFileTypes = optionsPage.IgnoredFileTypes?
                    .ToLowerInvariant()
                    .Split(',')
                    .Select(str => str.Trim());

                if (ignoredFileTypes == null)
                    return true;

                if (ignoredFileTypes != null && !ignoredFileTypes.Contains(fileType))
                {
                    return true;
                }
            }

            return false;
        }


    }
}
