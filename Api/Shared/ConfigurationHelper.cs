namespace Api.Shared
{
    public class ConfigurationHelper(IConfiguration configuration) : IConfigurationHelper
    {
        public string OriginsCors => configuration["ORIGINS_CONFIGURATION"] ?? string.Empty;
        public string Secret => configuration["SECRET"] ?? string.Empty;
        public SmtpConfig Smtp => new SmtpConfig(configuration);
        public class SmtpConfig(IConfiguration configuration)
        {
            public string Host { get; set; } = configuration["SMTP_HOST"] ?? string.Empty;
            public int Port { get; set; } = int.Parse(configuration["SMTP_PORT"] ?? string.Empty);
            public string UserName { get; set; } = configuration?["SMTP_USERNAME"] ?? string.Empty;
            public string Password { get; set; } = configuration?["SMTP_PASSWORD"] ?? string.Empty;
            public string FromSubject { get; set; } = configuration?["SMTP_FROM_SUBJECT"] ?? string.Empty;
            public string FromEmail { get; set; } = configuration?["SMTP_FROM_EMAIL"] ?? string.Empty;
        }
    }
}
