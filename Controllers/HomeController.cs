using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TP1.Models;
using TP1.Services;

namespace TP1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICreneauService _creneauService;

        public HomeController(ILogger<HomeController> logger, ICreneauService creneauService)
        {
            _logger = logger;
            _creneauService = creneauService;
        }

        public async Task<IActionResult> Index(DateTime? date, string? typeTerrain, string? localisation, string? recherche)
        {
            ViewBag.Date = date;
            ViewBag.TypeTerrain = typeTerrain;
            ViewBag.Localisation = localisation;
            ViewBag.Recherche = recherche;

            var creneaux = await _creneauService.GetCreneauxDisponiblesAsync(date, typeTerrain, localisation, recherche);
            return View(creneaux);
        }

        public async Task<IActionResult> Details(int id)
        {
            var creneau = await _creneauService.GetCreneauByIdAsync(id);
            if (creneau == null)
            {
                return NotFound();
            }

            return View(creneau);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int? statusCode = null)
        {
            if (statusCode == 404)
            {
                return View("NotFound");
            }

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
