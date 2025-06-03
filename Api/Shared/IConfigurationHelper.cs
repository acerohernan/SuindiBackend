namespace Api.Shared
{
    public interface IConfigurationHelper
    {
        public string OriginsCors { get; }

        public string Secret { get; }
    }
}
