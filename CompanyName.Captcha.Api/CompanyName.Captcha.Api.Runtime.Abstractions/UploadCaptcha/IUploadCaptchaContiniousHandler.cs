namespace CompanyName.Captcha.Api.Runtime.Abstractions.UploadCaptcha;

public interface IUploadCaptchaContiniousHandler : IUploadCaptchaHandler
{
    void SetNext(IUploadCaptchaHandler handler);
}