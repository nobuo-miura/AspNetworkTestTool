using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace AspNetworkTestTool.Controllers;

public class RedisController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Index(string config)
    {
        if (!string.IsNullOrEmpty(config))
        {
            ViewBag.Config = config;

            try
            {
                var conn = ConnectionMultiplexer.Connect(config);
                conn.GetDatabase();
                conn.Close();

                ViewBag.RedisResult = "Success.";
                ViewBag.RedisLog = "Success.";
            }
            catch (Exception err)
            {
                ViewBag.RedisResult = "Error";
                ViewBag.RedisLog = err;
            }
        }

        return View();
    }
}
