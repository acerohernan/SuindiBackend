using Api.Shared;

namespace Api.Modules
{
    public static class ServicesInjector
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddTransient<IConfigurationHelper, ConfigurationHelper>();
            return services;
        }
    }
}
