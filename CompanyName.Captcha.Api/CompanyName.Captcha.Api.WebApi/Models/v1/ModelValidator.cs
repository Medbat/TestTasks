using System.Text.RegularExpressions;
using CompanyName.Captcha.Api.WebApi.Models.v1.Captcha.UploadCaptcha;

namespace CompanyName.Captcha.Api.WebApi.Models.v1;

/// <summary>
/// Класс для проверки соответствия входящим параметрам контроллеров v1 бизнес-требованиям
/// </summary>
public class ModelValidator
{
    private const string NameStopWord = "captcha";

    /// <summary>
    /// Проверяет входные параметры метода captcha/v1/upload на соответствие бизнес-требованиям
    /// </summary>
    /// <param name="request">Входящие параметры метода</param>
    /// <returns>Результат проверки</returns>
    public ModelValidateResult ValidateUploadCaptchaRequest(UploadCaptchaRequest? request)
    {
        // TODO: проверки вынести в отдельные классы, сюда инъектить все проверки и пробегаться по ним
        var errors = new List<ModelError>();
        if (request == null)
        {
            errors.Add(new ModelError
            {
                // TODO: локализация текста ошибки
                ErrorMessage = "Не переданы данные для загрузки",
                // TODO: заполнять коды ошибок
                    
            });
            return new ModelValidateResult
            {
                Errors = errors
            };
        }

        if (string.IsNullOrWhiteSpace(request.Name))
        {
            errors.Add(new ModelError("Не указано имя"));
        }
        else
        {
            if (request.Name.Length < 4 || request.Name.Length > 8)
            {
                errors.Add(new ModelError("Длина имена должна быть от 4 до 8"));
            }
            else
            {
                if (!Regex.IsMatch(request.Name, @"^[a-zA-Z]+$"))
                {
                    errors.Add(new ModelError(
                        "Имя должно содержать только латинские символы",
                        ModelErrorCodes.NameContainsNonLatinSymbols)
                    );
                }
                else
                {
                    if (request.Name.Contains(NameStopWord, StringComparison.OrdinalIgnoreCase))
                    {
                        errors.Add(new ModelError($"Имя не должно содержать слово '{NameStopWord}'"));
                    }
                }
            }
        }

        if (!request.ContainsCyrillicSymbols && !request.ContainsLatinSymbols && !request.ContainsDigits)
        {
            errors.Add(new ModelError("Выбрано как минимум одно из: “Содержит кириллицу”, “Содержит латиницу”, " +
                                      "“Содержит цифры”"));
        }

        return new ModelValidateResult
        {
            Errors = errors
        };
    }
}