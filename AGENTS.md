# DevFreela — AGENTS.md

## Solution structure

`DevFreela/src/DevFreela.sln` — 4 projects:
- **DevFreela.Core** — entities, enums, repository interfaces, domain services
- **DevFreela.Application** — CQRS commands/queries via MediatR, FluentValidation validators, custom pipeline behaviors (logging, validation, domain checks)
- **DevFreela.Infrastructure** — EF Core DbContext, repository implementations, JWT/SendGrid/BCrypt services
- **DevFreela.API** — ASP.NET Core 8 Web API entrypoint, thin controllers dispatching MediatR messages

## Commands

Run from `DevFreela/src/` unless noted:
- `dotnet run --project DevFreela.API` — start API at https://localhost:7214 / http://localhost:5264
- `dotnet test DevFreela.UnitTests` — run all unit tests
- `dotnet build` — build solution
- `dotnet ef database update --project ../DevFreela.Infrastructure/DevFreela.Infrastructure.csproj` — apply EF migrations (run from `DevFreela.API/`)
- `docker build -t devfreelaapi -f DevFreela.API/Dockerfile .` — build Docker image
- `docker run -p 8080:8080 -p 8081:8081 devfreelaapi` — run container

## Architecture & patterns

- **Clean Architecture**: Core → Application → Infrastructure → API (dependencies inward)
- **CQRS via MediatR**: each feature is a folder under `Application/Features/{Entity}/{Action}/` with a command/query record, handler, and optional validator + pipeline behavior
- **Result pattern**: handlers return `Result<T>` or `Result` (in `Application/Common/Result.cs`) with `IsSuccess`/`IsFailure`/`Error`; controllers check `result.IsFailure` and return appropriate status codes
- **Unit of Work + Repository pattern**: `IUnitOfWork` in `Core.Interfaces` exposes `Projects`, `Skills`, `Users` repository properties and `CompleteAsync()`; handlers inject `IUnitOfWork` instead of individual repositories. Repository interfaces (`I{Entity}Repository` in Core) no longer expose `SaveChangesAsync`. `UnitOfWork` implementation in Infrastructure composes `DbContext` and registered repositories.
- **Controllers**: primary constructor with `IMediator`, thin methods that `mediator.Send(command)`
- **Validators**: FluentValidation `AbstractValidator<T>` auto-registered via `AddValidatorsFromAssembly`
- **Pipeline behaviors** (registered in order): `LoggingBehavior` → `ValidationBehavior` → feature-specific behaviors (`ValidateCreateProjectCommandBehavior`, `ValidateAddSkillsCommandBehavior`)
- **Auth**: JWT Bearer, role-based with `[Authorize(Roles = "Client")]` / `"Freelancer"`; `[AllowAnonymous]` on register/login/password-reset endpoints

## Testing

- **xUnit** + **FluentAssertions** + **Moq**/**NSubstitute** (both used, sometimes in same file) + **Bogus** for fake data (`Common/Helpers/FakeDataHelper.cs`, locale `pt_BR`)
- Test structure mirrors `Application/Features/` one-to-one
- **Naming**: `{Scenario}_ShouldExpectedBehavior_{Outcome}` (e.g. `InvalidTitle_NewObject_ThrowsArgumentException`)
- All external deps mocked; no integration tests

## Database

- **SQL Server** via `ConnectionStrings:DevFreelaCs`; falls back to **InMemory** if missing
- 5 DbSets: `Projects`, `Users`, `Skills`, `UserSkills`, `ProjectComments`
- EF Core entity configurations auto-discovered via `ApplyConfigurationsFromAssembly`

## Key conventions

- File-scoped namespaces, primary constructors, `record` types for DTOs
- `Async` suffix on async methods
- `var` when type is obvious
- Secrets via `IConfiguration`/User Secrets; never hardcoded
- Password hashing via `IPasswordHasher` (BCrypt.Net-Next)
