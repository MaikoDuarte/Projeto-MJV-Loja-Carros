using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using mjv_aula_10.Models;

namespace mjv_aula_10.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index(string Nome, string Sobrenome)
    {
        var a = ViewBag.Autenticado;
        var b = ViewData["NomeCompleto"];
        var c = TempData["Mensagem"];
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
}