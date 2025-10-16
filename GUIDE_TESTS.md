# 🧪 Guide de Tests - Système de Réservation de Terrains

## ⚠️ Pré-requis avant de tester

### Configuration Stripe OBLIGATOIRE
Avant de lancer les tests, vous **DEVEZ** configurer vos clés Stripe dans `appsettings.json` :

```json
"Stripe": {
  "PublishableKey": "pk_test_votre_cle_stripe",
  "SecretKey": "sk_test_votre_cle_stripe"
}
```

**Comment obtenir vos clés :**
1. Créez un compte sur https://stripe.com
2. Activez le mode Test
3. Allez dans Développeurs > Clés API
4. Copiez la clé publique (pk_test_...) et la clé secrète (sk_test_...)

---

## 📋 Checklist de Tests Fonctionnels

### ✅ Phase 1 : Navigation & Interface

**Test 1.1 - Page d'accueil**
- [ ] Ouvrir `https://localhost:5001`
- [ ] Vérifier l'affichage de la hero section
- [ ] Vérifier que les filtres s'affichent correctement
- [ ] Vérifier l'affichage des créneaux (devrait montrer ~126 créneaux pour 7 jours)
- [ ] Les images des terrains s'affichent-elles ?

**Test 1.2 - Filtrage & Recherche**
- [ ] Filtrer par date (sélectionner aujourd'hui)
- [ ] Filtrer par type de terrain (5-a-side, 7-a-side, 11-a-side)
- [ ] Filtrer par localisation (ex: "Paris 15e")
- [ ] Tester la recherche (ex: "Central")
- [ ] Combiner plusieurs filtres ensemble

**Test 1.3 - Responsive Design**
- [ ] Réduire la fenêtre du navigateur (mobile)
- [ ] Vérifier que le menu hamburger fonctionne
- [ ] Vérifier que les cartes s'empilent correctement

---

### ✅ Phase 2 : Authentification

**Test 2.1 - Inscription Client**
- [ ] Cliquer sur "Inscription" dans la navbar
- [ ] Remplir le formulaire avec :
  - Nom : "Test Client"
  - Email : "test@client.com"
  - Mot de passe : "Test123!"
  - Confirmer mot de passe : "Test123!"
  - Rôle : Client
- [ ] Soumettre le formulaire
- [ ] Vérifier la redirection vers l'accueil
- [ ] Vérifier que le nom s'affiche dans la navbar

**Test 2.2 - Déconnexion**
- [ ] Cliquer sur le dropdown du nom d'utilisateur
- [ ] Cliquer sur "Déconnexion"
- [ ] Vérifier la redirection et disparition du nom

**Test 2.3 - Connexion**
- [ ] Cliquer sur "Connexion"
- [ ] Utiliser les credentials :
  - Email : "client@terrains.com"
  - Mot de passe : "Client123!"
- [ ] Cocher "Se souvenir de moi"
- [ ] Vérifier la connexion réussie

**Test 2.4 - Comptes de test**
Tester la connexion avec chaque rôle :
- [ ] Client : client@terrains.com / Client123!
- [ ] Fournisseur : fournisseur@terrains.com / Fournisseur123!
- [ ] Admin : admin@terrains.com / Admin123!

---

### ✅ Phase 3 : Détails & Panier

**Test 3.1 - Page Détails**
- [ ] Cliquer sur "Voir détails" d'un créneau
- [ ] Vérifier l'affichage de toutes les informations :
  - Image du terrain
  - Nom et localisation
  - Date et horaire
  - Type de terrain
  - Prix et places disponibles
  - Description

**Test 3.2 - Ajout au Panier (Non connecté)**
- [ ] Se déconnecter si connecté
- [ ] Aller sur un créneau
- [ ] Vérifier qu'on voit "Vous devez être connecté"
- [ ] Cliquer sur "Se connecter"
- [ ] Vérifier la redirection après connexion

**Test 3.3 - Ajout au Panier (Connecté)**
- [ ] Se connecter en tant que client
- [ ] Aller sur un créneau
- [ ] Sélectionner une quantité (ex: 2 places)
- [ ] Cliquer sur "Ajouter au panier"
- [ ] Vérifier le message de succès
- [ ] Vérifier la redirection vers le panier

**Test 3.4 - Gestion du Panier**
- [ ] Dans le panier, vérifier l'affichage des articles
- [ ] Modifier la quantité avec les boutons +/-
- [ ] Vérifier le recalcul du total
- [ ] Ajouter un autre créneau
- [ ] Vérifier que les deux sont dans le panier
- [ ] Retirer un article avec le bouton X
- [ ] Vérifier la mise à jour

---

### ✅ Phase 4 : Paiement Stripe (CRITIQUE)

**⚠️ IMPORTANT : Configurez Stripe avant ce test !**

**Test 4.1 - Préparation du Paiement**
- [ ] Avoir au moins 1 article dans le panier
- [ ] Cliquer sur "Procéder au paiement"
- [ ] Vérifier l'affichage du récapitulatif
- [ ] Vérifier le montant total correct

**Test 4.2 - Interface Stripe**
- [ ] Vérifier que l'interface de paiement Stripe s'affiche
- [ ] Si erreur "Invalid API Key" → Configurez vos clés Stripe !

**Test 4.3 - Paiement Test**
Utiliser ces informations de carte de test :
- **Numéro** : 4242 4242 4242 4242
- **Date** : 12/34 (ou n'importe quelle date future)
- **CVC** : 123
- **Code postal** : 75001

- [ ] Remplir le formulaire de paiement
- [ ] Cliquer sur "Payer XX,XX €"
- [ ] Vérifier l'affichage du spinner de chargement
- [ ] Attendre la confirmation
- [ ] Vérifier la redirection vers la page de succès

**Test 4.4 - Vérification Post-Paiement**
- [ ] Sur la page de succès, cliquer sur "Voir mes réservations"
- [ ] Vérifier que la réservation apparaît avec statut "Confirmée"
- [ ] Vérifier que le panier est vide

**Test 4.5 - Dashboard Stripe**
- [ ] Se connecter sur dashboard.stripe.com
- [ ] Aller dans Paiements
- [ ] Vérifier que le paiement test apparaît

---

### ✅ Phase 5 : Factures

**Test 5.1 - Génération de Facture**
- [ ] Aller dans "Factures" (menu)
- [ ] Vérifier qu'une facture a été créée
- [ ] Vérifier le numéro de facture (format: FAC-YYYYMMDD-XXXXXX)

**Test 5.2 - Détails de la Facture**
- [ ] Cliquer sur "Voir" une facture
- [ ] Vérifier toutes les informations :
  - Numéro de facture
  - Date
  - Informations client
  - Détails de la réservation
  - Montant total
- [ ] Cliquer sur "Imprimer"
- [ ] Vérifier l'aperçu avant impression

**Test 5.3 - Historique des Réservations**
- [ ] Aller dans "Mes Réservations"
- [ ] Vérifier l'affichage de toutes les réservations
- [ ] Vérifier les badges de statut (couleurs)
- [ ] Cliquer sur "Facture" d'une réservation

---

### ✅ Phase 6 : Fournisseur

**Test 6.1 - Connexion Fournisseur**
- [ ] Se déconnecter
- [ ] Se connecter avec : fournisseur@terrains.com / Fournisseur123!
- [ ] Vérifier que le menu "Revenus" apparaît

**Test 6.2 - Page Revenus**
- [ ] Cliquer sur "Revenus"
- [ ] Vérifier l'affichage des KPIs :
  - Revenu Total
  - Nombre de réservations payées
  - Panier moyen
- [ ] Vérifier le tableau des réservations
- [ ] Vérifier les informations des clients

---

### ✅ Phase 7 : Administrateur

**Test 7.1 - Connexion Admin**
- [ ] Se déconnecter
- [ ] Se connecter avec : admin@terrains.com / Admin123!
- [ ] Vérifier que le menu "Admin" apparaît

**Test 7.2 - Dashboard Admin**
- [ ] Cliquer sur "Admin" > "Dashboard"
- [ ] Vérifier les 4 KPIs principaux :
  - Total Réservations
  - Réservations Confirmées
  - Revenu Total
  - Taux de Conversion
- [ ] Vérifier le Top 10 Clients (devrait être vide si pas de réservations)
- [ ] Vérifier le Taux de Remplissage par terrain
- [ ] Vérifier les Réservations Récentes

**Test 7.3 - Gestion des Réservations**
- [ ] Cliquer sur "Admin" > "Réservations"
- [ ] Vérifier le tableau complet
- [ ] Vérifier les informations affichées
- [ ] Vérifier les badges de statut

**Test 7.4 - Gestion des Terrains**
- [ ] Cliquer sur "Admin" > "Terrains"
- [ ] Vérifier l'affichage des 3 terrains
- [ ] Vérifier les statistiques (créneaux, complets)

**Test 7.5 - Gestion des Utilisateurs**
- [ ] Cliquer sur "Admin" > "Utilisateurs"
- [ ] Vérifier la liste des utilisateurs
- [ ] Vérifier les rôles affichés (badges)
- [ ] Vérifier les statistiques par utilisateur

---

### ✅ Phase 8 : Profil Utilisateur

**Test 8.1 - Consultation du Profil**
- [ ] Se connecter (n'importe quel rôle)
- [ ] Cliquer sur le nom d'utilisateur > "Profil"
- [ ] Vérifier l'affichage des informations :
  - Nom
  - Email
  - Rôle (lecture seule)
  - Date d'inscription (lecture seule)

**Test 8.2 - Modification du Profil**
- [ ] Modifier le nom (ex: "Nouveau Nom")
- [ ] Cliquer sur "Enregistrer"
- [ ] Vérifier le message de succès
- [ ] Vérifier que le nom est mis à jour dans la navbar

**Test 8.3 - Changement de Mot de Passe**
- [ ] Remplir "Nouveau mot de passe" : "NewPass123!"
- [ ] Remplir "Confirmer" : "NewPass123!"
- [ ] Cliquer sur "Enregistrer"
- [ ] Se déconnecter
- [ ] Se reconnecter avec le nouveau mot de passe

---

### ✅ Phase 9 : Pages d'Erreur

**Test 9.1 - Page 404**
- [ ] Accéder à une URL inexistante : `https://localhost:5001/page-inexistante`
- [ ] Vérifier l'affichage de la page 404 personnalisée
- [ ] Cliquer sur "Retour à l'accueil"

**Test 9.2 - Accès Refusé**
- [ ] Se connecter en tant que Client
- [ ] Essayer d'accéder à : `https://localhost:5001/Admin/Dashboard`
- [ ] Vérifier la redirection vers la page "Accès refusé"

---

### ✅ Phase 10 : Tests de Sécurité

**Test 10.1 - Protection des Routes**
- [ ] Se déconnecter complètement
- [ ] Essayer d'accéder à `/Cart` → Redirection vers Login
- [ ] Essayer d'accéder à `/Checkout` → Redirection vers Login
- [ ] Essayer d'accéder à `/Admin/Dashboard` → Redirection vers Login

**Test 10.2 - Validation des Formulaires**
- [ ] Sur la page d'inscription, soumettre un formulaire vide
- [ ] Vérifier les messages d'erreur de validation
- [ ] Tester avec un email invalide
- [ ] Tester avec un mot de passe trop court

**Test 10.3 - Gestion des Places**
- [ ] Ajouter un créneau au panier
- [ ] Essayer d'ajouter plus de places que disponible
- [ ] Vérifier le message d'erreur

---

## 🐛 Tests de Bugs Potentiels

### Bug Check 1 : Stripe non configuré
**Symptôme** : Erreur au moment du paiement "Invalid API Key"
**Solution** : Configurer les clés Stripe dans `appsettings.json`

### Bug Check 2 : Base de données vide
**Symptôme** : Aucun créneau affiché sur l'accueil
**Solution** : Relancer `dotnet ef database update` ou vérifier que le seeding s'est bien exécuté

### Bug Check 3 : Erreur de connexion SQL
**Symptôme** : Erreur au démarrage de l'application
**Solution** : Vérifier que LocalDB est installé et accessible

### Bug Check 4 : Images non affichées
**Symptôme** : Les images des terrains ne s'affichent pas
**Solution** : Normal, les images proviennent d'Unsplash (nécessite connexion internet)

---

## 📊 Résultats Attendus

### Après tous les tests, vous devriez avoir :
- ✅ Au moins 1 compte client créé manuellement
- ✅ Au moins 1 réservation payée avec succès
- ✅ Au moins 1 facture générée
- ✅ Les 3 terrains visibles
- ✅ Environ 126 créneaux affichés (3 terrains × 6 créneaux × 7 jours)
- ✅ Dashboard admin avec statistiques
- ✅ Paiement visible dans le dashboard Stripe

---

## 🎥 Préparation de la Vidéo de Démonstration

Pour la vidéo OBS, suivez ce scénario :

1. **Introduction** (30 sec)
   - Montrer la page d'accueil
   - Expliquer le concept

2. **Parcours Client** (3 min)
   - Inscription
   - Navigation et filtrage
   - Détails d'un créneau
   - Ajout au panier
   - Paiement Stripe
   - Consultation de la facture

3. **Parcours Fournisseur** (1 min)
   - Connexion
   - Page des revenus

4. **Parcours Admin** (2 min)
   - Dashboard avec KPIs
   - Gestion des réservations
   - Gestion des terrains et utilisateurs

5. **Conclusion** (30 sec)
   - Résumer les fonctionnalités
   - Montrer la page 404

**Durée totale recommandée : 7-10 minutes**

---

## 📝 Checklist Finale avant Remise

- [ ] Tous les tests passent
- [ ] Screenshots pris pour le rapport
- [ ] Vidéo enregistrée et validée
- [ ] Code commenté si nécessaire
- [ ] README.md à jour
- [ ] Rapport LaTeX complété
- [ ] Projet zippé
- [ ] Vérification de la date limite : **27 octobre 2025, 23h00**

---

**Bon courage pour les tests ! 🚀**

