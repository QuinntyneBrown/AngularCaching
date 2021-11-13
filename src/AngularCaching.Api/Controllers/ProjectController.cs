using System.Net;
using System.Threading.Tasks;
using AngularCaching.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AngularCaching.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController
    {
        private readonly IMediator _mediator;

        public ProjectController(IMediator mediator)
            => _mediator = mediator;

        [HttpGet("{projectId}", Name = "GetProjectByIdRoute")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetProjectById.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetProjectById.Response>> GetById([FromRoute]GetProjectById.Request request)
        {
            var response = await _mediator.Send(request);
        
            if (response.Project == null)
            {
                return new NotFoundObjectResult(request.ProjectId);
            }
        
            return response;
        }
        
        [HttpGet(Name = "GetProjectsRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetProjects.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetProjects.Response>> Get()
            => await _mediator.Send(new GetProjects.Request());
        
        [HttpPost(Name = "CreateProjectRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateProject.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateProject.Response>> Create([FromBody]CreateProject.Request request)
            => await _mediator.Send(request);
        
        [HttpGet("page/{pageSize}/{index}", Name = "GetProjectsPageRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetProjectsPage.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetProjectsPage.Response>> Page([FromRoute]GetProjectsPage.Request request)
            => await _mediator.Send(request);
        
        [HttpPut(Name = "UpdateProjectRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(UpdateProject.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UpdateProject.Response>> Update([FromBody]UpdateProject.Request request)
            => await _mediator.Send(request);
        
        [HttpDelete("{projectId}", Name = "RemoveProjectRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(RemoveProject.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<RemoveProject.Response>> Remove([FromRoute]RemoveProject.Request request)
            => await _mediator.Send(request);
        
    }
}