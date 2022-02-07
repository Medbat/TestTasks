namespace CompanyName.Captcha.Api.WebApi.Models.v1.Captcha.UploadCaptcha;

public class UploadCaptchaRequest
{
    public string Name { get; set; }

    public DateTimeOffset DateCreated { get; set; }

    public bool ContainsCyrillicSymbols { get; set; }

    public bool ContainsLatinSymbols { get; set; }

    public bool ContainsDigits { get; set; }

    public bool ContainsSpecials { get; set; }

    public bool IsCaseSensitive { get; set; }

    public FileAnswers FileAnswers { get; set; }

    public IFormFile File { get; set; }
}