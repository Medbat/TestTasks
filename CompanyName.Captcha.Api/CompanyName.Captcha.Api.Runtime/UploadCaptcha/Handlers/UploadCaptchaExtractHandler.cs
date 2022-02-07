using CompanyName.Captcha.Api.Primitives.UploadCaptcha;
using CompanyName.Captcha.Api.Primitives.UploadCaptcha.Options;
using CompanyName.Captcha.Api.Runtime.Abstractions;
using CompanyName.Captcha.Api.Runtime.UploadCaptcha.Context;
using Microsoft.Extensions.Options;

namespace CompanyName.Captcha.Api.Runtime.UploadCaptcha.Handlers;

public class UploadCaptchaExtractHandler : UploadCaptchaHandlerBase
{
    // вынести в конфиг
    private const int MinPictureCountBase = 2000;
    private const int MaxPictureCountBase = 3000;
    private const int PictureCountChange = 3000;

    private readonly IArchiveExtractor _archiveExtractor;
    private readonly UploadCaptchaExtractHandlerOptions _options;

    public UploadCaptchaExtractHandler(
        IArchiveExtractor archiveExtractor,
        IOptions<UploadCaptchaExtractHandlerOptions> options)
    {
        _archiveExtractor = archiveExtractor;
        _options = options.Value;
    }

    public override Task<UploadCaptchaHandlerResult> Handle(IUploadCaptchaHandlerContext context)
    {
        var extractArchiveResult = _archiveExtractor.Extract(
            context.FileStream, 
            Path.Combine(_options.ExtractionPath, context.Name));

        string? specialFile = null;
        if (context.FileAnswers == FileAnswers.AtSpecialFile)
        {
            specialFile = FindSpecialFile(extractArchiveResult.FilePaths);
            if (string.IsNullOrEmpty(specialFile))
            {
                return Task.FromResult(new UploadCaptchaHandlerResult
                {
                    Error = "Отсутствует файл с ответами",
                    IsSuccessful = false
                });
            }
        }

        var minPictureCount = MinPictureCountBase + CalculateMultiply(context);
        var maxPictureCount = MaxPictureCountBase + CalculateMultiply(context);
        var pictureCount = extractArchiveResult.FilePaths.Count
                           - (context.FileAnswers == FileAnswers.AtSpecialFile ? 1 : 0);

        if (pictureCount < minPictureCount || pictureCount > maxPictureCount)
        {
            return Task.FromResult(new UploadCaptchaHandlerResult
            {
                Error = "Передано некорректное количество изображений",
                IsSuccessful = false
            });
        }

        var fileAnswers = new List<(string file, string? answer)>(pictureCount);
            
        // можно применить стратегию
        if (!string.IsNullOrEmpty(specialFile))
        {
            using var specialFileStream = File.Open(specialFile, FileMode.Open);
            using var specialFileReader = new StreamReader(specialFileStream);
            while (!specialFileReader.EndOfStream)
            {
                var values = specialFileReader.ReadLine()?.Split(":");
                if (values is { Length: 2 })
                {
                    fileAnswers.Add((values[0], values[1]));
                }
            }

            if (fileAnswers.Count != pictureCount)
            {
                return Task.FromResult(new UploadCaptchaHandlerResult
                {
                    Error = "Количество строк в файле с ответами не совпадает с количеством изображений"
                });
            }
        }
        else
        {
            if (context.FileAnswers == FileAnswers.AtFileName)
            {
                foreach (var filePath in extractArchiveResult.FilePaths)
                {
                    fileAnswers.Add((Path.GetFileName(filePath), 
                        Path.GetFileNameWithoutExtension(filePath)));
                }
            }
        }

        return _successor?.Handle(new UploadCaptchaExtractHandlerContext
        {
            Answers = fileAnswers,
            ContainsCyrillicSymbols = context.ContainsCyrillicSymbols,
            ContainsDigits = context.ContainsDigits,
            // ...
        });
    }

    private string? FindSpecialFile(ICollection<string> filePaths)
    {
        return filePaths.FirstOrDefault(filePath => 
            filePath.EndsWith(".txt", StringComparison.OrdinalIgnoreCase));
    }

    private int CalculateMultiply(IUploadCaptchaHandlerContext context)
    {
        return (context.ContainsCyrillicSymbols ? PictureCountChange : 0)
               + (context.ContainsLatinSymbols ? PictureCountChange : 0)
               + (context.ContainsDigits ? PictureCountChange : 0)
               + (context.ContainsSpecials ? PictureCountChange : 0)
               + (context.IsCaseSensitive ? PictureCountChange : 0);
    }
}