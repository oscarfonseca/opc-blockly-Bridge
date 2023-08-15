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
    public async Task<ActionResult> Read()
    {
        var valueRead = await _opcClient.Read();
        return Ok($"Read request handled: {valueRead}");
    }
    
    [HttpPost("write")]
    public async Task<ActionResult> Write(WriteValueDto newValue)
    {
        var success = await _opcClient.Write(newValue.Value);
        return Ok($"Write request handled: {success}");
    }
}