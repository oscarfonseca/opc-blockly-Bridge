using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OpcServer.Models;
using OpcServer.OpcClient;

namespace OpcServer.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IOpcClient _opcClient;

    public HomeController(ILogger<HomeController> logger, IOpcClient opcClient)
    {
        _logger = logger;
        _opcClient = opcClient;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
    [HttpGet("read")]
    public IActionResult Read()
    {
        var valueRead = _opcClient.Read();
        return Ok($"Read request handled: {valueRead}");
    }
    
    [HttpPost("write")]
    public IActionResult Write()
    {
        var success = _opcClient.Write(true);
        return Ok($"Write request handled: {success}");
    }
}