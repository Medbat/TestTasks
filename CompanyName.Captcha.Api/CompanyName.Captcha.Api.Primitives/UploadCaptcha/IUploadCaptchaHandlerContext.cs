namespace CompanyName.Captcha.Api.Primitives.UploadCaptcha;

public interface IUploadCaptchaHandlerContext
{
    string Name { get; set; }

    DateTimeOffset DateCreated { get; set; }

    bool ContainsCyrillicSymbols { get; set; }

    bool ContainsLatinSymbols { get; set; }

    bool ContainsDigits { get; set; }

    bool ContainsSpecials { get; set; }

    bool IsCaseSensitive { get; set; }

    FileAnswers FileAnswers { get; set; }

    Stream FileStream { get; set; }
}