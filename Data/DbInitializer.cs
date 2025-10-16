using Microsoft.AspNetCore.Identity;
using TP1.Models;

namespace TP1.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(ApplicationDbContext context, UserManager<Utilisateur> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Créer la base de données si elle n'existe pas
            await context.Database.EnsureCreatedAsync();

            // Créer les rôles
            string[] roleNames = { "Admin", "Client", "Fournisseur" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Créer un admin par défaut
            var adminEmail = "admin@terrains.com";
            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var admin = new Utilisateur
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    Nom = "Administrateur",
                    Role = "Admin",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(admin, "Admin123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }

            // Créer un fournisseur par défaut
            var fournisseurEmail = "fournisseur@terrains.com";
            if (await userManager.FindByEmailAsync(fournisseurEmail) == null)
            {
                var fournisseur = new Utilisateur
                {
                    UserName = fournisseurEmail,
                    Email = fournisseurEmail,
                    Nom = "Fournisseur Test",
                    Role = "Fournisseur",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(fournisseur, "Fournisseur123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(fournisseur, "Fournisseur");
                }
            }

            // Créer un client par défaut
            var clientEmail = "client@terrains.com";
            if (await userManager.FindByEmailAsync(clientEmail) == null)
            {
                var client = new Utilisateur
                {
                    UserName = clientEmail,
                    Email = clientEmail,
                    Nom = "Client Test",
                    Role = "Client",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(client, "Client123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(client, "Client");
                }
            }

            // Vérifier si des terrains existent déjà
            if (context.Terrains.Any())
            {
                return; // La DB est déjà seeded
            }

            // Créer les terrains
            var terrains = new Terrain[]
            {
                new Terrain
                {
                    Nom = "Terrain Central",
                    Type = "11-a-side",
                    Localisation = "Paris 15e, Avenue de Suffren",
                    ImageUrl = "https://images.unsplash.com/photo-1574629810360-7efbbe195018?w=800",
                    Description = "Grand terrain de football en gazon synthétique de dernière génération. Équipé d'éclairage nocturne et de vestiaires modernes."
                },
                new Terrain
                {
                    Nom = "Terrain Express",
                    Type = "5-a-side",
                    Localisation = "Paris 10e, Rue de la Grange aux Belles",
                    ImageUrl = "https://images.unsplash.com/photo-1529900748604-07564a03e7a6?w=800",
                    Description = "Petit terrain couvert parfait pour les matchs rapides entre amis. Disponible 7j/7 avec réservation en ligne."
                },
                new Terrain
                {
                    Nom = "Stade Municipal",
                    Type = "7-a-side",
                    Localisation = "Paris 13e, Boulevard Vincent Auriol",
                    ImageUrl = "https://images.unsplash.com/photo-1543351611-58f69d7c1781?w=800",
                    Description = "Terrain semi-professionnel avec tribunes. Idéal pour tournois et compétitions amicales."
                }
            };

            context.Terrains.AddRange(terrains);
            await context.SaveChangesAsync();

            // Créer les créneaux pour les 7 prochains jours
            var creneaux = new List<Creneau>();
            var today = DateTime.Today;

            for (int day = 0; day < 7; day++)
            {
                var date = today.AddDays(day);

                foreach (var terrain in terrains)
                {
                    // Créneaux matin (9h-12h)
                    creneaux.Add(new Creneau
                    {
                        TerrainId = terrain.Id,
                        Date = date,
                        HeureDebut = new TimeSpan(9, 0, 0),
                        HeureFin = new TimeSpan(10, 30, 0),
                        Prix = terrain.Type == "11-a-side" ? 80 : terrain.Type == "7-a-side" ? 50 : 30,
                        Capacite = terrain.Type == "11-a-side" ? 22 : terrain.Type == "7-a-side" ? 14 : 10,
                        PlacesRestantes = terrain.Type == "11-a-side" ? 22 : terrain.Type == "7-a-side" ? 14 : 10
                    });

                    creneaux.Add(new Creneau
                    {
                        TerrainId = terrain.Id,
                        Date = date,
                        HeureDebut = new TimeSpan(10, 30, 0),
                        HeureFin = new TimeSpan(12, 0, 0),
                        Prix = terrain.Type == "11-a-side" ? 80 : terrain.Type == "7-a-side" ? 50 : 30,
                        Capacite = terrain.Type == "11-a-side" ? 22 : terrain.Type == "7-a-side" ? 14 : 10,
                        PlacesRestantes = terrain.Type == "11-a-side" ? 22 : terrain.Type == "7-a-side" ? 14 : 10
                    });

                    // Créneaux après-midi (14h-18h)
                    creneaux.Add(new Creneau
                    {
                        TerrainId = terrain.Id,
                        Date = date,
                        HeureDebut = new TimeSpan(14, 0, 0),
                        HeureFin = new TimeSpan(15, 30, 0),
                        Prix = terrain.Type == "11-a-side" ? 90 : terrain.Type == "7-a-side" ? 60 : 35,
                        Capacite = terrain.Type == "11-a-side" ? 22 : terrain.Type == "7-a-side" ? 14 : 10,
                        PlacesRestantes = terrain.Type == "11-a-side" ? 22 : terrain.Type == "7-a-side" ? 14 : 10
                    });

                    creneaux.Add(new Creneau
                    {
                        TerrainId = terrain.Id,
                        Date = date,
                        HeureDebut = new TimeSpan(16, 0, 0),
                        HeureFin = new TimeSpan(17, 30, 0),
                        Prix = terrain.Type == "11-a-side" ? 90 : terrain.Type == "7-a-side" ? 60 : 35,
                        Capacite = terrain.Type == "11-a-side" ? 22 : terrain.Type == "7-a-side" ? 14 : 10,
                        PlacesRestantes = terrain.Type == "11-a-side" ? 22 : terrain.Type == "7-a-side" ? 14 : 10
                    });

                    // Créneaux soir (18h-22h)
                    creneaux.Add(new Creneau
                    {
                        TerrainId = terrain.Id,
                        Date = date,
                        HeureDebut = new TimeSpan(18, 0, 0),
                        HeureFin = new TimeSpan(19, 30, 0),
                        Prix = terrain.Type == "11-a-side" ? 100 : terrain.Type == "7-a-side" ? 70 : 40,
                        Capacite = terrain.Type == "11-a-side" ? 22 : terrain.Type == "7-a-side" ? 14 : 10,
                        PlacesRestantes = terrain.Type == "11-a-side" ? 22 : terrain.Type == "7-a-side" ? 14 : 10
                    });

                    creneaux.Add(new Creneau
                    {
                        TerrainId = terrain.Id,
                        Date = date,
                        HeureDebut = new TimeSpan(20, 0, 0),
                        HeureFin = new TimeSpan(21, 30, 0),
                        Prix = terrain.Type == "11-a-side" ? 100 : terrain.Type == "7-a-side" ? 70 : 40,
                        Capacite = terrain.Type == "11-a-side" ? 22 : terrain.Type == "7-a-side" ? 14 : 10,
                        PlacesRestantes = terrain.Type == "11-a-side" ? 22 : terrain.Type == "7-a-side" ? 14 : 10
                    });
                }
            }

            context.Creneaux.AddRange(creneaux);
            await context.SaveChangesAsync();
        }
    }
}


