using Bogus;
using Bogus.DataSets;
using DevFreela.Application.Features.Projects.CommentProject;
using DevFreela.Application.Features.Projects.CompleteProject;
using DevFreela.Application.Features.Projects.CreateProject;
using DevFreela.Application.Features.Projects.DeleteProject;
using DevFreela.Application.Features.Projects.GetProject;
using DevFreela.Application.Features.Projects.SearchProjects;
using DevFreela.Application.Features.Projects.StartProject;
using DevFreela.Application.Features.Projects.UpdateProject;
using DevFreela.Application.Features.Skills.CreateSkill;
using DevFreela.Application.Features.Skills.GetAllSkills;
using DevFreela.Application.Features.Users.AddSkills;
using DevFreela.Application.Features.Users.CreateUser;
using DevFreela.Application.Features.Users.GetUser;
using DevFreela.Application.Features.Users.Login;
using DevFreela.Application.Features.Users.PutProfilePicture;
using DevFreela.Application.Features.Users.RequestPasswordReset;
using DevFreela.Application.Features.Users.ResetPassword;
using DevFreela.Application.Features.Users.ValidatePasswordResetCode;
using DevFreela.Core.Common;
using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using System;

namespace DevFreela.UnitTests.Common.Helpers
{
    public static class FakeDataHelper
    {
        private static readonly Faker<Project> _projectFaker = new Faker<Project>("pt_BR")
            .CustomInstantiator(f => new Project(
                f.Commerce.ProductName(),
                f.Lorem.Sentence(),
                f.Random.Guid(),
                f.Random.Guid(),
                f.Random.Decimal(
                    min: ValidationRules.ProjectTotalCostMinimumValue,
                    max: ValidationRules.ProjectTotalCostMinimumValue * 100)));

        private static readonly Faker<User> _userFaker = new Faker<User>("pt_BR")
            .CustomInstantiator(f => new User(
                f.Person.FullName,
                f.Person.Email,
                f.Date.BetweenDateOnly(
                    ValidationRules.UserBirthDateMinimumValue,
                    ValidationRules.UserBirthDateMaximumValue),
                f.Internet.PasswordCustom(
                    minLength: ValidationRules.UserPasswordMinimumLength,
                    maxLength: ValidationRules.UserPasswordMaximumLength),
                f.PickRandom<UserRole>()));

        private static readonly Faker<ProjectComment> _projectCommentFaker = new Faker<ProjectComment>("pt_BR")
            .RuleFor(c => c.Content, f => f.Lorem.Paragraph())
            .RuleFor(c => c.ProjectId, f => f.Random.Guid())
            .RuleFor(c => c.UserId, f => f.Random.Guid());

        private static readonly Faker<CreateProjectCommand> _createProjectCommandFaker = new Faker<CreateProjectCommand>("pt_BR")
            .CustomInstantiator(f => new CreateProjectCommand(
                f.Commerce.ProductName(),
                f.Lorem.Sentence(),
                f.Random.Guid(),
                f.Random.Guid(),
                f.Random.Decimal(
                    min: ValidationRules.ProjectTotalCostMinimumValue,
                    max: ValidationRules.ProjectTotalCostMinimumValue * 100)));

        private static readonly Faker<CommentProjectCommand> _commentProjectCommandFaker = new Faker<CommentProjectCommand>("pt_BR")
            .CustomInstantiator(f => new CommentProjectCommand(
                f.Random.Guid(),
                f.Lorem.Sentence()));

        private static readonly Faker<CompleteProjectCommand> _completeProjectCommandFaker = new Faker<CompleteProjectCommand>("pt_BR")
            .CustomInstantiator(f => new CompleteProjectCommand(f.Random.Guid()));

        private static readonly Faker<DeleteProjectCommand> _deleteProjectCommandFaker = new Faker<DeleteProjectCommand>("pt_BR")
            .CustomInstantiator(f => new DeleteProjectCommand(f.Random.Guid()));

        private static readonly Faker<GetProjectQuery> _getProjectQueryFaker = new Faker<GetProjectQuery>("pt_BR")
            .CustomInstantiator(f => new GetProjectQuery(f.Random.Guid()));

        private static readonly Faker<SearchProjectsQuery> _searchProjectQueryFaker = new Faker<SearchProjectsQuery>("pt_BR")
            .CustomInstantiator(f => new SearchProjectsQuery(
                f.Commerce.ProductName(),
                f.Lorem.Sentence(),
                f.Random.Int(min: 0),
                f.Random.Int(min: 0)));

        private static readonly Faker<StartProjectCommand> _startProjectCommandFaker = new Faker<StartProjectCommand>("pt_BR")
            .CustomInstantiator(f => new StartProjectCommand(f.Random.Guid()));

        private static readonly Faker<UpdateProjectCommand> _updateProjectCommandFaker = new Faker<UpdateProjectCommand>("pt_BR")
            .CustomInstantiator(f => new UpdateProjectCommand(
                f.Commerce.ProductName(),
                f.Lorem.Sentence(),
                f.Random.Decimal(min: ValidationRules.ProjectTotalCostMinimumValue)));

        private static readonly Faker<CreateSkillCommand> _createSkillCommandFaker = new Faker<CreateSkillCommand>("pt_BR")
            .CustomInstantiator(f => new CreateSkillCommand(f.Name.JobTitle()));

        private static readonly Faker<GetAllSkillsQuery> _getAllSkillsQueryFaker = new("pt_BR");

        private static readonly Faker<AddSkillsCommand> _addSkillsCommandFaker = new Faker<AddSkillsCommand>("pt_BR")
            .CustomInstantiator(f =>
            {
                var guids = new List<Guid>();
                var numberofSkills = f.Random.Int(min: 0, max: 20);

                for (var i = 0; i < numberofSkills; i++)
                {
                    guids.Add(f.Random.Guid());
                }

                return new AddSkillsCommand(guids);
            });

        private static readonly Faker<CreateUserCommand> _createUserCommandFaker = new Faker<CreateUserCommand>("pt_BR")
            .CustomInstantiator(f => new CreateUserCommand(
                f.Person.FullName,
                f.Person.Email,
                f.Date.BetweenDateOnly(
                    ValidationRules.UserBirthDateMinimumValue,
                    ValidationRules.UserBirthDateMaximumValue),
                f.Internet.PasswordCustom(
                    minLength: ValidationRules.UserPasswordMinimumLength,
                    maxLength: ValidationRules.UserPasswordMaximumLength),
                f.PickRandom<UserRole>()));

        private static readonly Faker<LoginCommand> _loginCommandFaker = new Faker<LoginCommand>("pt_BR")
            .CustomInstantiator(f => new LoginCommand(
                f.Internet.Email(),
                f.Internet.PasswordCustom(
                    minLength: ValidationRules.UserPasswordMinimumLength,
                    maxLength: ValidationRules.UserPasswordMaximumLength)));

        private static readonly Faker<GetUserQuery> _getUserQueryFaker = new Faker<GetUserQuery>("pt_BR")
            .CustomInstantiator(f => new GetUserQuery(f.Random.Guid()));

        private static readonly Faker<PutProfilePictureCommand> _putProfilePictureCommandFaker = new("pt_BR");

        private static readonly Faker<RequestPasswordResetCommand> _requestPasswordResetCommandFaker = new Faker<RequestPasswordResetCommand>("pt_BR")
            .CustomInstantiator(f => new RequestPasswordResetCommand(f.Person.Email));

        private static readonly Faker<ResetPasswordCommand> _resetPasswordCommandFaker = new Faker<ResetPasswordCommand>("pt_BR")
            .CustomInstantiator(f => new ResetPasswordCommand(
                f.Person.Email,
                f.Random.Int(100000, 999999).ToString(),
                f.Internet.PasswordCustom(ValidationRules.UserPasswordMinimumLength, ValidationRules.UserPasswordMaximumLength)));

        private static readonly Faker<ValidatePasswordResetCodeCommand> _validatePasswordResetCodeCommandFaker = new Faker<ValidatePasswordResetCodeCommand>("pt_BR")
            .CustomInstantiator(f => new ValidatePasswordResetCodeCommand(
                f.Person.Email,
                f.Random.Int(100000, 999999).ToString()));

        public static Faker Faker { get; } = new("pt_BR");

        public static Project CreateFakeProject() => _projectFaker.Generate();

        public static User CreateFakeUser() => _userFaker.Generate();

        public static ProjectComment CreateFakeProjectComment() => _projectCommentFaker.Generate();

        public static CreateProjectCommand CreateFakeCreateProjectCommand() => _createProjectCommandFaker.Generate();

        public static CommentProjectCommand CreateFakeCommenteProjectCommand() => _commentProjectCommandFaker.Generate();

        public static CompleteProjectCommand CreateFakeCompleteProjectCommand() => _completeProjectCommandFaker.Generate();

        public static DeleteProjectCommand CreateFakeDeleteProjectCommand() => _deleteProjectCommandFaker.Generate();

        public static GetProjectQuery CreateFakeGetProjectQuery() => _getProjectQueryFaker.Generate();

        public static SearchProjectsQuery CreateFakeSearchProjectsQuery() => _searchProjectQueryFaker.Generate();

        public static StartProjectCommand CreateFakeStartProjectCommand() => _startProjectCommandFaker.Generate();

        public static UpdateProjectCommand CreateFakeUpdateProjectCommand() => _updateProjectCommandFaker.Generate();

        public static CreateSkillCommand CreateFakeCreateSkillCommand() => _createSkillCommandFaker.Generate();

        public static GetAllSkillsQuery CreateFakeGetAllSkillsQuery() => _getAllSkillsQueryFaker.Generate();

        public static AddSkillsCommand CreateFakeAddSkillsCommand(bool invalidData = false)
        {
            if (invalidData) return new AddSkillsCommand(null!);

            return _addSkillsCommandFaker.Generate();
        }

        public static CreateUserCommand CreateFakeCreateUserCommand() => _createUserCommandFaker.Generate();

        public static GetUserQuery CreateFakeGetUserQuery() => _getUserQueryFaker.Generate();

        public static LoginCommand CreateFakeLoginCommand() => _loginCommandFaker.Generate();

        public static PutProfilePictureCommand CreateFakePutProfilePictureCommand() => _putProfilePictureCommandFaker.Generate();

        public static RequestPasswordResetCommand CreateFakeRequestPasswordResetCommand() => _requestPasswordResetCommandFaker.Generate();

        public static ResetPasswordCommand CreateFakeResetPasswordCommand() => _resetPasswordCommandFaker.Generate();

        public static ValidatePasswordResetCodeCommand CreateFakeValidatePasswordResetCodeCommand() => _validatePasswordResetCodeCommandFaker.Generate();

        public static decimal GetProjectTotalCostValue() => Faker.Random.Decimal(
            min: ValidationRules.ProjectTotalCostMinimumValue,
            max: ValidationRules.ProjectTotalCostMinimumValue * 100);

        public static string PasswordCustom(
            this Internet internet,
            int minLength,
            int maxLength,
            bool includeUppercase = true,
            bool includeNumber = true,
            bool includeSymbol = true)
        {

            ArgumentNullException.ThrowIfNull(internet);
            ArgumentOutOfRangeException.ThrowIfLessThan(minLength, 1);
            ArgumentOutOfRangeException.ThrowIfLessThan(maxLength, minLength);

            var r = internet.Random;
            var s = "";

            s += r.Char('a', 'z').ToString();
            if (s.Length < maxLength && includeUppercase) s += r.Char('A', 'Z').ToString();
            if (s.Length < maxLength && includeNumber) s += r.Char('0', '9').ToString();
            if (s.Length < maxLength && includeSymbol) s += r.Char('!', '/').ToString();
            if (s.Length < minLength) s += r.String2(minLength - s.Length);                // pad up to min
            if (s.Length < maxLength) s += r.String2(r.Number(0, maxLength - s.Length));   // random extra padding in range min..max

            var chars = s.ToArray();
            var charsShuffled = r.Shuffle(chars).ToArray();

            return new string(charsShuffled);
        }
    }
}
