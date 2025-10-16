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
    public class ReservationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Utilisateur> _userManager;
        private readonly IFactureService _factureService;

        public ReservationsController(
            ApplicationDbContext context,
            UserManager<Utilisateur> userManager,
            IFactureService factureService)
        {
            _context = context;
            _userManager = userManager;
            _factureService = factureService;
        }

        [HttpGet]
        public async Task<IActionResult> MyBookings()
        {
            var userId = _userManager.GetUserId(User);
            var reservations = await _context.Reservations
                .Include(r => r.Creneau)
                .ThenInclude(c => c.Terrain)
                .Where(r => r.UtilisateurId == userId)
                .OrderByDescending(r => r.DateReservation)
                .ToListAsync();

            return View(reservations);
        }

        [HttpGet]
        public async Task<IActionResult> History()
        {
            var userId = _userManager.GetUserId(User);
            var factures = await _factureService.GetFacturesByUtilisateurAsync(userId!);

            return View(factures);
        }

        [HttpGet]
        public async Task<IActionResult> Invoice(int id)
        {
            var userId = _userManager.GetUserId(User);
            var facture = await _factureService.GetFactureByReservationIdAsync(id);

            if (facture == null || facture.Reservation.UtilisateurId != userId)
            {
                return NotFound();
            }

            return View(facture);
        }

        [HttpGet]
        [Authorize(Roles = "Fournisseur,Admin")]
        public async Task<IActionResult> Revenue()
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound();
            }

            // Pour un fournisseur, on calcule les revenus de tous les terrains
            // (Dans une vraie app, on lierait les terrains aux fournisseurs)
            var reservations = await _context.Reservations
                .Include(r => r.Creneau)
                .ThenInclude(c => c.Terrain)
                .Include(r => r.Utilisateur)
                .Where(r => r.Statut == "Payee")
                .OrderByDescending(r => r.DateReservation)
                .ToListAsync();

            var totalRevenu = reservations.Sum(r => r.MontantTotal);
            var reservationsParMois = reservations
                .GroupBy(r => new { r.DateReservation.Year, r.DateReservation.Month })
                .Select(g => new
                {
                    Date = new DateTime(g.Key.Year, g.Key.Month, 1),
                    Total = g.Sum(r => r.MontantTotal),
                    Count = g.Count()
                })
                .OrderByDescending(x => x.Date)
                .ToList();

            ViewBag.TotalRevenu = totalRevenu;
            ViewBag.ReservationsParMois = reservationsParMois;
            ViewBag.TotalReservations = reservations.Count;

            return View(reservations);
        }
    }
}


