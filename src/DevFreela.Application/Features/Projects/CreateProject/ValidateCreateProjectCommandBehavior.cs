using DevFreela.Application.Common;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Features.Projects.CreateProject
{
    public class ValidateCreateProjectCommandBehavior(
        IUserRepository repository)
        : IPipelineBehavior<CreateProjectCommand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(CreateProjectCommand request, RequestHandlerDelegate<Result<Guid>> next, CancellationToken cancellationToken)
        {
            var clientExists = await repository.GetByIdAsync(request.ClientId, false, cancellationToken) != null;
            var freelancerExists = await repository.GetByIdAsync(request.FreelancerId, false, cancellationToken) != null;

            if (!clientExists || !freelancerExists)
            {
                return Result.Failure<Guid>("Client or Freelancer not found.");
            }

            return await next(cancellationToken);
        }
    }
}
