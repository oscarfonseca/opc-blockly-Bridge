using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OpcServer.Models;

namespace OpcServer.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
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
        return Ok("Read request handled");
    }
    
    [HttpPost("write")]
    public IActionResult Write()
    {
        return Ok("Write request handled");
    }
}