namespace Api.Shared
{
    public class ConfigurationHelper(IConfiguration configuration) : IConfigurationHelper
    {
        public string OriginsCors => configuration?["ORIGINS_CONFIGURATION"] ?? string.Empty;
        public string Secret => configuration?["SECRET"] ?? string.Empty;
    }
}
