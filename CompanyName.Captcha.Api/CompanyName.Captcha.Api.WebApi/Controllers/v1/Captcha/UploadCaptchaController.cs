using CompanyName.Captcha.Api.Primitives.UploadCaptcha;
using CompanyName.Captcha.Api.Runtime.Abstractions.UploadCaptcha;
using CompanyName.Captcha.Api.WebApi.Models.v1;
using CompanyName.Captcha.Api.WebApi.Models.v1.Captcha.UploadCaptcha;
using Microsoft.AspNetCore.Mvc;

namespace CompanyName.Captcha.Api.WebApi.Controllers.v1.Captcha;

[Route("api/captcha/v1/upload")]
public class UploadCaptchaController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly ModelValidator _modelValidator;
    private readonly IUploadCaptchaHandler _handler;

    public UploadCaptchaController(
        ILogger<UploadCaptchaController> logger, 
        ModelValidator modelValidator, 
        IUploadCaptchaHandler handler)
    {
        _logger = logger;
        _modelValidator = modelValidator;
        _handler = handler;
    }

    [HttpPost]
    public async Task<IActionResult> Upload(UploadCaptchaRequest? request)
    {
        var modelValidationResult = _modelValidator.ValidateUploadCaptchaRequest(request);
        if (modelValidationResult.Errors.Any())
        {
            return new BadRequestObjectResult(modelValidationResult.Errors);
        }

        var fileMemoryStream = new MemoryStream();
        await request.File.CopyToAsync(fileMemoryStream);

        var result = await _handler.Handle(new UploadCaptchaHandlerContextBase
        {
            ContainsCyrillicSymbols = request.ContainsCyrillicSymbols,
            ContainsDigits = request.ContainsDigits,
            ContainsLatinSymbols = request.ContainsLatinSymbols,
            // ...
        });

        if (result is { IsSuccessful: true })
        {
            return new OkResult();
        }
        // уточнить http код
        return Problem(detail: result.Error);
    }
}