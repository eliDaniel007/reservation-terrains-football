# 📦 Checklist de Remise Finale - TP1

## ⏰ Date limite : 27 octobre 2025, 23h00

---

## 📋 Éléments à remettre sur Moodle

### 1. 📁 Code Source (.zip)

**Fichier**: `TP1_VotreNom_Matricule.zip`

#### Contenu à inclure :
- [ ] Tout le code source du projet
- [ ] Fichier `README.md`
- [ ] Fichier `GUIDE_TESTS.md`
- [ ] Fichier `.gitignore`
- [ ] Fichier `appsettings.json` (SANS vos vraies clés Stripe)

#### Contenu à EXCLURE :
- [ ] Dossier `/bin`
- [ ] Dossier `/obj`
- [ ] Dossier `/Migrations` (optionnel)
- [ ] Fichier `appsettings.Development.json` avec clés sensibles
- [ ] Base de données (fichiers .mdf, .ldf)

#### Comment créer le .zip :
```bash
# Depuis le répertoire parent du projet
zip -r TP1_VotreNom_Matricule.zip TP1/ -x "*/bin/*" "*/obj/*" "*.mdf" "*.ldf"
```

Ou sur Windows :
1. Sélectionner le dossier TP1
2. Clic droit > "Envoyer vers" > "Dossier compressé"
3. Renommer en `TP1_VotreNom_Matricule.zip`

---

### 2. 📄 Rapport (PDF)

**Fichier**: `Rapport_TP1_VotreNom_Matricule.pdf`

#### Sections requises :
- [ ] Page de garde (titre, nom, matricule, date)
- [ ] Table des matières
- [ ] Introduction (contexte, objectifs, technologies)
- [ ] Architecture du système
- [ ] Fonctionnalités implémentées
- [ ] Détails d'implémentation
- [ ] Tests et validation
- [ ] Interface utilisateur (avec captures d'écran)
- [ ] Difficultés rencontrées et solutions
- [ ] Améliorations possibles
- [ ] Conclusion
- [ ] Annexes (structure projet, comptes test)
- [ ] Références

#### Captures d'écran minimales :
- [ ] Page d'accueil avec liste des créneaux
- [ ] Page de détails d'un créneau
- [ ] Page du panier
- [ ] Page de paiement Stripe
- [ ] Page de succès après paiement
- [ ] Page de facture
- [ ] Dashboard admin
- [ ] Page 404

#### Pour compiler le rapport LaTeX :
```bash
pdflatex Rapport_Template.tex
pdflatex Rapport_Template.tex  # 2e fois pour la table des matières
```

---

### 3. 🎥 Vidéo de Démonstration

**Fichier**: `Demo_TP1_VotreNom_Matricule.mp4`

#### Logiciel recommandé : OBS Studio
Télécharger : https://obsproject.com/

#### Durée : 7-10 minutes maximum

#### Contenu de la vidéo :

**1. Introduction (30 sec)**
- [ ] Présentation rapide du projet
- [ ] Affichage de la page d'accueil

**2. Parcours Client (3-4 min)**
- [ ] Inscription d'un nouveau client
- [ ] Navigation et utilisation des filtres
- [ ] Sélection d'un créneau et détails
- [ ] Ajout au panier (plusieurs articles)
- [ ] Modification du panier
- [ ] Processus de paiement Stripe complet
- [ ] Consultation de la facture générée
- [ ] Historique des réservations

**3. Parcours Fournisseur (1 min)**
- [ ] Connexion en tant que fournisseur
- [ ] Affichage de la page des revenus
- [ ] Consultation des statistiques

**4. Parcours Admin (2-3 min)**
- [ ] Connexion en tant qu'admin
- [ ] Dashboard avec KPIs
- [ ] Top 10 clients
- [ ] Taux de remplissage des terrains
- [ ] Gestion des réservations
- [ ] Gestion des terrains
- [ ] Gestion des utilisateurs

**5. Fonctionnalités supplémentaires (1 min)**
- [ ] Modification du profil
- [ ] Page 404 personnalisée
- [ ] Responsive design (réduire la fenêtre)

**6. Conclusion (30 sec)**
- [ ] Résumé des fonctionnalités
- [ ] Remerciements

#### Paramètres d'enregistrement OBS :
- **Résolution** : 1920×1080 (Full HD)
- **FPS** : 30
- **Bitrate** : 2500-5000 kbps
- **Format** : MP4 (H.264)

#### Conseils pour la vidéo :
- ✅ Préparez un script à suivre
- ✅ Fermez les applications inutiles
- ✅ Désactivez les notifications
- ✅ Utilisez un micro de bonne qualité
- ✅ Parlez clairement et à un rythme modéré
- ✅ Montrez les fonctionnalités importantes
- ✅ Ne perdez pas de temps sur les détails mineurs
- ✅ Testez l'enregistrement avant la version finale

---

## ✅ Checklist de Vérification Finale

### Avant de zipper le code :

**Fonctionnalités**
- [ ] Toutes les pages requises sont présentes
- [ ] L'authentification fonctionne pour les 3 rôles
- [ ] Les filtres et la recherche fonctionnent
- [ ] Le panier gère correctement les quantités
- [ ] Le paiement Stripe fonctionne en mode sandbox
- [ ] Les factures se génèrent correctement
- [ ] Le dashboard admin affiche les bonnes données
- [ ] La page 404 personnalisée s'affiche

**Code**
- [ ] Le projet compile sans erreur : `dotnet build`
- [ ] Aucune erreur de linter
- [ ] Les fichiers sensibles sont exclus
- [ ] Le README est à jour et complet
- [ ] Les commentaires importants sont présents

**Base de données**
- [ ] Les migrations sont créées
- [ ] Le seeding fonctionne correctement
- [ ] Les 3 terrains avec créneaux sont présents
- [ ] Les comptes de test existent

**Sécurité**
- [ ] Les clés Stripe dans appsettings.json sont des placeholders
- [ ] Pas de mots de passe en dur dans le code
- [ ] Les routes sont protégées par rôle
- [ ] La validation des formulaires est en place

### Avant de soumettre le rapport :

**Contenu**
- [ ] Toutes les sections sont complétées
- [ ] Les captures d'écran sont insérées
- [ ] Le diagramme de base de données est présent
- [ ] Les extraits de code sont pertinents
- [ ] La pagination est correcte
- [ ] La table des matières est générée

**Format**
- [ ] Fichier en PDF
- [ ] Nom du fichier correct
- [ ] Taille raisonnable (< 10 MB)
- [ ] Lisibilité des images

### Avant de soumettre la vidéo :

**Qualité**
- [ ] Durée : 7-10 minutes
- [ ] Résolution : minimum 1280×720
- [ ] Audio clair et compréhensible
- [ ] Pas de coupures brusques
- [ ] Le curseur est visible

**Contenu**
- [ ] Tous les parcours sont démontrés
- [ ] Les fonctionnalités principales sont montrées
- [ ] Le paiement Stripe est testé en direct
- [ ] Les dashboards sont présentés
- [ ] Le responsive design est démontré

**Format**
- [ ] Fichier en MP4
- [ ] Nom du fichier correct
- [ ] Taille raisonnable (< 500 MB)
- [ ] Compatible avec les lecteurs standards

---

## 📤 Instructions de Soumission sur Moodle

### Étape 1 : Préparation
1. Créer un dossier "Remise_TP1"
2. Y placer les 3 fichiers :
   - `TP1_VotreNom_Matricule.zip`
   - `Rapport_TP1_VotreNom_Matricule.pdf`
   - `Demo_TP1_VotreNom_Matricule.mp4`

### Étape 2 : Vérification
- [ ] Vérifier que tous les fichiers sont présents
- [ ] Vérifier les noms des fichiers
- [ ] Vérifier que le .zip s'extrait correctement
- [ ] Vérifier que le PDF s'ouvre
- [ ] Vérifier que la vidéo se lit

### Étape 3 : Soumission
1. Se connecter à Moodle
2. Aller dans le cours
3. Trouver la zone de dépôt du TP1
4. Téléverser les 3 fichiers
5. Vérifier que le téléversement est complet
6. Confirmer la soumission

### ⚠️ IMPORTANT
- **Date limite** : 27 octobre 2025, 23h00
- **Aucun retard accepté** sans justification médicale
- **Vérifiez** que tous les fichiers sont bien soumis
- **Conservez** une copie de sauvegarde de tout

---

## 📊 Grille d'évaluation (référence)

| Critère | Pondération | Points |
|---------|-------------|---------|
| Fonctionnalités complètes | 11% | /110 |
| Parcours et filtrage | 2.5% | /25 |
| Entity Framework Core | 2.5% | /25 |
| Paiement Stripe | 2% | /20 |
| Interface utilisateur | 3.5% | /35 |
| Système de facturation | 1.5% | /15 |
| Dashboard admin | 1% | /10 |
| **TOTAL** | **24%** | **/240** |

### Points clés pour maximiser la note :

**Fonctionnalités (11 points)**
- ✅ Toutes les pages requises
- ✅ Authentification multi-rôles
- ✅ Gestion complète du panier
- ✅ Profil utilisateur éditable

**Parcours/Filtrage (2.5 points)**
- ✅ Filtres multiples fonctionnels
- ✅ Recherche opérationnelle
- ✅ Combinaison de filtres

**EF Core (2.5 points)**
- ✅ Migrations correctes
- ✅ Relations entre entités
- ✅ Seeding fonctionnel

**Stripe (2 points)**
- ✅ PaymentIntent fonctionnel
- ✅ Gestion des erreurs
- ✅ Confirmation de paiement

**Interface (3.5 points)**
- ✅ Design moderne et cohérent
- ✅ Responsive (mobile/tablette/desktop)
- ✅ UX intuitive
- ✅ Page 404

**Facturation (1.5 points)**
- ✅ Génération automatique
- ✅ Numéro unique
- ✅ Téléchargement/impression

**Admin (1 point)**
- ✅ Dashboard avec KPIs
- ✅ Statistiques pertinentes
- ✅ Gestion complète

---

## 🎯 Dernières Vérifications (30 min avant la remise)

### T-30 minutes :
- [ ] Relire le rapport une dernière fois
- [ ] Visionner la vidéo en entier
- [ ] Extraire et tester le .zip
- [ ] Vérifier l'heure actuelle vs deadline

### T-15 minutes :
- [ ] Téléverser les fichiers sur Moodle
- [ ] Vérifier que le téléversement est complet
- [ ] Télécharger les fichiers depuis Moodle pour confirmer

### T-5 minutes :
- [ ] Confirmation finale de la soumission
- [ ] Screenshot de la confirmation
- [ ] Respirer profondément 😊

---

## 📞 En cas de problème

### Problème technique avec Moodle :
1. **Capture d'écran** de l'erreur
2. **Email immédiat** au professeur avec :
   - Sujet : "TP1 - Problème soumission - [Votre Nom]"
   - Description du problème
   - Capture d'écran
   - Fichiers en pièce jointe si possible

### Dépassement de taille :
- **Vidéo trop grosse** : Compresser avec HandBrake
- **Rapport trop gros** : Réduire la résolution des images
- **Code trop gros** : Vérifier les exclusions (.gitignore)

---

## 🎉 Après la remise

### Archivage :
- [ ] Sauvegarder le projet sur GitHub/GitLab (privé)
- [ ] Copier sur un disque externe
- [ ] Garder une copie cloud

### Portfolio :
- [ ] Ajouter au portfolio si besoin
- [ ] Mettre à jour votre CV avec ce projet
- [ ] Préparer une version "présentable" pour employeurs

---

## ✨ Bon courage !

Vous avez travaillé dur sur ce projet. Assurez-vous de :
- ✅ Soumettre à temps
- ✅ Vérifier chaque élément de cette checklist
- ✅ Garder des copies de sauvegarde

**La réussite est au bout de l'effort !** 🚀

---

*Date de création de ce guide : [Date actuelle]*  
*Dernière mise à jour : [Date actuelle]*

