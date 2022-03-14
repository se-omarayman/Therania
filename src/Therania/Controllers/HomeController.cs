using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Therania.Models;

namespace Therania.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        ViewData["test"] = "test success";
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
    
    public IActionResult Pricing()
    {
        return View();
    }
    
    public IActionResult About()
    {
        return View();
    }

    public IActionResult Contact()
    {
        return View();
    }
    public IActionResult Faq()
    {
        return View();
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}