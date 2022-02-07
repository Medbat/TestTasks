namespace CompanyName.Captcha.Api.Primitives.UploadCaptcha;

public class UploadCaptchaHandlerContextBase : IUploadCaptchaHandlerContext
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
}