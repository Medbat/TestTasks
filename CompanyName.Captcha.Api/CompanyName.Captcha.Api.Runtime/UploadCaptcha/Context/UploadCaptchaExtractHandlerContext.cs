using CompanyName.Captcha.Api.Primitives.UploadCaptcha;

namespace CompanyName.Captcha.Api.Runtime.UploadCaptcha.Context;

public class UploadCaptchaExtractHandlerContext : IUploadCaptchaHandlerContext
{
    public string Name { get; set; }
    public DateTimeOffset DateCreated { get; set; }
    public bool ContainsCyrillicSymbols { get; set; }
    public bool ContainsLatinSymbols { get; set; }
    public bool ContainsDigits { get; set; }
    public bool ContainsSpecials { get; set; }
    public bool IsCaseSensitive { get; set; }
    public FileAnswers FileAnswers { get; set; }
    public Stream FileStream { get; set; }

    public List<(string file, string? answer)> Answers { get; set; }
}