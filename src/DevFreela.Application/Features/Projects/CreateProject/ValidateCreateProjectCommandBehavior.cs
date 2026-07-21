using DevFreela.Application.Common;
using DevFreela.Core.Common;
using DevFreela.Core.Interfaces;
using MediatR;

namespace DevFreela.Application.Features.Projects.CreateProject
{
    public class ValidateCreateProjectCommandBehavior(
        IUnitOfWork unitOfWork)
        : IPipelineBehavior<CreateProjectCommand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(CreateProjectCommand request, RequestHandlerDelegate<Result<Guid>> next, CancellationToken cancellationToken)
        {
            var clientExists = await unitOfWork.Users.GetByIdAsync(request.ClientId, false, cancellationToken) != null;
            var freelancerExists = await unitOfWork.Users.GetByIdAsync(request.FreelancerId, false, cancellationToken) != null;

            if (!clientExists || !freelancerExists)
            {
                return Result.Failure<Guid>(ValidationRules.ClientOrFreelancerNotFoundValidationMessage);
            }

            return await next(cancellationToken);
        }
    }
}
