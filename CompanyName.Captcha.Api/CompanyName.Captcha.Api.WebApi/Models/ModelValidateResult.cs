namespace CompanyName.Captcha.Api.WebApi.Models;

public class ModelValidateResult
{
    public ICollection<ModelError> Errors { get; set; }
}