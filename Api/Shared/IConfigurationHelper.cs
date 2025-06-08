using static Api.Shared.ConfigurationHelper;

namespace Api.Shared
{
    public interface IConfigurationHelper
    {
        public string OriginsCors { get; }

        public string Secret { get; }
        public SmtpConfig Smtp { get; }
    }
}
