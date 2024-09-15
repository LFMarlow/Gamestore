# GAMESTORE

## À propos

Réalisation du Projet Gamestore dans le cadre de l'ECF Final pour le Titre Professionnel "Développeur Web et Web mobile".

## Table des matières

- 🪧 [À propos](#à-propos)
- 📦 [Prérequis](#prérequis)
- 🚀 [Installation](#installation)
- 🏗️ [Construit avec](#construit-avec)

## Prérequis

Liste de tous les éléments nécessaires pour le déploiement en local du projet :

- Microsoft Visual Studio (Pour le projet complet)
- Un Système de Gestion de Base de Données (Exemple pour du local : XAMPP avec MySQL)
- Un Système de Gestion de Base de Données Orienté Documents (Exemple : MongoDB)

## Installation

### Étapes pour le déploiement de l'application en local :

- Etape 1 : Télécharger le projet complet
- Etape 2 : Ouvrir la Solution (Fichier Gamestore.sln) avec Microsoft Visual Studio.
- Etape 3 : Installer et mettre à jour les packages NuGet (Clique Droit sur le projet et Gérer les packages NuGet, mettre à jour les packages existant et installé "MySql.Data", "MongoDB.BSon" et "MongoDB.Driver").
- Etape 4 : Ouvrir PHPMyAdmin (EX : Avec XAMPP), importer le fichier inclus dans le Projet "Gamestore_BDD.sql" et créer un utilisateur pour cette BDD.
- Etape 5 : Ouvrir MongoDB, créer une Databases nomme "Gamestore" ainsi que la collection "video_games" et importer le fichier inclus dans le Projet "Gamestore.video_games.json".
- Etape 6 : Revenir sur Visual Studio, ouvrir le fichier "Web.config" et modifier les variables contenues dans "connectionStrings" pour lier vos BDD local au projet.
- Etape 7 : Lancer IIS Express pour tester le projet (Icone flèche verte)

## Construit avec

### Langages & Frameworks

Langages Utilisés pour ce projet :
- HTML
- CSS
- JavaScript
- C#

Frameworks Utilisés pour ce projet :
- ASP.NET
- BootStrap
