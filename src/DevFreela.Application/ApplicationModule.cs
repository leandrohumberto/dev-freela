using DevFreela.Application.Common.Configs;
using DevFreela.Application.Features.Projects.CreateProject;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DevFreela.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<FreelanceTotalCostConfig>(configuration.GetSection("FreelanceTotalCostConfig"));

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateProjectCommand).Assembly));

            return services;
        }
    }
}
