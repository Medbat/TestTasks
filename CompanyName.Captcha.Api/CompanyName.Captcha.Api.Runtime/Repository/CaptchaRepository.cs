using CompanyName.Captcha.Api.EntityFramework;
using CompanyName.Captcha.Api.Runtime.Abstractions.Repository;

namespace CompanyName.Captcha.Api.Runtime.Repository;

public class CaptchaRepository : ICaptchaRepository
{
    private readonly CaptchaDbContext _dbContext;

    public CaptchaRepository(CaptchaDbContext dbContext)
    {
        _dbContext = dbContext;
    }
}