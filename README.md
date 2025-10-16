# 🏟️ Système de Réservation de Terrains de Football

Application web ASP.NET Core MVC pour la réservation de créneaux horaires sur des terrains de football.

## 📋 Table des matières

- [Fonctionnalités](#fonctionnalités)
- [Technologies](#technologies)
- [Prérequis](#prérequis)
- [Installation](#installation)
- [Configuration](#configuration)
- [Utilisation](#utilisation)
- [Architecture](#architecture)
- [Captures d'écran](#captures-décran)
- [Tests](#tests)
- [Déploiement](#déploiement)

## ✨ Fonctionnalités

### Pour les Clients
- ✅ Inscription et connexion sécurisée
- ✅ Parcourir les créneaux disponibles avec filtres (date, type, localisation)
- ✅ Recherche par nom de terrain ou localisation
- ✅ Afficher les détails d'un créneau
- ✅ Ajouter des créneaux au panier
- ✅ Paiement sécurisé via Stripe (sandbox)
- ✅ Télécharger/imprimer les factures
- ✅ Consulter l'historique des réservations
- ✅ Gérer son profil utilisateur

### Pour les Fournisseurs
- ✅ Accès au tableau des revenus
- ✅ Visualiser les réservations par terrain
- ✅ Statistiques de gains cumulés

### Pour les Administrateurs
- ✅ Dashboard avec KPIs (revenus, taux de remplissage, top clients)
- ✅ Gestion complète des réservations
- ✅ Vue d'ensemble des terrains
- ✅ Gestion des utilisateurs
- ✅ Statistiques détaillées

## 🛠️ Technologies

- **Backend**: ASP.NET Core MVC 8.0
- **Base de données**: SQL Server (LocalDB)
- **ORM**: Entity Framework Core 9.0
- **Authentification**: ASP.NET Core Identity
- **Paiement**: Stripe SDK .NET
- **Frontend**: Bootstrap 5, Font Awesome, jQuery
- **UI/UX**: Razor Views, responsive design

## 📦 Prérequis

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server LocalDB](https://learn.microsoft.com/sql/database-engine/configure-windows/sql-server-express-localdb)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) ou [Visual Studio Code](https://code.visualstudio.com/)
- Compte [Stripe](https://stripe.com) (mode test)
- [Postman](https://www.postman.com/) (optionnel, pour tester l'API)

## 🚀 Installation

### 1. Cloner le dépôt

```bash
git clone https://github.com/votre-username/TP1.git
cd TP1
```

### 2. Restaurer les packages NuGet

```bash
dotnet restore
```

### 3. Configurer la base de données

La chaîne de connexion par défaut dans `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ReservationTerrainsDB;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

### 4. Appliquer les migrations

```bash
dotnet ef database update
```

Cela créera automatiquement:
- La base de données `ReservationTerrainsDB`
- Les tables nécessaires
- 3 utilisateurs de test
- 3 terrains avec créneaux pour les 7 prochains jours

### 5. Configurer Stripe

1. Copiez le fichier de configuration exemple:
   ```bash
   cp appsettings.json.example appsettings.json
   ```

2. Créez un compte sur [Stripe](https://stripe.com)
3. Récupérez vos clés API de test (Dashboard → Developers → API keys)
4. Mettez à jour `appsettings.json` avec vos vraies clés:

```json
"Stripe": {
  "PublishableKey": "pk_test_votre_cle_publique",
  "SecretKey": "sk_test_votre_cle_secrete"
}
```

⚠️ **Important**: Le fichier `appsettings.json` est dans `.gitignore` pour protéger vos clés API.

### 6. Lancer l'application

```bash
dotnet run
```

L'application sera accessible sur `https://localhost:5001` (ou le port indiqué dans la console).

## ⚙️ Configuration

### Comptes de test pré-configurés

| Rôle | Email | Mot de passe |
|------|-------|--------------|
| Admin | admin@terrains.com | Admin123! |
| Fournisseur | fournisseur@terrains.com | Fournisseur123! |
| Client | client@terrains.com | Client123! |

### Variables d'environnement (Production)

Pour le déploiement en production, utilisez des variables d'environnement:

```bash
ASPNETCORE_ENVIRONMENT=Production
ConnectionStrings__DefaultConnection="votre_chaine_production"
Stripe__PublishableKey="pk_live_..."
Stripe__SecretKey="sk_live_..."
```

## 📖 Utilisation

### Parcours Client

1. **Inscription**: Créez un compte client via `/Account/Register`
2. **Parcourir**: Explorez les créneaux disponibles sur la page d'accueil
3. **Filtrer**: Utilisez les filtres (date, type de terrain, localisation, recherche)
4. **Détails**: Cliquez sur "Voir détails" pour plus d'informations
5. **Panier**: Ajoutez des créneaux au panier
6. **Paiement**: Procédez au paiement sécurisé via Stripe
7. **Confirmation**: Recevez votre confirmation et téléchargez votre facture

### Carte de test Stripe

Pour tester les paiements en mode sandbox:

- **Numéro**: 4242 4242 4242 4242
- **Date**: N'importe quelle date future
- **CVC**: N'importe quel code à 3 chiffres
- **ZIP**: N'importe quel code postal

## 🏗️ Architecture

### Structure du projet

```
TP1/
├── Controllers/           # Contrôleurs MVC
│   ├── AccountController.cs
│   ├── AdminController.cs
│   ├── CartController.cs
│   ├── CheckoutController.cs
│   ├── HomeController.cs
│   └── ReservationsController.cs
├── Models/               # Modèles de données
│   ├── ViewModels/       # ViewModels pour les vues
│   ├── Utilisateur.cs
│   ├── Terrain.cs
│   ├── Creneau.cs
│   ├── PanierItem.cs
│   ├── Reservation.cs
│   ├── Paiement.cs
│   └── Facture.cs
├── Views/                # Vues Razor
│   ├── Account/
│   ├── Admin/
│   ├── Cart/
│   ├── Checkout/
│   ├── Home/
│   ├── Reservations/
│   └── Shared/
├── Data/                 # Contexte et seeding
│   ├── ApplicationDbContext.cs
│   └── DbInitializer.cs
├── Services/             # Services métier
│   ├── CreneauService.cs
│   ├── PaiementService.cs
│   └── FactureService.cs
├── wwwroot/              # Fichiers statiques
│   ├── css/
│   ├── js/
│   └── lib/
├── Migrations/           # Migrations EF Core
├── appsettings.json      # Configuration
└── Program.cs            # Point d'entrée
```

### Modèle de données

```
Utilisateur (Identity)
  ├── PanierItems (1:N)
  └── Reservations (1:N)

Terrain
  └── Creneaux (1:N)

Creneau
  ├── PanierItems (1:N)
  └── Reservations (1:N)

Reservation
  ├── Paiement (1:1)
  └── Facture (1:1)
```

## 🧪 Tests

### Tests manuels avec Postman

Collection Postman disponible (à créer) pour tester:
- Les endpoints d'authentification
- Les opérations CRUD sur les réservations
- Le processus de paiement

### Tests des fonctionnalités

**✓ Authentification et autorisation**
- [x] Inscription client/fournisseur
- [x] Connexion/déconnexion
- [x] Gestion du profil
- [x] Restrictions par rôle (Admin, Fournisseur, Client)

**✓ Gestion des créneaux**
- [x] Affichage des créneaux disponibles
- [x] Filtres: date, type de terrain, localisation
- [x] Recherche par nom/localisation
- [x] Affichage des détails

**✓ Panier & Réservations**
- [x] Ajout/suppression d'articles
- [x] Modification des quantités
- [x] Calcul du total
- [x] Validation des places disponibles

**✓ Paiement Stripe**
- [x] Création du PaymentIntent
- [x] Interface de paiement sécurisée
- [x] Gestion des erreurs
- [x] Confirmation du paiement

**✓ Facturation**
- [x] Génération automatique après paiement
- [x] Numéro de facture unique
- [x] Téléchargement/impression PDF
- [x] Historique des factures

**✓ Dashboard Administrateur**
- [x] KPIs: revenus, réservations, taux de conversion
- [x] Top 10 clients
- [x] Taux de remplissage par terrain
- [x] Réservations récentes

**✓ Interface utilisateur**
- [x] Design responsive (mobile, tablette, desktop)
- [x] Bootstrap 5 + Font Awesome
- [x] Page 404 personnalisée
- [x] Messages de succès/erreur
- [x] Navigation intuitive

## 🚢 Déploiement

### Déploiement sur Azure

1. Créez une Azure SQL Database
2. Créez une App Service
3. Configurez les variables d'environnement
4. Publiez l'application

```bash
dotnet publish -c Release
```

### Déploiement Docker (optionnel)

Créez un `Dockerfile`:

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["TP1.csproj", "./"]
RUN dotnet restore "TP1.csproj"
COPY . .
RUN dotnet build "TP1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TP1.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TP1.dll"]
```

## 📝 Checklist du projet

### ✅ Fonctionnalités de base
- [x] Système d'authentification (Login, Register, Profile)
- [x] Gestion des rôles (Client, Fournisseur, Admin)
- [x] Parcours et filtrage des créneaux
- [x] Panier de réservations multi-créneaux
- [x] Paiement électronique Stripe (sandbox)
- [x] Génération et téléchargement de factures
- [x] Historique des réservations client
- [x] Tableau des revenus fournisseur
- [x] Dashboard administrateur

### ✅ Pages requises
- [x] Accueil avec liste des créneaux
- [x] Détails d'un créneau
- [x] Connexion
- [x] Inscription
- [x] Profil utilisateur
- [x] Panier
- [x] Paiement
- [x] Historique/Factures
- [x] Dashboard Admin
- [x] Page 404 personnalisée

### ✅ Techniques
- [x] ASP.NET Core MVC
- [x] Entity Framework Core (migrations, relations)
- [x] ASP.NET Core Identity
- [x] Stripe.NET SDK
- [x] Bootstrap 5
- [x] Seeding de données
- [x] Gestion des erreurs
- [x] Variables d'environnement

## 👥 Auteur

**Votre Nom** - TP1 Réservation de Terrains

## 📄 Licence

Ce projet est réalisé dans le cadre d'un travail pratique universitaire.

## 🙏 Remerciements

- [Bootstrap](https://getbootstrap.com/)
- [Font Awesome](https://fontawesome.com/)
- [Stripe](https://stripe.com/)
- [Unsplash](https://unsplash.com/) pour les images de terrains

---

**Date de remise**: 27 octobre 2025, 23h00

**Note**: Ce README sera complété avec des captures d'écran et une vidéo de démonstration avant la remise finale.


