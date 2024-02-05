using System.Diagnostics;
using System.Reflection;
using AspNetworkTestTool.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspNetworkTestTool.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger) => _logger = logger;

    [HttpGet]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }

    [HttpGet]
    public IActionResult Index()
    {
        ViewBag.Version = Assembly.GetExecutingAssembly().GetName().Version?.ToString();

        var rh = "";
        foreach (var key in Request.Headers.Keys)
        {
            Request.Headers.TryGetValue(key, out var values);
            rh += key + " : ";
            rh = values.Aggregate(rh, (current, value) => current + value + " ");
            rh += Environment.NewLine;
        }

        ViewBag.User = rh;
        return View();
    }
}
