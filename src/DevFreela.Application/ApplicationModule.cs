using DevFreela.Application.Common;
using DevFreela.Application.Common.Behaviors;
using DevFreela.Application.Common.Configs;
using DevFreela.Application.Features.Projects.CreateProject;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DevFreela.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            AddServices(services, configuration);
            AddHandlers(services);
            AddValidators(services);
            AddBehaviors(services);

            return services;
        }

        private static void AddServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<FreelanceTotalCostConfig>(configuration.GetSection("FreelanceTotalCostConfig"));
        }

        private static void AddValidators(IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private static void AddHandlers(IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(CreateProjectCommand).Assembly);

                // cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });
        }

        private static void AddBehaviors(IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddTransient<IPipelineBehavior<CreateProjectCommand, Result<Guid>>, ValidateCreateProjectCommandBehavior>();
        }
    }
}
