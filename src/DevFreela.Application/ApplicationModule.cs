using DevFreela.Application.Common;
using DevFreela.Application.Common.Configs;
using DevFreela.Application.Features.Projects.CreateProject;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DevFreela.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            AddServices(services, configuration);
            AddHandlers(services);
            AddBehaviors(services);

            return services;
        }

        private static void AddServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<FreelanceTotalCostConfig>(configuration.GetSection("FreelanceTotalCostConfig"));
        }

        private static void AddHandlers(IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateProjectCommand).Assembly));
        }

        private static void AddBehaviors(IServiceCollection services)
        {
            services.AddTransient<IPipelineBehavior<CreateProjectCommand, Result<Guid>>, ValidateCreateProjectCommandBehavior>();
        }
    }
}
