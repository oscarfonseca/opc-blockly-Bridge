using Microsoft.AspNetCore.Mvc;
using OpcServerApi.DTO;
using OpcServerApi.OpcClient;

namespace OpcServerApi.Controllers;

[ApiController]
[Route("[controller]")]
public class OpcController : ControllerBase
{
    private readonly ILogger<OpcController> _logger;
    private readonly IOpcClient _opcClient;

    public OpcController(ILogger<OpcController> logger, IOpcClient opcClient)
    {
        _logger = logger;
        _opcClient = opcClient;
    }

    [HttpGet("read")]
    public async Task<ActionResult<PostValueDto>> Read(GetValueDto getValue)
    {
        var result = await _opcClient.Read(getValue);
        return new PostValueDto
        {
            Value = result,
            NodeId = getValue.NodeId
        };
    }

    [HttpPost("write")]
    public async Task<ActionResult> Write(PostValueDto postValue)
    {
        var success = await _opcClient.Write(postValue);
        return Ok($"Write request handled: {success}");
    }
}