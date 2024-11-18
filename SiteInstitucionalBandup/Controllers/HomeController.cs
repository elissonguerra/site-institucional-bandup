using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SiteInstitucionalBandup.Models;

namespace SiteInstitucionalBandup.Controllers;

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

    public IActionResult Marcas()
    {
        return View();
    }

    public IActionResult Cosmos()
    {
        return View();
    }

    public IActionResult Eventos()
    {
        return View();
    }

    public IActionResult TrabalheConosco()
    {
        return View();
    }

    public IActionResult Lojas()
    {
        return View();
    }

    public IActionResult Historia()
    {
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
