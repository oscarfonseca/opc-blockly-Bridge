using OpcServerApi.OpcClient;

namespace OpcServerApi.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IOpcClient, OpcClient.OpcClient>();
        }
    }
}