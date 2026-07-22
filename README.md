# 🧩 Design Patterns Game — Code Katas C# / .NET

Un jeu d'apprentissage sous forme de dépôt Git. **26 niveaux indépendants**, un
seul objectif : lire un code métier problématique (couplage fort, répétitions,
rigidité) et le **refactoriser** en appliquant le bon patron de conception.

> **Règle d'or** — Le nom du patron à trouver n'apparaît **jamais** dans les
> dossiers, namespaces, classes ou commentaires. À toi de le deviner à partir
> du problème métier. 🤫

---

## 🎮 Comment jouer

1. **Clone** le dépôt.
2. Choisis un niveau, ex. `Level_01_ComplexFactoryBlueprint/`.
3. Ouvre le projet **`.Start`** : c'est le code métier à refactoriser. Il
   *fonctionne* déjà en partie, mais il est mal conçu (rigide, difficile à
   étendre).
4. Lance les tests du niveau (voir ci-dessous). Certains sont **rouges** :
   - des tests de **comportement** (le code doit produire le bon résultat) ;
   - des tests d'**architecture** (le bon patron doit être appliqué — vérifié
     par réflexion, de façon *agnostique aux noms* que tu choisis).
5. Refactorise **`.Start`** jusqu'à ce que **tous les tests passent au vert**.
   🟢 = niveau gagné.
6. Bloqué ? Le projet **`.Solution`** contient une implémentation de référence.

### Lancer les tests d'un niveau

```bash
# L'arbitre : teste TON code (.Start). Rouge tant que non résolu.
dotnet test Level_01_ComplexFactoryBlueprint/Level_01.Tests/Level_01.Tests.csproj

# La référence : teste la solution idéale (.Solution). Toujours vert.
dotnet test Level_01_ComplexFactoryBlueprint/Level_01.Tests.Reference/Level_01.Tests.Reference.csproj
```

> Prérequis : le **SDK .NET 10**.

---

## 🗂️ Structure d'un niveau

Chaque niveau est **autonome** (sa propre solution `.sln`, compilable et
testable seul) :

```
Level_XX_<DomaineMetier>/
├── Level_XX.sln
├── Level_XX.Start/            # Le code métier à refactoriser (TON terrain)
├── Level_XX.Solution/         # La solution de référence (à consulter si besoin)
├── Level_XX.Tests/            # L'arbitre xUnit  -> teste .Start
└── Level_XX.Tests.Reference/  # Les mêmes tests -> teste .Solution (garde-fou du dépôt)
```

Les projets `.Tests` et `.Tests.Reference` **partagent exactement les mêmes
fichiers de test** (liés). L'un les exécute contre ton code, l'autre contre la
référence : c'est la garantie que l'arbitre est équitable et que la solution
idéale passe bien l'épreuve.

### La double validation

| Volet          | Outil                | Vérifie que…                                                        |
|----------------|----------------------|---------------------------------------------------------------------|
| Comportement   | xUnit (classique)    | le code refactorisé produit toujours le bon résultat métier.        |
| Architecture   | `System.Reflection`  | le bon patron est appliqué (interfaces, abstractions, absence de constructeurs obèses…), sans imposer de noms de classes. |

---

## 🤖 Intégration continue (GitHub Actions)

Le workflow [`.github/workflows/ci.yml`](.github/workflows/ci.yml) exécute, à
chaque push, deux jobs par niveau :

- **`reference (Level_XX)`** — teste `.Solution`. **Doit être vert** (santé du dépôt).
- **`arbiter (Level_XX)`** — teste `.Start`. **Rouge tant que le niveau n'est pas
  résolu**, vert dès qu'il l'est.

Sur un clone frais, le workflow est donc partiellement rouge **par conception** :
chaque `arbiter` rouge est un niveau qu'il te reste à résoudre. Ton tableau de
score, c'est la liste des jobs `arbiter` qui passent au vert. 🏆

> Pour ajouter un niveau, il suffit d'ajouter une entrée dans les deux matrices
> (`reference` et `arbiter`) du workflow.

---

## ⚙️ Contraintes techniques

- **C# / .NET 10**, **xUnit**.
- Uniquement des **bibliothèques de classes**, **projets console** et **tests**.
- **Aucun** backend ASP.NET / serveur web : on teste la logique pure et
  l'architecture logicielle.

---

## 🧭 Les 26 niveaux

> Classés par domaine métier. Aucun patron n'est nommé : c'est le jeu.

### Fondations
| # | Dossier | Problème métier à résoudre |
|---|---------|-----------------------------|
| 01 | `Level_01_ComplexFactoryBlueprint`   | Construire une usine d'assemblage avec de multiples convoyeurs. |
| 02 | `Level_02_EnemySpawner`              | Cloner massivement des entités sans surcharger la mémoire. |
| 03 | `Level_03_DatabaseConnectionPool`    | Sécuriser l'accès multithread à une ressource partagée. |
| 04 | `Level_04_EnergyResourceGenerator`   | Instancier dynamiquement des contrats de ressources. |
| 05 | `Level_05_CrossPlatformUI`           | Générer des composants graphiques cohérents. |
| 06 | `Level_06_OrganizationChart`         | Gérer une hiérarchie d'employés et de managers. |
| 07 | `Level_07_EquipmentStatModifiers`    | Empiler des statistiques sur un équipement de jeu. |

### Interactions
| # | Dossier | Problème métier à résoudre |
|---|---------|-----------------------------|
| 08 | `Level_08_LegacyPaymentGateway`      | Connecter une vieille API de paiement à un nouveau système. |
| 09 | `Level_09_HomeTheaterStartup`        | Unifier l'allumage de 6 sous-systèmes. |
| 10 | `Level_10_DeviceRemoteControl`       | Séparer l'interface d'une télécommande de la TV. |
| 11 | `Level_11_AccessControlCache`        | Mettre en cache et sécuriser l'accès à une base de données. |
| 12 | `Level_12_SupportTicketRouting`      | Faire escalader un ticket technique. |
| 13 | `Level_13_UndoRedoSystem`            | Encapsuler des actions pour les annuler. |
| 14 | `Level_14_ConcertTourAlerts`         | Notifier des fans d'une nouvelle date de tournée. |

### Logique métier
| # | Dossier | Problème métier à résoudre |
|---|---------|-----------------------------|
| 15 | `Level_15_LogisticsRouting`          | Interchanger des algorithmes d'itinéraire. |
| 16 | `Level_16_CharacterCombatPhases`     | Gérer les états de combat d'un personnage. |
| 17 | `Level_17_CampfireSaveState`         | Sauvegarder et restaurer l'état d'un joueur. |
| 18 | `Level_18_DataMiningWorkflow`        | Figer un algorithme tout en laissant des étapes redéfinissables. |
| 19 | `Level_19_TaxCalculationExport`      | Ajouter une opération d'export sur une hiérarchie existante. |

### Architecture moderne & .NET
| # | Dossier | Problème métier à résoudre |
|---|---------|-----------------------------|
| 20 | `Level_20_AppSettingManagement`      | Typer fortement la configuration. |
| 21 | `Level_21_DependencyInjectionCleanup`| Modulariser un fichier d'enregistrement de services obèse. |
| 22 | `Level_22_HighTrafficOrders`         | Séparer les modèles de lecture et d'écriture. |
| 23 | `Level_23_EndpointRefactoring`       | Isoler des requêtes métier complexes de bout en bout. |
| 24 | `Level_24_ECommerceCheckoutFlow`     | Coordonner une transaction distribuée. |
| 25 | `Level_25_ReliableMessagePublishing` | Garantir la publication d'un événement métier. |
| 26 | `Level_26_ExternalApiResilience`     | Protéger l'application contre les pannes d'une API tierce. |

---

## 📌 État d'avancement du dépôt

- ✅ **Levels 01 → 26** — tous implémentés (Start / Solution / Tests / Tests.Reference).
- Chaque niveau est validé par la CI : le job `reference` est vert (la solution de
  référence passe l'arbitre), le job `arbiter` est rouge tant que tu n'as pas résolu
  le `.Start`.

Bon refactoring ! 🛠️
