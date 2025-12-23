using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HediyelikEsya.Models;

namespace HediyelikEsya.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    // ANA SAYFA
    public IActionResult Index()
    {
        return View();
    }

    // HAKKINDA SAYFASI
    public IActionResult Hakkinda()
    {
        return View();
    }

    // --- YENİ EKLENEN: TASARIM (KONFİGÜRATÖR) SAYFASI ---
    public IActionResult Konfigurator()
    {
        return View();
    }

    // GİZLİLİK SAYFASI (Hakkımızda buraya yönleniyorsa bunu kullanıyoruz)
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