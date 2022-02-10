using Octopus.Core.Common.Enums;
using System;
using System.IO;

namespace Octopus.Core.Common.Extensions
{
    public static class FileInfoExtensions
    {
        public static FileExtension GetFileExtension(this FileInfo file)
        {
            var fileExtension = Path.GetExtension(file.FullName);

            switch (fileExtension)
            {
                case ".csv":
                    return FileExtension.CSV;

                case ".xml":
                    return FileExtension.XML;

                case ".json":
                    return FileExtension.JSON;

                default:
                    return FileExtension.Unknown;
            }
        }

        public static bool IsAvailable(this FileInfo file)
        {
            try
            {
                using (var fs = file.Open(FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                }
            }
            catch (IOException)
            {
                return false;
            }
            catch (Exception)
            {
                throw;
            }

            return true;
        }
    }
}
