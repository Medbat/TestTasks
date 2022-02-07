using System.IO.Compression;
using CompanyName.Captcha.Api.Primitives;
using CompanyName.Captcha.Api.Runtime.Abstractions;

namespace CompanyName.Captcha.Api.Runtime;

public class ZipArchiveExtractor : IArchiveExtractor
{
    public ExtractArchiveResult Extract(Stream fileStream, string extractPath)
    {
        var filesExtracted = new List<string>();

        fileStream.Position = 0;
        using var archive = new ZipArchive(fileStream, ZipArchiveMode.Read, false);
        foreach (var entry in archive.Entries)
        {
            // защита от path traversal attack
            var destinationPath = Path.GetFullPath(Path.Combine(extractPath, entry.FullName));
            if (destinationPath.StartsWith(extractPath, StringComparison.Ordinal))
            {
                entry.ExtractToFile(destinationPath);
                filesExtracted.Add(destinationPath);
            }
        }

        return new ExtractArchiveResult
        {
            FilePaths = filesExtracted
        };
    }
}