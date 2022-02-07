namespace CompanyName.Captcha.Api.WebApi.Models;

public class ModelError
{
    public ModelError(string errorMessage, int errorCode)
    {
        ErrorMessage = errorMessage;
        ErrorCode = errorCode;
    }

    public ModelError(string errorMessage)
    {
        ErrorMessage = errorMessage;
    }

    public ModelError()
    {

    }

    public string ErrorMessage { get; set; }

    public int ErrorCode { get; set; }
}