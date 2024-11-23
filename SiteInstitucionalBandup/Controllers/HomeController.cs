using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SiteInstitucionalBandup.Models;
using SiteInstitucionalBandup.Data;


namespace SiteInstitucionalBandup.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _context;

    public HomeController(ILogger<HomeController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

   public IActionResult Marcas()
    {
        var marcas = _context.Marcas.ToList();
        return View(marcas);
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
