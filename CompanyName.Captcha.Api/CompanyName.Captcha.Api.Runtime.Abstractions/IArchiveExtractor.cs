using CompanyName.Captcha.Api.Primitives;

namespace CompanyName.Captcha.Api.Runtime.Abstractions;

public interface IArchiveExtractor
{
    ExtractArchiveResult Extract(Stream fileStream, string extractPath);
}