using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace tomware.Microwf.Engine
{
  [Authorize]
  [Route("api/workflow")]
  public class WorkflowController : Controller
  {
    private readonly IWorkflowService _service;

    public WorkflowController(IWorkflowService service)
    {
      _service = service;
    }

    [HttpGet("definitions")]
    [ProducesResponseType(typeof(IEnumerable<WorkflowDefinitionViewModel>), 200)]
    public IActionResult Get()
    {
      var result = _service.GetWorkflowDefinitions();

      return Ok(result);
    }

    [HttpGet("dot/{type}")]
    [ProducesResponseType(typeof(string), 200)]
    public IActionResult Dot(string type)
    {
      var result = _service.Dot(type);

      return Ok(result);
    }
  }
}
