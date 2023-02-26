using DevFreela.API.Models;
using DevFreela.Application.Commands.CreateComment;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.Delete;
using DevFreela.Application.Commands.Finish;
using DevFreela.Application.Commands.Start;
using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Application.InputModels;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using DevFreela.Application.Queries.GetProjectById;
using Microsoft.AspNetCore.Authorization;

namespace DevFreela.API.Controllers
{
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IMediator _mediator;
        public ProjectsController(IProjectService projectService, IMediator mediator)
        {
            _projectService = projectService;   
            _mediator = mediator;
        }
        //api/projects?query=net core
        [HttpGet]
        [Authorize(Roles = "client, freelancer")]
        public async Task<IActionResult> Get(string query)
        {
            var getAllProjectsQuery = new GetAllProjectsQuery(query);

            var projects = await _mediator.Send(getAllProjectsQuery);

            return Ok(projects);
        }

        // api/projects/id
        [HttpGet("{id}")]
        [Authorize(Roles = "client, freelancer")]

        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetProjectByIdQuery(id);
            var project = await _mediator.Send(query);

            if (project == null)
            {
                return NotFound();
            }
            return Ok(project);
        }

        [HttpPost]
        [Authorize(Roles = "client")]

        public async Task<IActionResult> Post([FromBody] CreateProjectCommand command)
        {
            //var id = _projectService.Create(inputModel);
            var id = await _mediator.Send(command);
            // Cadastrar o projeto

            return CreatedAtAction(nameof (GetById), new { id = id }, command);
        }
        // api/project/id
        [HttpPut("{id}")]
        [Authorize(Roles = "client")]

        public async Task<IActionResult> Put(int id, [FromBody] UpdateProjectCommand command)
        {
            //_projectService.Update(inputModel);
            await _mediator.Send(command);

            //Atualizo o objeto

            return NoContent();
        }

        //api/project/id
        [HttpDelete("{id}")]
        [Authorize(Roles = "client")]

        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteCommand(id);
            // _projectService.Delete(id);
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpPost("{id}/comments")]
        [Authorize(Roles = "client, freelancer")]

        public async Task<IActionResult> PostComment(int id, [FromBody] CreateCommentCommand command)
        {
            //_projectService.CreateComment(inputModel);
            await _mediator.Send(command);

            return NoContent();

        }

        [HttpPut("{id}/start")]
        [Authorize(Roles = "client")]

        public async Task<IActionResult> Start(int id)
        {
            var command = new StartCommand(id);
            // _projectService.Start(id);
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpPut("{id}/finish")]
        [Authorize(Roles = "client")]

        public async Task<IActionResult> Finish(int id)
        {
            var command = new FinishCommand(id);
            // _projectService.Finish(id);
            await _mediator.Send(command);

            return NoContent();
        }

    }
}
