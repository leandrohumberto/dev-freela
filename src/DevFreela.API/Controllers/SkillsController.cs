using DevFreela.Application.Features.Skills.CreateSkill;
using DevFreela.Application.Features.Skills.GetAllSkills;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController(IMediator mediator) : ControllerBase
    {
        // GET api/skills
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetAllSkillsQuery(), cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest();
            }

            return Ok(result.Value);
        }

        // POST api/skills
        [HttpPost]
        public async Task<IActionResult> Post(CreateSkillCommand command, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(new { result.Error });
            }

            return NoContent();
        }
    }
}
