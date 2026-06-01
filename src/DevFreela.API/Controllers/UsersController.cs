using DevFreela.Application.Features.Users.AddSkills;
using DevFreela.Application.Features.Users.CreateUser;
using DevFreela.Application.Features.Users.GetUser;
using DevFreela.Application.Features.Users.Login;
using DevFreela.Application.Features.Users.RequestPasswordReset;
using DevFreela.Application.Features.Users.ResetPassword;
using DevFreela.Application.Features.Users.ValidatePasswordResetCode;
using DevFreela.Core.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController(IMediator mediator) : ControllerBase
    {
        // POST api/users
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(new { result.Error });
            }

            return CreatedAtAction(nameof(GetById), new { id = result.Value?.Id }, result.Value);
        }

        // GET api/users/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetUserQuery(id), cancellationToken);

            if (result.IsFailure)
            {
                return NotFound(new { result.Error });
            }

            return Ok(result.Value);
        }
        
        // PUT api/users/{id}/profile-picture
        [HttpPut("{id}")]
        public IActionResult PutProfilePicture(Guid id, IFormFile file)
        {
            var description = $"File: {file.FileName}, Size: {file.Length} bytes";

            // Processar a imagem

            return Ok(description);
        }

        // PUT api/users/{id}/skills
        [HttpPut("{id}/skills")]
        public async Task<IActionResult> AddSkills(Guid id, AddSkillsCommand command, CancellationToken cancellationToken)
        {
            command.UserId = id;
            var result = await mediator.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(new { result.Error });
            }

            return NoContent();
        }

        // POST api/users/login
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginCommand command, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(new { result.Error });
            }

            return Ok(result.Value);
        }

        // POST api/users/password-reset/request
        [HttpPost("password-reset/request")]
        [AllowAnonymous]
        public async Task<IActionResult> RequestPasswordReset(RequestPasswordResetCommand command, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(new { result.Error });
            }

            return NoContent();
        }

        // POST api/users/password-reset/validate
        [HttpPost("password-reset/validate")]
        [AllowAnonymous]
        public async Task<IActionResult> ValidatePasswordResetCode(ValidatePasswordResetCodeCommand command, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(new { result.Error });
            }

            return NoContent();
        }

        // POST api/users/password-reset/reset
        [HttpPost("password-reset/reset")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordCommand command, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(new { result.Error });
            }

            return Ok();
        }
    }
}
