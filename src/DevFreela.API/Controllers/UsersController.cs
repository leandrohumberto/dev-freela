using DevFreela.Application.Features.Users.AddSkills;
using DevFreela.Application.Features.Users.CreateUser;
using DevFreela.Application.Features.Users.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IMediator mediator) : ControllerBase
    {
        // POST api/users
        [HttpPost]
        public async Task<IActionResult> Post(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(new { result.Error });
            }

            return CreatedAtAction(nameof(GetById), new { id = result.Value }, command);
        }

        // GET api/users/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetUserCommand(id), cancellationToken);

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
    }
}
