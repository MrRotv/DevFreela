using DevFreela.API.Models;
using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DevFreela.API.Controllers
{
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;
        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;   
        }
        //api/projects?query=net core
        [HttpGet]
        public IActionResult Get(string query)
        {
            var projects = _projectService.GetAll(query);

            return Ok(projects);
        }

        // api/projects/id
        [HttpGet("{id}")]

        public IActionResult GetById(int id)
        {
            var project = _projectService.GetbyId(id);
            if (project == null)
            {
                return NotFound();
            }
            return Ok(project);
        }

        [HttpPost]

        public IActionResult Post([FromBody] NewProjectInputModel inputModel)
        {
            if (inputModel.Title.Length > 50)
            {
                return BadRequest();
            }

            var id = _projectService.Create(inputModel);

            // Cadastrar o projeto

            return CreatedAtAction(nameof (GetById), new { id = id }, inputModel);
        }
        // api/project/id
        [HttpPut("{id}")]

        public IActionResult Put(int id, [FromBody] UpdateProjectInputModel inputModel)
        {
            if (inputModel.Description.Length > 500)
            {
                return BadRequest();
            }

            _projectService.Update(inputModel);

            //Atualizo o objeto

            return NoContent();
        }

        //api/project/id
        [HttpDelete("{id}")]

        public IActionResult Delete(int id)
        {
            _projectService.Delete(id);

            return NoContent();
        }

        [HttpPost("{id}/comments")]

        public IActionResult PostComment(int id, [FromBody] CreateCommentInputModel inputModel)
        {
            _projectService.CreateComment(inputModel);

            return NoContent();

        }

        [HttpPut("{id}/start")]

        public IActionResult Start(int id)
        {

            _projectService.Start(id);

            return NoContent();
        }

        [HttpPut("{id}/finish")]

        public IActionResult Finish(int id)
        {
            _projectService.Finish(id);

            return NoContent();
        }

    }
}
