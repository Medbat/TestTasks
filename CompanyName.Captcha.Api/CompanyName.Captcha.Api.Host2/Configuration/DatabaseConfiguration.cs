namespace CompanyName.Captcha.Api.Host.Configuration;

public class DatabaseConfiguration
{
    public string ConnectionString { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public int PoolSize { get; set; }
}