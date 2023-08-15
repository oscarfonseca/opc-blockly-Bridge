using Microsoft.AspNetCore.Mvc;
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
    public async Task<IActionResult> Read()
    {
        var valueRead = await _opcClient.Read();
        return Ok($"Read request handled: {valueRead}");
    }
    
    [HttpPost("write")]
    public async Task<IActionResult> Write()
    {
        var success = await _opcClient.Write(true);
        return Ok($"Write request handled: {success}");
    }
}