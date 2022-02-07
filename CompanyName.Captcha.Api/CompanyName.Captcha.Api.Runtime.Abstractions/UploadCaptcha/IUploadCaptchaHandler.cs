using CompanyName.Captcha.Api.Primitives.UploadCaptcha;

namespace CompanyName.Captcha.Api.Runtime.Abstractions.UploadCaptcha;

public interface IUploadCaptchaHandler
{
    Task<UploadCaptchaHandlerResult> Handle(IUploadCaptchaHandlerContext context);
}