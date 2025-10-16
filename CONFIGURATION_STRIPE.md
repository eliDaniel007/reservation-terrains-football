# 💳 Guide de Configuration Stripe

## ⚠️ IMPORTANT
Sans configuration Stripe, **le paiement ne fonctionnera pas**. Suivez ce guide étape par étape.

---

## 🚀 Étape 1 : Créer un compte Stripe

1. Allez sur https://stripe.com
2. Cliquez sur **"Start now"** ou **"S'inscrire"**
3. Remplissez le formulaire avec :
   - Email
   - Nom complet
   - Pays : **Canada** ou votre pays
   - Mot de passe

4. Confirmez votre email (vérifiez votre boîte de réception)

---

## 🔧 Étape 2 : Activer le mode Test

Une fois connecté au dashboard Stripe :

1. En haut à droite, vous verrez un toggle **"Mode test"**
2. **Assurez-vous qu'il est activé** (couleur bleue/orange)
3. Si vous voyez "Mode réel" ou "Live mode", cliquez pour basculer en mode test

> ⚡ **TOUJOURS utiliser le mode test pour le développement !**

---

## 🔑 Étape 3 : Récupérer vos clés API

### 3.1 Accéder aux clés

1. Dans le menu de gauche, cliquez sur **"Développeurs"** ou **"Developers"**
2. Cliquez sur **"Clés API"** ou **"API keys"**
3. Vous verrez deux types de clés :
   - **Clé publiable** (Publishable key) : commence par `pk_test_`
   - **Clé secrète** (Secret key) : commence par `sk_test_`

### 3.2 Copier les clés

#### Option 1 : Clés standard (recommandé pour le TP)

**Clé publiable** :
```
pk_test_51...
```
- Cliquez sur l'icône 👁️ pour révéler
- Cliquez sur l'icône 📋 pour copier

**Clé secrète** :
```
sk_test_51...
```
- Cliquez sur "Révéler la clé de test en direct"
- Copiez la clé **IMMÉDIATEMENT** (elle ne sera plus affichée après)

> ⚠️ Si vous perdez la clé secrète, vous devrez en créer une nouvelle !

#### Option 2 : Créer de nouvelles clés

Si vous voulez créer des clés spécifiques pour ce projet :

1. Cliquez sur **"+ Créer une clé secrète"**
2. Donnez-lui un nom : "TP1_Reservation_Terrains"
3. Sélectionnez les permissions nécessaires (par défaut = OK)
4. Cliquez sur "Créer"
5. **COPIEZ LA CLÉ IMMÉDIATEMENT** (elle ne sera plus affichée)

---

## 📝 Étape 4 : Configurer appsettings.json

### 4.1 Ouvrir le fichier

Ouvrez le fichier `appsettings.json` à la racine du projet TP1.

### 4.2 Trouver la section Stripe

Cherchez cette section :

```json
"Stripe": {
  "PublishableKey": "pk_test_51OX...",
  "SecretKey": "sk_test_51OX..."
}
```

### 4.3 Remplacer les clés

Remplacez les valeurs par VOS clés :

```json
"Stripe": {
  "PublishableKey": "pk_test_VOTRE_CLE_PUBLIQUE_ICI",
  "SecretKey": "sk_test_VOTRE_CLE_SECRETE_ICI"
}
```

### 4.4 Exemple complet

Votre fichier `appsettings.json` devrait ressembler à ceci :

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ReservationTerrainsDB;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  "Stripe": {
    "PublishableKey": "pk_test_VOTRE_CLE_PUBLIQUE_ICI",
    "SecretKey": "sk_test_VOTRE_CLE_SECRETE_ICI"
  }
}
```

### 4.5 Sauvegarder

- Appuyez sur **Ctrl+S** (Windows/Linux) ou **Cmd+S** (Mac)
- Fermez le fichier

---

## ✅ Étape 5 : Vérifier la configuration

### 5.1 Redémarrer l'application

Si l'application est en cours d'exécution :

1. Dans le terminal, appuyez sur **Ctrl+C** pour l'arrêter
2. Relancez avec `dotnet run`

### 5.2 Tester le paiement

1. Allez sur votre site : https://localhost:5001
2. Connectez-vous (ou inscrivez-vous)
3. Ajoutez un créneau au panier
4. Cliquez sur "Procéder au paiement"

**Si tout est bien configuré, vous devriez voir :**
- ✅ Le formulaire de paiement Stripe s'affiche
- ✅ Les champs de carte apparaissent
- ✅ Aucune erreur "Invalid API Key"

**Si vous voyez une erreur :**
- ❌ "Invalid API Key" → Vérifiez que vous avez copié les bonnes clés
- ❌ "publishableKey" error → Vérifiez la clé publique
- ❌ Interface ne s'affiche pas → Ouvrez la console développeur (F12)

---

## 💳 Étape 6 : Tester un paiement

### 6.1 Cartes de test Stripe

Stripe fournit des numéros de carte de test. Utilisez **UNIQUEMENT** ces cartes en mode test :

#### Carte de succès (recommandée)
```
Numéro:    4242 4242 4242 4242
Date:      12/34 (ou n'importe quelle date future)
CVC:       123 (ou n'importe quel code à 3 chiffres)
Code postal: 75001 (ou n'importe quel code)
```

#### Autres cartes de test

**Carte nécessitant authentification 3D Secure :**
```
Numéro: 4000 0027 6000 3184
```

**Carte refusée (insuffisance de fonds) :**
```
Numéro: 4000 0000 0000 9995
```

**Carte refusée (générique) :**
```
Numéro: 4000 0000 0000 0002
```

### 6.2 Effectuer un paiement test

1. Remplissez le formulaire avec la carte `4242 4242 4242 4242`
2. Cliquez sur "Payer XX,XX €"
3. Attendez la confirmation
4. Vous devriez être redirigé vers la page de succès

### 6.3 Vérifier dans Stripe Dashboard

1. Allez sur https://dashboard.stripe.com/test/payments
2. Vous devriez voir votre paiement test
3. Statut : **Réussi** (succeeded)
4. Montant : celui de votre panier

---

## 🔍 Étape 7 : Dépannage

### Problème : "Invalid API Key"

**Causes possibles :**
1. Clés non configurées dans appsettings.json
2. Mauvaise clé copiée
3. Clé de production au lieu de clé de test
4. Caractères invisibles copiés avec la clé

**Solutions :**
```bash
# Vérifier que les clés commencent par pk_test_ et sk_test_
# PAS pk_live_ ou sk_live_

# Vérifier qu'il n'y a pas d'espaces avant/après
"PublishableKey": "pk_test_..."  # ✅ Bon
"PublishableKey": " pk_test_..." # ❌ Espace au début
"PublishableKey": "pk_test_... " # ❌ Espace à la fin
```

### Problème : L'interface Stripe ne s'affiche pas

**Causes possibles :**
1. Connexion Internet coupée (Stripe CDN)
2. Bloqueur de publicités actif
3. JavaScript désactivé
4. Erreur dans le code JavaScript

**Solutions :**
1. Vérifiez votre connexion Internet
2. Désactivez temporairement le bloqueur de pub
3. Ouvrez la console (F12) et cherchez les erreurs
4. Rechargez la page (Ctrl+F5)

### Problème : Paiement échoue systématiquement

**Vérifications :**
```javascript
// Dans la console navigateur (F12 > Console), vérifiez :
console.log('Stripe loaded:', typeof Stripe !== 'undefined');
// Devrait afficher : true

// Vérifier la clé publique :
console.log('Public key:', 'pk_test_...');
// Devrait commencer par pk_test_
```

### Problème : "Network error" lors du paiement

**Causes :**
- Problème de connexion
- CORS non configuré
- Antivirus bloquant

**Solutions :**
1. Vérifiez la connexion Internet
2. Essayez avec un autre navigateur
3. Désactivez temporairement l'antivirus

---

## 📚 Ressources Utiles

### Documentation Stripe
- **Guide de démarrage** : https://stripe.com/docs/development/quickstart
- **API Reference** : https://stripe.com/docs/api
- **Cartes de test** : https://stripe.com/docs/testing

### Dashboard Stripe
- **Paiements tests** : https://dashboard.stripe.com/test/payments
- **Logs** : https://dashboard.stripe.com/test/logs
- **Webhooks** : https://dashboard.stripe.com/test/webhooks

### Support
- **Centre d'aide** : https://support.stripe.com/
- **Forum communautaire** : https://stackoverflow.com/questions/tagged/stripe-payments

---

## 🎯 Checklist Finale

Avant de considérer que Stripe est bien configuré :

- [ ] Compte Stripe créé et vérifié
- [ ] Mode test activé (toggle bleu/orange)
- [ ] Clé publique copiée (commence par `pk_test_`)
- [ ] Clé secrète copiée (commence par `sk_test_`)
- [ ] Clés ajoutées dans `appsettings.json`
- [ ] Application redémarrée après modification
- [ ] Page de paiement affiche le formulaire Stripe
- [ ] Paiement test réussi avec carte 4242...
- [ ] Paiement visible dans dashboard Stripe
- [ ] Facture générée après paiement
- [ ] Réservation marquée comme "Payée"
- [ ] Panier vidé après paiement réussi

---

## 🎉 Configuration terminée !

Si tous les points de la checklist sont cochés, **Stripe est correctement configuré** !

Vous pouvez maintenant :
- ✅ Tester tous les parcours de paiement
- ✅ Enregistrer votre vidéo de démonstration
- ✅ Préparer votre remise finale

---

## ⚠️ IMPORTANT pour la remise

### NE PAS inclure vos vraies clés Stripe dans le .zip !

Avant de zipper votre projet :

1. Ouvrez `appsettings.json`
2. Remplacez vos vraies clés par des placeholders :

```json
"Stripe": {
  "PublishableKey": "pk_test_VOTRE_CLE_PUBLIQUE",
  "SecretKey": "sk_test_VOTRE_CLE_SECRETE"
}
```

3. Dans le README, ajoutez :
```markdown
## Configuration Stripe

Pour tester l'application, vous devez configurer vos propres clés Stripe :
1. Créez un compte sur stripe.com
2. Activez le mode test
3. Copiez vos clés API
4. Modifiez appsettings.json avec vos clés
```

---

**Date de création** : [Date actuelle]  
**Version** : 1.0  
**Auteur** : Guide de configuration TP1

