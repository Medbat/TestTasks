using CompanyName.Captcha.Api.Primitives.UploadCaptcha;
using CompanyName.Captcha.Api.Runtime.Abstractions.Repository;

namespace CompanyName.Captcha.Api.Runtime.UploadCaptcha.Handlers;

public class UploadCaptchaSaveDataHandler : UploadCaptchaHandlerBase
{
    private readonly ICaptchaRepository _captchaRepository;

    public UploadCaptchaSaveDataHandler(ICaptchaRepository captchaRepository)
    {
        _captchaRepository = captchaRepository;
    }

    public override Task<UploadCaptchaHandlerResult> Handle(IUploadCaptchaHandlerContext context)
    {
        throw new NotImplementedException();
    }
}