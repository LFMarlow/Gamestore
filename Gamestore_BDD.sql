CREATE DATABASE  IF NOT EXISTS `gamestore` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `gamestore`;
-- MySQL dump 10.13  Distrib 8.0.38, for Win64 (x86_64)
--
-- Host: localhost    Database: gamestore
-- ------------------------------------------------------
-- Server version	8.0.39

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `command`
--

DROP TABLE IF EXISTS `command`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `command` (
  `id_commande` int NOT NULL AUTO_INCREMENT,
  `statut_commande` varchar(255) COLLATE utf8mb4_general_ci NOT NULL,
  `titre_jeux` varchar(255) COLLATE utf8mb4_general_ci NOT NULL,
  `genre` varchar(100) COLLATE utf8mb4_general_ci NOT NULL,
  `date_retrait` date NOT NULL,
  `Name_Store` varchar(100) COLLATE utf8mb4_general_ci NOT NULL,
  `id_client` int NOT NULL,
  `id_game` int NOT NULL,
  PRIMARY KEY (`id_commande`),
  KEY `id_client` (`id_client`)
) ENGINE=InnoDB AUTO_INCREMENT=46 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `command`
--

LOCK TABLES `command` WRITE;
/*!40000 ALTER TABLE `command` DISABLE KEYS */;
INSERT INTO `command` VALUES (1,'Livré','Age of Empire IV','Stratégie','2024-07-12','Gamestore Wasquehal',10,1),(2,'Livré','Wayfinder','RPG,Action','2024-07-12','Gamestore Wasquehal',11,3),(6,'Livré','Wayfinder','RPG,Action','2024-06-12','Gamestore Wasquehal',10,1),(7,'Livré','Ghost of Tsushima Directors Cut','Action,Aventure','2024-07-13','Gamestore Wasquehal',10,2),(8,'Livré','Roboquest','FPS,Action,Roguelite','2024-07-14','Gamestore Wasquehal',12,4),(9,'Livré','SAND LAND','Action,RPG','2024-07-20','Gamestore Wasquehal',12,8),(10,'Livré','Soulmask','Survie','2024-07-19','Gamestore Euralille',11,12),(11,'Livré','The Elder Scrolls Online','MMORPG','2024-07-20','Gamestore Euralille',11,7),(12,'Livré','SAND LAND','Action,RPG','2024-07-20','Gamestore Euralille',11,8),(13,'Livré','Total War  Warhammer III','Stratégie','2024-07-24','Gamestore Euralille',11,11),(14,'Livré','Manor Lords','Simulation,Stratégie','2024-07-24','Gamestore Euralille',10,5),(15,'Livré','Final Fantasy XIV','MMORPG','2024-07-24','Gamestore Euralille',10,6),(16,'Livré','Age of Empire IV','Stratégie','2024-07-15','Gamestore Euralille',11,1),(17,'Livré','Tekken 8','Combat','2024-07-24','Gamestore Euralille',10,9),(18,'Livré','Elden Ring','RPG','2024-07-24','Gamestore Euralille',10,10),(19,'Livré','Roboquest','FPS,Action,Roguelite','2024-07-25','Gamestore Euralille',10,4),(20,'Livré','Wayfinder','RPG,Action','2024-07-23','Gamestore Euralille',11,3),(21,'Livré','Elden Ring','RPG','2024-07-24','Gamestore Euralille',11,10),(22,'Livré','Ghost of Tsushima Directors Cut','Action,Aventure','2024-07-24','Gamestore Euralille',11,2),(23,'Livré','Wayfinder','RPG,Action','2024-07-24','Gamestore Euralille',11,3),(24,'Livré','Roboquest','FPS,Action,Roguelite','2024-07-24','Gamestore Euralille',11,4),(25,'Livré','Age of Empire IV','Stratégie','2024-07-24','Gamestore Euralille',11,1),(33,'Livré','SAND LAND','Action,RPG','2024-06-21','Gamestore Wasquehal',11,8),(34,'Livré','Wayfinder','RPG,Action','2024-07-12','Gamestore Wasquehal',12,3),(35,'Livré','Elden Ring','RPG','2024-07-12','Gamestore Euralille',12,10),(36,'Livré','SAND LAND','Action,RPG','2024-07-12','Gamestore Wasquehal',12,8),(37,'Livré','SAND LAND','Action,RPG','2024-07-13','Gamestore Wasquehal',11,8),(38,'Livré','SAND LAND','Action,RPG','2024-07-13','Gamestore Wasquehal',10,8),(39,'Livré','Roboquest','FPS,Action,Roguelite','2024-07-14','Gamestore Wasquehal',11,4),(40,'Livré','Elden Ring','RPG','2024-07-15','Gamestore Euralille',12,10),(41,'Livré','Manor Lords','Simulation,Stratégie','2024-07-15','Gamestore Euralille',12,5),(42,'Livré','Age of Empire IV','Stratégie','2024-07-15','Gamestore Euralille',12,1),(43,'Livré','Roboquest','FPS,Action,Roguelite','2024-07-16','Gamestore Wasquehal',10,4),(44,'Livré','The Elder Scrolls Online','MMORPG','2024-07-20','Gamestore Euralille',10,7),(45,'Livré','The Elder Scrolls Online','MMORPG','2024-07-20','Gamestore Euralille',12,7);
/*!40000 ALTER TABLE `command` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `jeux_video`
--

DROP TABLE IF EXISTS `jeux_video`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `jeux_video` (
  `id` int NOT NULL AUTO_INCREMENT,
  `image` varchar(10000) COLLATE utf8mb4_general_ci NOT NULL,
  `title` varchar(100) COLLATE utf8mb4_general_ci NOT NULL,
  `price` float NOT NULL,
  `pegi` int NOT NULL,
  `quantity` int NOT NULL,
  `genre` varchar(100) COLLATE utf8mb4_general_ci NOT NULL,
  `description` longtext COLLATE utf8mb4_general_ci NOT NULL,
  `discount` int DEFAULT NULL,
  `price_discount` decimal(4,2) DEFAULT NULL,
  `id_game` int NOT NULL,
  PRIMARY KEY (`id`),
  KEY `title` (`title`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `jeux_video`
--

LOCK TABLES `jeux_video` WRITE;
/*!40000 ALTER TABLE `jeux_video` DISABLE KEYS */;
INSERT INTO `jeux_video` VALUES (1,'https://gaming-cdn.com/images/products/2244/orig/age-of-empires-iv-anniversary-edition-anniversary-edition-pc-jeu-steam-cover.jpg?v=1713188921','Age of Empire IV',30,12,12,'Stratégie','Pour célébrer son premier anniversaire, la franchise de jeu de stratégie à succès acclamée par la critique surprend ses millions de joueurs dans le monde avec Age of Empires IV : Édition Anniversaire, qui vous place au cœur d\'encore plus de batailles épiques qui ont façonné l\'Histoire.\n\nGrâce à un mélange de mécaniques à la fois innovantes et familières, étendez votre empire sur de vastes paysages fidèlement reproduits dans une résolution 4K époustouflante. Age of Empires IV : Édition Anniversaire sonne le début d\'une nouvelle ère pour les jeux de stratégie en temps réel avec sa myriade de nouveau contenu gratuit dont de nouvelles civilisations, de nouvelles cartes, plusieurs mises à jour et langues supplémentaires, ainsi que de nouvelles maîtrises, défis, provocations et codes de triches - le tout pour une valeur incroyable qui englobe plus d’histoire que jamais !\n\nDeux nouvelles civilisations, 8 nouvelles cartes - Incarnez les puissants Maliens d’Afrique de l’Ouest, l’une des civilisations commerçantes les plus riches de tous les temps, et enchainez les prouesses économiques en exploitant les minerais et en investissant dans l’or.\n\nOu alors l’un des empires ayant subsisté le plus longtemps de l’Histoire : la civilisation ottomane et son armée redoutable, qui dispose des plus grandes armes de siège à poudre à canon du jeu, les Grands Bombardiers, de puissants canons capables d\'anéantir tous les adversaires qui se dresseront sur votre chemin.\n\nPersonnalisez vos parties avec des mods (beta) - Jouez comme vous le souhaitez et personnalisez vos parties grâce aux outils de contenu de la version beta de Mod Editor. Créez vos propres cartes pour des parties en escarmouche ou multijoueur, concevez des scénarios de mission uniques, assemblez des kits de réglages et inventez de nouveaux modes de jeu pour Age of Empires IV.\n\nRetour vers l\'Histoire - Le passé est un prologue à votre immersion dans le cadre historique de 10 civilisations inédites à travers le monde. Dans votre quête de victoire, vous rencontrerez les empires anglais et chinois, en passant par le sultanat de Delhi. Construisez des villes, gérez vos ressources et dirigez vos troupes au combat, sur terre ou en mer, à travers 8 campagnes uniques et 35 missions qui s\'étalent sur plus de 500 ans d\'histoire, depuis l\'Âge sombre jusqu\'à la Renaissance.\n\nPour les joueurs de tous âges - Age of Empires IV promet une expérience agréable pour les nouveaux joueurs, avec un système de tutoriels qui leur enseignera les bases des jeux de stratégie en temps réel et un mode histoire pour permettre une prise en main facile du jeu aux débutants, tout en offrant de la difficulté aux joueurs vétérans qui découvriront de nouvelles mécaniques de jeu ainsi que des stratégies et des techniques de combat avancées.\n\nDéfiez le monde entier - Connectez-vous en ligne pour affronter, coopérer ou assister à des parties avec jusqu\'à 7 de vos amis dans les modes JcJ et JcE multijoueurs. Retrouvez également des saisons classées et bien plus encore !\n\nPavez votre chemin vers la gloire avec des personnages historiques - Vivez les aventures de Jeanne d\'Arc dans sa quête pour bouter les Anglais hors du royaume de France, ou prenez la tête des puissantes hordes mongoles en incarnant Gengis Khan dans sa conquête de l\'Asie. À vous de choisir, mais attention : chacune de vos décisions déterminera le cours de l\'Histoire.',NULL,NULL,1),(2,'https://gaming-cdn.com/images/products/9093/380x218/ghost-of-tsushima-director-s-cut-pc-game-steam-cover.jpg?v=1718985600','Ghost of Tsushima Directors Cut',60,18,31,'Action,Aventure','À la fin du XIIIe siècle, l\'empire mongol a dévasté des nations entières lors de sa campagne pour la conquête de l\'Est. L\'île de Tsushima est le dernier rempart protégeant l\'archipel japonais d\'une invasion de l\'immense flotte mongole menée par Khotun Khan, un général aussi retors qu\'impitoyable.\n\nAlors que la première vague de l\'assaut mongol embrase l\'île, le courageux samouraï Jin Sakai prend les armes. En tant que dernier survivant de son clan, il est prêt à tout pour protéger son peuple et reconquérir sa terre, quel qu\'en soit le prix. Jin doit s\'affranchir des traditions qui ont fait de lui un guerrier pour emprunter une nouvelle voie, celle du Fantôme, et mener une guerre peu conventionnelle afin de libérer Tsushima.',50,30.00,2),(3,'https://gaming-cdn.com/images/products/13291/616x353/wayfinder-pc-game-steam-cover.jpg?v=1696581973','Wayfinder',25,12,64,'RPG,Action','Le monde d\'Evenor est dévasté. Vous devez maîtriser les pouvoirs d\'un Wayfinder afin de contrôler le Chaos qui a envahi votre monde. Combinez vos forces avec 2 autres amis pour renforcer vos pouvoirs. Façonnez vos aventures grâce aux mutateurs, des éléments de personnalisation permettant de façonner votre exploration de ce monde immersif, afin de récupérer de précieux équipements et butins !\nContrôlez le Chaos\nPersonnalisez votre expérience de jeu et partez à l\'aventure avec l\'aide d\'un mystérieux appareil, la Dague du Gloom. Chaque aventure dispose de mutateurs et défis que VOUS choisissez et contrôlez. Personnalisez les zones que vous explorerez, les boss que vous rencontrerez, les matériaux que vous récupérerez et les Wayfinders que vous deviendrez.\nDevenez un Wayfinder\nContrôlez et maîtrisez le pouvoir d\'un puissant Wayfinder ! Utilisez une large variété de Capacités uniques selon votre style de jeu préféré : de la magie arcanique aux technologies en passant par des arts martiaux mortels. Parcourez un monde immersif avec d\'autres Wayfinders et façonnez vos aventures selon vos envies. Choisissez ce que vous explorerez et les ennemis que vous affronterez afin de personnaliser la façon dont vous jouerez avec votre Wayfinder.\nExplorez et collectionnez\nVous découvrirez de nouveaux lieux, monstres et équipements au cours de chaque chasse et expédition dans le Gloom. Collectionnez tous les ensembles d\'armure, chaque arme unique ainsi que des centaines de décorations d\'appartement tout en sauvant les différentes régions d\'un monde dévasté.\nPlus forts ensemble\nLes Wayfinders sont plus forts ensemble ! Terminez des missions de l\'histoire ou la dernière chasse de boss avec des amis. Wayfinder est jouable en coopération à 3 joueurs et vous permet d\'entrer et sortir des aventures de vos amis pour progresser ensemble dans votre lutte contre le Gloom.',NULL,NULL,3),(4,'https://gaming-cdn.com/images/products/7990/616x353/roboquest-pc-game-steam-cover.jpg?v=1706698443','Roboquest',29.99,7,54,'FPS,Action,Roguelite','Roboquest est un action-FPS Roguelite jouable en solo ou en coopération à deux joueurs.\n\nFoncez et combattez des hordes d\'ennemis dans des environnements générés aléatoirement, améliorez votre Gardien au fur et à mesure que vous progressez et détruisez les puissants boss qui se dressent sur votre route.\n\nAméliorez votre campement pour débloquer de nouvelles options de spécialisation pour votre Gardien et tenter d\'aller de plus en plus loin à chaque partie.\n\nEn 2700, l’humanité survit tant bien que mal dans des villages dispersés dans le désert. L’avenir paraît… incertain.\nJusqu’à ce qu’une jeune ingénieure nommée Max fasse une découverte qui peut changer la donne. En réactivant un ancien Gardien abandonné dans le désert, elle va donner un nouvel espoir à l’humanité.\nEnsemble, ils vont explorer les canyons mystérieux qui les entourent, en quête de réponses et d\'une solution.Mais la situation se complique quand ils découvrent que les canyons débordent de robots déterminés à les arrêter. Mais qui les contrôle ? Et que protègent-ils ?',NULL,NULL,4),(5,'https://gaming-cdn.com/images/products/8253/616x353/manor-lords-pc-jeu-steam-europe-cover.jpg?v=1717577219','Manor Lords',49.99,16,85,'Simulation,Stratégie','Manor Lords est un jeu de stratégie qui vous permet d’incarner un seigneur médiéval. Développez votre village de départ pour en faire une cité animée, gérez les ressources et les chaînes de production et agrandissez votre territoire par la conquête.\n\nPuisant son inspiration dans l’art et l’architecture de la Franconie de la fin du XIVe siècle, Manor Lords privilégie l’exactitude historique dans la mesure du possible, et l’utilise comme base pour les mécanismes de jeu et les graphismes. On évite les clichés courants sur le Moyen- ge au profit de l’exactitude historique, afin de rendre le monde plus authentique, plus vivant et plus crédible.',50,24.99,5),(6,'https://gaming-cdn.com/images/products/216/616x353/final-fantasy-xiv-online-starter-edition-starter-edition-pc-jeu-europe-cover.jpg?v=1719952982','Final Fantasy XIV',9.99,16,47,'MMORPG','Si vous souhaitez découvrir FINAL FANTASY XIV Online, cette édition est faite pour vous ! Elle comprend trois jeux salués par la critique : le jeu de base FINAL FANTASY XIV: A Realm Reborn et les deux extensions FINAL FANTASY XIV: Heavensward et FINAL FANTASY XIV: Stormblood. Inclut également une période gratuite de 30 jours d’abonnement*\n\nRejoignez 30 millions d\'aventuriers en ligne et vivez une expérience FINAL FANTASY épique ! Tous les éléments qui font le succès de la franchise sont réunis : un scénario inoubliable, des combats grisants et une pléthore d’environnements captivants à explorer.\n\nJouez en solo ou avec vos amis ! Partez à la conquête des donjons de l’épopée en solo accompagné de PNJ alliés ou bien avec vos amis !\n\n\"Affrontez votre destin dans FINAL FANTASY XIV: Heavensward :\n• Visitez la cité d’Ishgard, enlisée dans une guerre sans fin contre les dragons.\n• Découvrez trois jobs supplémentaires : utilisez la magie curative pour soigner vos alliés en tant qu’Astromancien. Faites parler votre arsenal de pointe en tant que Machiniste. Fendez l’air de votre épée ténébreuse en devenant un Chevalier noir.\n\nRavivez l\'espoir dans FINAL FANTASY XIV: Stormblood :\n• Embarquez pour l’Orient et défiez l’empire de Garlemald.\n• Découvrez deux jobs supplémentaires : maniez la magie et le fleuret comme personne en tant que Mage rouge. Éprouvez le tranchant de votre katana en devenant un Samouraï.\" ',NULL,NULL,6),(7,'https://gaming-cdn.com/images/products/2124/616x353/the-elder-scrolls-online-pc-mac-jeu-cover.jpg?v=1705596590','The Elder Scrolls Online',19.99,18,59,'MMORPG','Vivez une histoire en expansion constante dans tout Tamriel avec The Elder Scrolls Online, un RPG en ligne primé. Explorez un monde riche et vivant entre amis ou partez pour une aventure solo.\nJOUEZ COMME VOUS VOULEZ\nCombat, artisanat, vol, exploration… combinez différents types d’équipements et de compétences pour créer votre propre style de jeu. Le choix vous appartient, dans un monde Elder Scrolls persistant et en expansion constante.\nRACONTEZ VOTRE HISTOIRE\nDécouvrez les secrets de Tamriel tandis que vous partez en quête de votre âme perdue et sauvez le monde face à Oblivion. Vivez toutes les histoires dans toutes les régions du monde, dans l’ordre que vous voudrez… seul ou en groupe.\nUN JDR MULTIJOUEUR\nAccomplissez les quêtes entre amis, rejoignez d’autres aventuriers pour explorer de dangereux donjons remplis de monstres, ou participez à des batailles JcJ épiques entre plusieurs centaines de joueurs.',NULL,NULL,7),(8,'https://gaming-cdn.com/images/products/14327/616x353/sand-land-pc-jeu-steam-europe-cover.jpg?v=1714119916','SAND LAND',69.99,12,77,'Action,RPG','Explorez un monde désertique où les humains, comme les démons, subissent les conséquences d\'une pénurie d\'eau extrême : SAND LAND. Faites la connaissance du prince des démons, Beelzebub, de Thief, son chaperon, et de l\'intrépide shérif Rao, et suivez cette équipe bigarrée pour une extraordinaire aventure à la recherche de la source légendaire dissimulée dans le désert, et bien d\'autres péripéties encore. Parfois, la fin n\'est que le début puisque derrière ces terres arides se profile un mystérieux nouveau royaume à explorer : Forest Land.\n\nCet action-RPG se déroule dans un monde chaleureux et nostalgique créé par Akira Toriyama, où vous deviendrez le personnage principal, Beelzebub. Apprenez à maîtriser ses pouvoirs et menez votre petite troupe de héros bigarrés à la découverte du monde légendaire de SAND LAND et des terres inédites de Forest Land. Mais attention aux nombreux dangers qui peuplent ces contrées ; entre les bandits, la faune sauvage et les implacables généraux de l\'armée, parvenir à atteindre la source et à sauver le monde ne sera pas tâche facile !\n\nUtilisez votre doigté et votre imagination pour assembler des chars et d\'autres véhicules qui vous aideront à naviguer dans ces immenses terres pleines de ressources en utilisant un vaste éventail de combinaisons de pièces. Bâtissez votre base d\'opérations dans une ville florissante avec l\'aide des individus que vous rencontrerez en chemin dans ce désert.',15,59.49,8),(9,'https://gaming-cdn.com/images/products/9579/616x353/tekken-8-pc-jeu-steam-cover.jpg?v=1706550061','Tekken 8',70,16,71,'Combat','Des personnages intégralement remodelés et relookés. Des modèles extrêmement détaillés totalement recréés et des graphismes ultra réalistes repoussent les limites des hardwares et des technologies nouvelle génération en apportant une nouvelle intensité et une nouvelle atmosphère aux combats emblématiques de TEKKEN. Des environnements éclatants et des stages destructibles sont réunis pour créer un sentiment d\'immersion sans pareil et proposer l\'expérience de jeu ultime.\n• NOUVEAU JEU, NOUVELLES RIVALITES\n\n\"Fist Meets Fate\" dans TEKKEN 8. Détenant le record de l\'histoire la plus longue pour une franchise de jeux vidéo, la saga TEKKEN entame un nouveau chapitre avec TEKKEN 8, qui poursuit l\'histoire tragique des familles Mishima et Kazama, où la rancune familiale fait place à d\'importants combats, 6 mois après la fin de leur dernier affrontement. L\'histoire de l\'essor et de la détermination de Jin Kazama marque un nouveau chapitre de cette saga intemporelle.\n• DES COMBATS FRENETIQUES ET PALPITANTS QUI METTENT A L\'HONNEUR L\'AGRESSIVITE ET LA DESTRUCTION\n\nLe nouveau système de combat, le mode Heat, renforce l\'agressivité des combats tout en conservant les sensations et les tactiques uniques à la série des TEKKEN. Les stages destructibles rendent les combats plus intenses que jamais. Les coups surpuissants comme les Rage Arts subjugueront les joueurs autant que les spectateurs. Toutes ces puissantes mécaniques rassemblées font de TEKKEN 8 l\'opus le plus palpitant de la série à ce jour !\n• PROFITEZ DE VOTRE VIE DE TEKKEN !\n\nDans le nouveau mode solo Arcade Quest, créez votre propre avatar unique et lancez-vous dans votre nouvelle vie de TEKKEN. Affrontez de nombreux adversaires au sein de différentes salles d\'arcade en progressant à travers l\'histoire, et apprenez à maîtriser les bases et les mécaniques de TEKKEN 8. Débloquez de nombreux éléments de personnalisation différents pour personnage et avatar au fil de votre progression.',NULL,NULL,9),(10,'https://gaming-cdn.com/images/products/4824/616x353/elden-ring-pc-jeu-steam-europe-cover.jpg?v=1711550841','Elden Ring',60,16,39,'RPG','UNE NOUVELLE AVENTURE GRANDIOSE VOUS ATTEND\nLevez-vous, Sans-éclat, et puisse la grâce guider vos pas. Brandissez la puissance du Cercle d\'Elden. Devenez Seigneur de l\'Entre-terre.\n• Un vaste monde à explorer\nUn monde immense aux environnements riches et variés, parsemé d\'obscurs et tortueux donjons tous reliés naturellement entre eux, vous attend. Au fil de votre exploration, goûtez à l\'inconnu, bravez les menaces permanentes et accomplissez votre destinée.\n• Créez votre propre personnage\nEn plus de l\'apparence de votre personnage, vous êtes libre de personnaliser votre arsenal d\'armes, d\'armures et de sorts. Construisez un personnage qui correspond à votre style de jeu, et devenez un guerrier surpuissant, ou encore un maître de la magie.\n• Un scénario épique né d\'un mythe\nPlusieurs histoires se mêlent dans un récit narré par fragments. Vous rencontrerez des personnages complexes au fil de votre découverte épique de l\'Entre-terre.\n• Connectez-vous librement à d\'autres joueurs grâce au mode en ligne\nEn plus du mode multijoueur, vous permettant de vous connecter directement à d\'autres joueurs et de voyager ensemble, le jeu propose un système en ligne asynchrone unique vous permettant de ressentir la présence des autres Sans-éclat.',47,31.79,10),(11,'https://gaming-cdn.com/images/products/8144/616x353/total-war-warhammer-iii-pc-mac-jeu-steam-europe-cover.jpg?v=1681394922','Total War  Warhammer III',60,16,30,'Stratégie','Total War : WARHAMMER III pour PC est un jeu vidéo de stratégie au tour par tour et en temps réel, basé sur le jeu de table du même nom, et le troisième de la série de jeux vidéo. Les joueurs déplacent à tour de rôle leurs pièces/personnages sur la carte et apprennent à gérer au mieux leurs colonies afin d\'atteindre tous leurs objectifs, de vaincre leurs ennemis et de s\'entendre avec ceux qu\'ils ne peuvent pas vaincre immédiatement !\n\nA propos du jeu\nLes possesseurs des deux précédents jeux de la série pourront accéder à toutes les cartes qu\'ils ont déjà débloquées dans ces arènes, ainsi qu\'à toutes les nouvelles zones ajoutées pour ce jeu. Le jeu se déroule dans deux lieux : les royaumes du Chaos et les terres de l\'Est, pour une touche de mystère et d\'exotisme oriental !\n\nLa carte du jeu est absolument gigantesque, vous avez donc beaucoup à explorer, même si vous vous frayez un chemin vers votre cible en combattant des hordes presque sans fin d\'ennemis implacables et assoiffés de sang ! La carte implique aussi bien la diplomatie que la guerre avec les ennemis IA (intelligence artificielle) - et vous êtes le seul à pouvoir décider si vos forces sont assez fortes pour les vaincre, ou si vous devez continuer à être poli un peu plus longtemps !\n\nLes manières de jouer\nEn raison de l\'origine réelle du jeu, les batailles se déroulent en temps réel, ce qui vous permet de savourer chaque détail une fois que vous avez eu l\'œil pour observer l\'action - votre première bataille passera dans un brouillard de confusion, de sang et d\'armes !\n\nIl y a plusieurs façons de jouer, y compris un mode de combat personnalisé où vous pouvez planifier l\'action selon vos propres désirs au lieu de jouer les batailles préétablies du jeu.\n\nIl existe également un mode de combat multijoueur dans lequel vous et votre équipe devez capturer tous les points de contrôle afin d\'invoquer le boss pour un affrontement final satisfaisant.\n\nLes joueurs bénéficient de fortifications, de buffs pratiques et de troupes de renfort qui peuvent être facilement convoquées, pour autant que vous ayez gagné suffisamment de surplus de fournitures pour bénéficier de cette aide. Si vous n\'êtes pas un bon joueur, vous passerez un bon bout de temps à gérer vos ressources !\n\nQui est présent ?\nDans le jeu, il y a deux populations humaines : le Kislev (qui est basé sur la Russie médiévale et qui comprend une cavalerie avec des ours (Vous avez dit cliché ?!) ainsi qu\'une magie appelée Lore of Ice) et le Grand Cathay (qui est issu de l\'ancienne Chine impériale). Ils sont rejoints par quatre factions basées sur les dieux du chaos qui sont les suivantes :\n\nKhorne : le dieu du sang, le seigneur de la guerre, meurtrier et colérique. Ses rugissements de mécontentement résonnent à travers les royaumes. En fait, à travers tout le temps et l\'espace.\nTzeentch : également connu sous le nom de Changeur de voies, ce dieu sombre est en charge de la mutation, de l\'évolution, du désir ardent que les choses soient différentes ainsi que de la machination et de la sorcellerie.\nNurgle : le Dieu du Chaos qui se délecte des humains dont la peur de la mort est presque écrasante...\nSlaanesh : le Seigneur du plaisir et un Dieu sombre dédié à tout ce qui est décadent et hédoniste - et pas d\'une manière amusante.',30,42.00,11),(12,'https://gaming-cdn.com/images/products/16818/616x353/soulmask-pc-jeu-steam-cover.jpg?v=1718879049','Soulmask',29.99,7,20,'Survie','« Soulmask » est un jeu bac à sable de survie où vous êtes le « dernier élu », béni par un masque mystérieux. Survivez dans un monde primitif rempli de croyances ancestrales. Partez de rien, explorez, bâtissez, formez une tribu, et découvrez les secrets des civilisations antiques.\n\n\nFusionnez âme et masque…\n·      Vous enviez les talents et compétences exceptionnels des PNJ ? N\'hésitez pas à leur « emprunter » ! Dans le jeu, le masque ne vous aide pas seulement à synchroniser les consciences et recruter des barbares pour renforcer votre tribu, mais grâce à la compatibilité des consciences, il vous permet aussi de prendre temporairement le contrôle de leur corps, vous transformant en n\'importe qui que vous voyez !\n·      Au fur et à mesure de votre progression dans le jeu, découvrez davantage de masques hantés par l\'esprit des héros, et revivez leur apparence originale et leurs exploits légendaires : Exploitez le don d\'immortalité, marchez parmi vos ennemis cachés dans l\'ombre, tirez des flèches à tête chercheuse avec une précision mortelle et bien plus encore... Grâce à ces dix masques uniques qui n’attendent que d\'être découverts !\n\n\nCréez une tribu au caractère unique et recrutez des membres dynamiques\n·      Même en tant que joueur solitaire, vous ne vous sentirez jamais seul ou impuissant : dans le jeu, presque tous les barbares PNJ, chacun avec des personnalités et des talents différents, peuvent être recrutés sous votre bannière. Parmi eux, il y a des guerriers assoiffés de sang, des chasseurs habiles, des artisans doués et même... des paresseux qui préfèrent rester allongés toute la journée !\n·      Un puissant soutien de l’IA et des commandes vous permettent de configurer et de gérer librement le travail des membres de votre tribu : surveillance et récolte des fermes, production à la chaîne, patrouilles, réparations automatiques... Tout est programmé et sous votre contrôle, sans que vous ayez besoin d\'intervenir personnellement.\n·      Après une journée chargée, pourquoi ne pas fumer une cigarette ou boire un petit verre ? Ou peut-être pratiquer le tir à l\'arc puis prendre un bain chaud ? Les membres de votre tribu pleins de caractère trouveront leurs propres loisirs après le travail (bien que nous ne puissions garantir qu\'ils le feront seulement après leurs tâches -_-). Croyez-moi, vous ne pourrez résister à l’envie de danser joyeusement autour du feu de joie qu\'ils auront spontanément allumé !\n\n\nCombat dynamique basé sur une physique réaliste\n·      Le jeu offre actuellement 8 styles différents d\'armes et 75 compétences de combat ainsi que des modules d\'actions spéciales, chacun reproduisant fidèlement les caractéristiques physiques réelles.\n·      Adaptez votre style de combat pour vaincre vos adversaires : écrasez le crâne d\'un félin avec un marteau ou attaquez les créatures volantes avec des lances et des flèches. Même si vous êtes un amateur de combat à mains nues, vous trouverez votre style unique parmi les maîtres de la boxe et les assassins !\n\n\nUn vaste monde rempli de multiples civilisations anciennes\n·      Parcourez un vaste monde incluant des forêts tropicales, des rivières, des déserts, des plateaux, des marécages, des volcans et des montagnes enneigées, vivez, survivez et relevez les défis posés par les climats et les créatures uniques de chaque région.\n·      Explorez les anciennes ruines et les temples majestueux disséminés à travers le monde, qui semblent liés aux masques, à la civilisation et au passé secret de ce monde.\n·      Avec plus de 500 heures de contenu original, que vous choisissiez de cultiver et d\'élever des animaux pour devenir la tribu la plus riche, ou d\'explorer les profondeurs du monde et des ruines en quête de ce lieu éternel, le choix vous appartient.\n\n\nDes guerres tribales authentiques et intenses\nPrenez part dans une lutte pour votre survie — que ce soit contre des tribus primitives bien ancrées dans ces terres, des forces d\'exilés en quête de pillages, ou même d\'autres joueurs en mode JcJ (optionnel). Avec le soutien de la physique réaliste, construisez des camps sur des falaises, utilisez des armes et des pièges primitifs pour vaincre vos adversaires, et vivez avec vos tribus l\'expérience la plus authentique des affrontements tribaux.\nVotre jeu, votre expérience\nLe jeu prend actuellement en charge le mode solo hors ligne ou le multijoueur en ligne avec jusqu’à 50 joueurs sur les serveurs officiels. Vous pouvez également créer votre propre LAN ou serveur privé tout en invitant d’autres personnes à rejoindre. Sur des serveurs privés, non officiels, personnalisez de nombreux paramètres pour adapter l’expérience de jeu selon vos préférences, afin que vous et vos amis puissiez explorer le monde grandiose de Soulmask à votre rythme.',NULL,NULL,12);
/*!40000 ALTER TABLE `jeux_video` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `magasins`
--

DROP TABLE IF EXISTS `magasins`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `magasins` (
  `id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) COLLATE utf8mb4_general_ci NOT NULL,
  `Address` varchar(100) COLLATE utf8mb4_general_ci NOT NULL,
  `Ville` varchar(100) COLLATE utf8mb4_general_ci NOT NULL,
  `PostalCode` int NOT NULL,
  `Latitude` double(8,6) NOT NULL,
  `Longitude` double(7,6) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `magasins`
--

LOCK TABLES `magasins` WRITE;
/*!40000 ALTER TABLE `magasins` DISABLE KEYS */;
INSERT INTO `magasins` VALUES (1,'Gamestore Wasquehal','Avenue du Grand Cottignies','Wasquehal',59290,50.682732,3.126962),(2,'Gamestore Euralille','Centre commercial EURALILLE','Lille',59777,50.633585,3.062979),(3,'Gamestore Villeneuve D\'Ascq','Centre commercial VILLENEUVE D\'ASCQ','Villeneuve D\'Ascq',59650,50.617935,3.129649);
/*!40000 ALTER TABLE `magasins` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `panier`
--

DROP TABLE IF EXISTS `panier`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `panier` (
  `id` int NOT NULL AUTO_INCREMENT,
  `titre_jeux` varchar(255) COLLATE utf8mb4_general_ci NOT NULL,
  `id_client` int NOT NULL,
  `id_game` int NOT NULL,
  PRIMARY KEY (`id`),
  KEY `id_client` (`id_client`),
  KEY `id_game` (`id_game`)
) ENGINE=InnoDB AUTO_INCREMENT=39 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `panier`
--

LOCK TABLES `panier` WRITE;
/*!40000 ALTER TABLE `panier` DISABLE KEYS */;
/*!40000 ALTER TABLE `panier` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `pegi`
--

DROP TABLE IF EXISTS `pegi`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `pegi` (
  `id` int NOT NULL AUTO_INCREMENT,
  `pegi` varchar(1000) COLLATE utf8mb4_general_ci NOT NULL,
  `description` varchar(10) COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pegi`
--

LOCK TABLES `pegi` WRITE;
/*!40000 ALTER TABLE `pegi` DISABLE KEYS */;
INSERT INTO `pegi` VALUES (1,'https://upload.wikimedia.org/wikipedia/commons/2/2c/PEGI_3.svg','pegi_3'),(2,'https://upload.wikimedia.org/wikipedia/commons/2/29/PEGI_7.svg','pegi_7'),(3,'https://upload.wikimedia.org/wikipedia/commons/4/44/PEGI_12.svg','pegi_12'),(4,'https://upload.wikimedia.org/wikipedia/commons/8/8a/PEGI_16.svg','pegi_16'),(5,'https://upload.wikimedia.org/wikipedia/commons/7/75/PEGI_18.svg','pegi_18');
/*!40000 ALTER TABLE `pegi` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users` (
  `id_client` int NOT NULL AUTO_INCREMENT,
  `Nom` varchar(50) COLLATE utf8mb4_general_ci NOT NULL,
  `Prenom` varchar(50) COLLATE utf8mb4_general_ci NOT NULL,
  `email` varchar(255) COLLATE utf8mb4_general_ci NOT NULL,
  `password` varchar(255) COLLATE utf8mb4_general_ci NOT NULL,
  `postal_adress` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `role_users` varchar(255) COLLATE utf8mb4_general_ci NOT NULL,
  `token_users` varchar(255) COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`id_client`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (8,'Lesage','Thomas','thomas59.lesage@gmail.com','$MYHASH$V1$10000$K7OyoJb3ertnWt7PbziEhngEDgqDQJJwIHmwFkxmcdp6Ijmn','670 chemin de la marotte','Administrateur','aQakhw+78rqGXg=='),(9,'DevTest','Marlow','marlowtestdev@gmail.com','$MYHASH$V1$10000$LwOWasU+zAfImzapP/E45TXXiDcilowjvgChhIHr9gRvVw20','42 rue de oui','Employé','myFA9To8FgfK3A=='),(10,'DeRiv','Geralt','Witcher@gmail.com','$MYHASH$V1$10000$yiWnb8kooN2k1sLucPrm/KzFQIO1+uYdhOgjFTCwBXEIpR9w','5 rue zplit','Utilisateur','3DIdNQgJ3NsV2w=='),(11,'Tory','Manda','Mandatory@gmail.com','$MYHASH$V1$10000$t8MaHQ2X2os3/EIFB2OhXhQfQZSOAMAjEbleTTZXp6xdRuvb','Ascent','Utilisateur','YDFfZ7idKKtcsA=='),(12,'Gentle','Mates','GentleMates@gmail.com','$MYHASH$V1$10000$8PIW0RFfRhyjkWAPvDf/OD52nAWxKNQjbSHwn24apM/LKd5k','oui','Utilisateur','XIvN9kLV1xdxpQ==');
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-07-28 19:34:06
