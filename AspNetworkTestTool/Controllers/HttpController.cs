using Microsoft.AspNetCore.Mvc;

namespace AspNetworkTestTool.Controllers;

public class HttpController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Index(string url, string certificate)
    {
        if (!string.IsNullOrEmpty(url))
        {
            var checkResult = Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
                              && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (!checkResult)
            {
                ViewBag.HttpLog = "Invalid URL.";
                return View();
            }

            ViewBag.Url = url;

            var httpClientHandler = new HttpClientHandler();

            if (certificate == "on")
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = delegate { return true; };
            }

            HttpClient httpClient = new(httpClientHandler);

            try
            {
                var response = httpClient.GetAsync(uriResult).Result;
                ViewBag.HttpResult = response.StatusCode;
                ViewBag.HttpLog = response.Content.ReadAsStringAsync().Result;
            }
            catch (Exception err)
            {
                ViewBag.HttpResult = "Error";
                ViewBag.HttpLog = err;
            }
        }

        return View();
    }
}
