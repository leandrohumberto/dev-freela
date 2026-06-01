using DevFreela.Application.Features.Projects.CommentProject;
using DevFreela.Application.Features.Projects.CompleteProject;
using DevFreela.Application.Features.Projects.CreateProject;
using DevFreela.Application.Features.Projects.DeleteProject;
using DevFreela.Application.Features.Projects.GetProject;
using DevFreela.Application.Features.Projects.SearchProjects;
using DevFreela.Application.Features.Projects.StartProject;
using DevFreela.Application.Features.Projects.UpdateProject;
using DevFreela.Core.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ProjectsController(IMediator mediator) : ControllerBase
    {
        // GET api/projects?search=crm
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] SearchProjectsQuery query,
        CancellationToken cancellationToken)
        {
            var result = await mediator.Send(query, cancellationToken);

            if (result.IsFailure)
            {
                return NotFound(new { result.Error });
            }

            return Ok(result.Value);
        }

        // GET api/projects/{guid}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id,
        CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetProjectQuery(id), cancellationToken);

            if (result.IsFailure)
            {
                return NotFound(new { result.Error });
            }

            return Ok(result.Value);
        }

        // POST api/projects
        [HttpPost]
        [Authorize(Roles = nameof(UserRole.Client))]
        public async Task<IActionResult> Post([FromBody] CreateProjectCommand command,
        CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(new { result.Error });
            }

            return CreatedAtAction(nameof(GetById), new { id = result.Value }, command);
        }

        // PUT api/projects/{guid}
        [HttpPut("{id}")]
        [Authorize(Roles = nameof(UserRole.Client))]
        public async Task<IActionResult> Put(Guid id, UpdateProjectCommand command,
        CancellationToken cancellationToken)
        {
            command.ProjectId = id;

            var result = await mediator.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return NotFound(new { result.Error });
            }

            return NoContent();
        }

        // DELETE api/projects/{guid}
        [HttpDelete("{id}")]
        [Authorize(Roles = nameof(UserRole.Client))]
        public async Task<IActionResult> Delete(Guid id,
        CancellationToken cancellationToken)
        {
            var command = new DeleteProjectCommand(id);

            var result = await mediator.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return NotFound(new { result.Error });
            }

            return NoContent();
        }

        // PUT api/projects/{guid}/start
        [HttpPut("{id}/start")]
        [Authorize(Roles = nameof(UserRole.Client))]
        public async Task<IActionResult> Start(Guid id, CancellationToken cancellationToken)
        {
            var command = new StartProjectCommand(id);

            var result = await mediator.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return NotFound(new { result.Error });
            }

            return NoContent();
        }

        // PUT api/projects/{guid}/complete
        [HttpPut("{id}/complete")]
        [Authorize(Roles = nameof(UserRole.Client))]
        public async Task<IActionResult> Complete(Guid id, CancellationToken cancellationToken)
        {
            var command = new CompleteProjectCommand(id);

            var result = await mediator.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return NotFound(new { result.Error });
            }

            return NoContent();
        }

        // POST api/projects/{id}/comments
        [HttpPost("{id}/comments")]
        [Authorize(Roles = $"{nameof(UserRole.Client)}, {nameof(UserRole.Freelancer)}")]
        public async Task<IActionResult> Comment(Guid id, CommentProjectCommand command, CancellationToken cancellationToken)
        {
            command.ProjectId = id;

            var result = await mediator.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return NotFound(new { result.Error });
            }

            return Ok();
        }
    }
}
