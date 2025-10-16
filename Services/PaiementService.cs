using Microsoft.EntityFrameworkCore;
using Stripe;
using TP1.Data;
using TP1.Models;

namespace TP1.Services
{
    public class PaiementService : IPaiementService
    {
        private readonly ApplicationDbContext _context;

        public PaiementService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<(string ClientSecret, string PaymentIntentId)> CreerPaymentIntentAsync(decimal montant, string devise = "eur")
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)(montant * 100), // Stripe utilise les centimes
                Currency = devise,
                AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                {
                    Enabled = true,
                },
            };

            var service = new PaymentIntentService();
            var paymentIntent = await service.CreateAsync(options);
            return (paymentIntent.ClientSecret, paymentIntent.Id);
        }

        public async Task<Paiement> EnregistrerPaiementAsync(int reservationId, string paymentIntentId, decimal montant)
        {
            var paiement = new Paiement
            {
                ReservationId = reservationId,
                StripePaymentIntentId = paymentIntentId,
                Montant = montant,
                Statut = "EnAttente",
                DatePaiement = DateTime.Now
            };

            _context.Paiements.Add(paiement);
            await _context.SaveChangesAsync();
            return paiement;
        }

        public async Task<bool> ConfirmerPaiementAsync(string paymentIntentId)
        {
            var paiement = await _context.Paiements
                .Include(p => p.Reservation)
                .FirstOrDefaultAsync(p => p.StripePaymentIntentId == paymentIntentId);

            if (paiement == null) return false;

            try
            {
                var service = new PaymentIntentService();
                var paymentIntent = await service.GetAsync(paymentIntentId);

                if (paymentIntent.Status == "succeeded")
                {
                    paiement.Statut = "Reussi";
                    paiement.Reservation.Statut = "Payee";
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (StripeException)
            {
                paiement.Statut = "Echoue";
                await _context.SaveChangesAsync();
            }

            return false;
        }
    }
}


