﻿using Octopus.Core.Common.Enums;
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
    }
}
