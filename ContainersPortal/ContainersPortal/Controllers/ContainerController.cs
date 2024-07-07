using ContainersPortal.Database;
using ContainersPortal.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ContainersPortal.Controllers;

[ApiController]
[Route("[controller]")]
public class ContainerController : Controller
{
    private readonly UserContext _userContext;
    private readonly DockerManagerService _dockerService;

    public ContainerController(UserContext userContext, DockerManagerService dockerService)
    {
        _userContext = userContext;
        _dockerService = dockerService;
    }

    [HttpGet("GetContainerInfo")]
    [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
    public IActionResult GetContainerInfo([FromQuery] string username)
    {
        var user = _userContext.Users.Where(u => u.UserName == username).FirstOrDefault();

        if (user == null)
            return NotFound();


        var model = new
        {
            containerName = "user1-cont", //user.DockerContainerName ,
            isActive = user.IsActive
        };

        return Json(model);
    }
    public class UserRequest
    {
        public string ContainerName { get; set; }
    }

    [HttpPost("StartContainer")]
    [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
    public IActionResult StartContainer([FromBody] UserRequest userRequest)
    {
        try
        {
            _dockerService.StartContainer(userRequest.ContainerName);

            return Ok(new { success = true });
        }
        catch (Exception ex)
        {
            return BadRequest(new { success = false });
        }

    }

    [HttpPost("StopContainer")]
    [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
    public IActionResult StopContainer([FromBody] UserRequest userRequest)
    {
        try
        {
            _dockerService.StopContainer(userRequest.ContainerName);

            return Ok(new { success = true });
        }
        catch (Exception ex)
        {
            return BadRequest(new { success = false });
        }

    }


}
