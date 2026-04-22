using FluentValidation;

namespace DevFreela.Application.Features.Users.GetUser
{
    public class GetUserValidator : AbstractValidator<GetUserQuery>
    {
        public GetUserValidator()
        {
            RuleFor(x => x.UserId)
                .NotNull();
        }
    }
}
