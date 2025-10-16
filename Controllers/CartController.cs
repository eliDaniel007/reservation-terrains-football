using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TP1.Data;
using TP1.Models;
using TP1.Services;

namespace TP1.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Utilisateur> _userManager;
        private readonly ICreneauService _creneauService;

        public CartController(ApplicationDbContext context, UserManager<Utilisateur> userManager, ICreneauService creneauService)
        {
            _context = context;
            _userManager = userManager;
            _creneauService = creneauService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var panierItems = await _context.PanierItems
                .Include(p => p.Creneau)
                .ThenInclude(c => c.Terrain)
                .Where(p => p.UtilisateurId == userId)
                .ToListAsync();

            ViewBag.Total = panierItems.Sum(p => p.Creneau.Prix * p.Quantite);
            return View(panierItems);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int creneauId, int quantite = 1)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Unauthorized();
            }

            var creneau = await _creneauService.GetCreneauByIdAsync(creneauId);
            if (creneau == null || creneau.PlacesRestantes < quantite)
            {
                TempData["ErrorMessage"] = "Créneau non disponible ou places insuffisantes.";
                return RedirectToAction("Details", "Home", new { id = creneauId });
            }

            var existingItem = await _context.PanierItems
                .FirstOrDefaultAsync(p => p.UtilisateurId == userId && p.CreneauId == creneauId);

            if (existingItem != null)
            {
                if (creneau.PlacesRestantes >= existingItem.Quantite + quantite)
                {
                    existingItem.Quantite += quantite;
                }
                else
                {
                    TempData["ErrorMessage"] = "Places insuffisantes pour cette quantité.";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                var panierItem = new PanierItem
                {
                    UtilisateurId = userId,
                    CreneauId = creneauId,
                    Quantite = quantite,
                    DateAjout = DateTime.Now
                };
                _context.PanierItems.Add(panierItem);
            }

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Créneau ajouté au panier.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int id, int quantite)
        {
            var userId = _userManager.GetUserId(User);
            var panierItem = await _context.PanierItems
                .Include(p => p.Creneau)
                .FirstOrDefaultAsync(p => p.Id == id && p.UtilisateurId == userId);

            if (panierItem == null)
            {
                return NotFound();
            }

            if (quantite <= 0)
            {
                _context.PanierItems.Remove(panierItem);
            }
            else if (panierItem.Creneau.PlacesRestantes >= quantite)
            {
                panierItem.Quantite = quantite;
            }
            else
            {
                TempData["ErrorMessage"] = "Places insuffisantes.";
                return RedirectToAction("Index");
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            var userId = _userManager.GetUserId(User);
            var panierItem = await _context.PanierItems
                .FirstOrDefaultAsync(p => p.Id == id && p.UtilisateurId == userId);

            if (panierItem == null)
            {
                return NotFound();
            }

            _context.PanierItems.Remove(panierItem);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Article retiré du panier.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Clear()
        {
            var userId = _userManager.GetUserId(User);
            var panierItems = await _context.PanierItems
                .Where(p => p.UtilisateurId == userId)
                .ToListAsync();

            _context.PanierItems.RemoveRange(panierItems);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Panier vidé.";
            return RedirectToAction("Index");
        }
    }
}


