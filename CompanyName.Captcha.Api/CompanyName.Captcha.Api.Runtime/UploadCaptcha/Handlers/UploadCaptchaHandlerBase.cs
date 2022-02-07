using CompanyName.Captcha.Api.Primitives.UploadCaptcha;
using CompanyName.Captcha.Api.Runtime.Abstractions.UploadCaptcha;

namespace CompanyName.Captcha.Api.Runtime.UploadCaptcha.Handlers;

public abstract class UploadCaptchaHandlerBase : IUploadCaptchaContiniousHandler
{
    protected IUploadCaptchaHandler _successor;

    public void SetNext(IUploadCaptchaHandler handler)
    {
        _successor = handler;
    }

    public abstract Task<UploadCaptchaHandlerResult> Handle(IUploadCaptchaHandlerContext context);
}