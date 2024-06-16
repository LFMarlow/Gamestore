using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Gamestore.Classes
{
    public class DALGamestore
    {
        private string chaineTest = ConfigurationManager.ConnectionStrings["ChaineBdd"].ConnectionString;
        
        MySqlConnection connexion;
        MySqlCommand command;
        MySqlDataReader reader;

        public DALGamestore()
        {
            connexion = new MySqlConnection(chaineTest);
        }

        //Method de connexion à la BDD
        private bool Connecter()
        {
            bool isConnected = false;
            try
            {
                connexion.Open();
                if (connexion.State == System.Data.ConnectionState.Open)
                {
                    isConnected = true;
                }
            }
            catch (Exception ex)
            {
                isConnected = false;
            }
            return isConnected;
        }

        //Deconnexion de la BDD
        private void Deconnecter()
        {
            if ((reader != null) && (reader.IsClosed != true))
            {
                reader.Close();
            }
            connexion.Close();
        }

        //Vérification si adresse mail déjà renseigné
        public Classes.Users VerifDoublonMailInscription(string prmMail)
        {
            string verifDoublonMail = "SELECT (email) FROM users WHERE email =" + "'" + prmMail + "'";

            Classes.Users objUsers = null;
            bool isConnected = false;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {

                    command = new MySqlCommand(verifDoublonMail, connexion);
                    reader = command.ExecuteReader();
                    reader.Read();
                    objUsers = new Classes.Users();

                    objUsers.email = Convert.ToString(reader["email"]);
                };
            }
            catch (Exception ex)
            {
                objUsers = null;
            }

            Deconnecter();
            return objUsers;
        }

        //Inscription de l'utilisateur dans la BDD
        public bool Inscription(string prmNom, string prmPrenom, string prmMail, string prmPassword, string prmRolesUsers, string prmPostalAdress)
        {
            bool estInscrit = false;
            string usersInscription =
            "INSERT INTO users (Nom, Prenom, email, password, postal_adress, role_users) VALUES ('" + prmNom + "'," + "'" + prmPrenom + "'," + "'" + prmMail + "'," + "'" + prmPassword + "'," + "'" + prmPostalAdress + "'," + "'" + prmRolesUsers + "'" + ")";

            bool isConnected = false;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    command = new MySqlCommand(usersInscription, connexion);
                    reader = command.ExecuteReader();
                    estInscrit = true;
                }
            }
            catch (InvalidOperationException)
            {
                isConnected = false;
            }
            Deconnecter();
            return estInscrit;
        }

        //Authentification de l'utilisateur sur l'application
        public Classes.Users AuthentificationDAL(String prmEmail, String prmPassword)
        {

            String requete = "SELECT * FROM users WHERE email = '" + prmEmail + "' AND password= '" + prmPassword + "'";
            bool isConnected = false;
            Classes.Users objUsers = null;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    command = new MySqlCommand(requete, connexion);
                    reader = command.ExecuteReader();
                    objUsers = new Classes.Users();

                    if (reader.Read())
                    {
                        objUsers.nom = Convert.ToString(reader["Nom"]);
                        objUsers.prenom = Convert.ToString(reader["Prenom"]);
                        objUsers.email = Convert.ToString(reader["email"]);
                        objUsers.password = Convert.ToString(reader["password"]);
                        objUsers.roleUsers = Convert.ToString(reader["role_users"]);
                        objUsers.postalAdress = Convert.ToString(reader["postal_adress"]);
                    }
                }
            }
            catch (Exception ex)
            {
                //Erreur de récupération
                objUsers = null;
            }

            Deconnecter();
            return objUsers;

        }

        //Récupération du mot de passe de l'utilisateur via son adress mail pour comparer le hachage
        public Classes.Users AuthentificationEmail(String prmEmail)
        {

            String requete = "SELECT password FROM users WHERE email = '" + prmEmail + "'";
            bool isConnected = false;
            Classes.Users objUsers = null;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    command = new MySqlCommand(requete, connexion);
                    reader = command.ExecuteReader();
                    objUsers = new Classes.Users();

                    if (reader.Read())
                    {
                        objUsers.password = Convert.ToString(reader["password"]);
                    }
                }
            }
            catch (Exception ex)
            {
                //Erreur de récupération
                objUsers = null;
            }

            Deconnecter();
            return objUsers;

        }

        //Inscription des employé par l'administrateur
        public bool InscriptionEmploye(string prmNom, string prmPrenom, string prmMail, string prmPassword, string prmRolesUsers)
        {
            bool estInscrit = false;
            string usersInscription =
            "INSERT INTO users (Nom, Prenom, email, password, role_users) VALUES ('" + prmNom + "'," + "'" + prmPrenom + "'," + "'" + prmMail + "'," + "'" + prmPassword + "'," + "'" + prmRolesUsers + "'" + ")";

            bool isConnected = false;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    command = new MySqlCommand(usersInscription, connexion);
                    reader = command.ExecuteReader();
                    estInscrit = true;
                }
            }
            catch (InvalidOperationException)
            {
                isConnected = false;
            }
            Deconnecter();
            return estInscrit;
        }

        //Création de jeux vidéo dans la bdd pour vente
        public bool CreateGame(string prmImage, string prmTitre, string prmPrix, string prmPEGI, string prmQuantity, string prmGenre, string prmDescription)
        {
            bool estInscrit = false;
            string usersInscription =
            "INSERT INTO jeux_video (image, title, price, pegi, quantity, genre, description) VALUES ('" + prmImage + "'," + "'" + prmTitre + "'," + "'" + prmPrix + "'," + "'" + prmPEGI + "'," + "'" + prmQuantity + "'," + "'" + prmGenre  + "'," + "'" + prmDescription.Replace("'", "''") + "'" + ")";

            bool isConnected = false;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    command = new MySqlCommand(usersInscription, connexion);
                    reader = command.ExecuteReader();
                    estInscrit = true;
                }
            }
            catch (InvalidOperationException)
            {
                isConnected = false;
            }
            Deconnecter();
            return estInscrit;
        }

        public Classes.JeuxVideo RécupJeuxVideo(string prmTitle)
        {
            string requete = "SELECT * FROM jeux_video WHERE title= '" + prmTitle + "'";

            bool isConnected = false;
            Classes.JeuxVideo objJeuxVideo= null;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    command = new MySqlCommand(requete, connexion);
                    reader = command.ExecuteReader();
                    objJeuxVideo = new Classes.JeuxVideo();

                    if (reader.Read())
                    {
                        objJeuxVideo.urlImage = Convert.ToString(reader["image"]);
                        objJeuxVideo.prix = Convert.ToSingle(reader["price"]);
                        objJeuxVideo.description = Convert.ToString(reader["description"]);
                        objJeuxVideo.quantite = Convert.ToInt32(reader["quantity"]);
                        objJeuxVideo.pegi = Convert.ToInt32(reader["pegi"]);
                        objJeuxVideo.title = Convert.ToString(reader["title"]);
                        objJeuxVideo.genre = Convert.ToString(reader["genre"]);
                    }
                }
            }
            catch (Exception ex)
            {
                //Erreur de récupération
                objJeuxVideo = null;
            }

            Deconnecter();
            return objJeuxVideo;

        }

        public List<String> RécupTousJeuxVideo()
        {
            string requete = "SELECT DISTINCT title, quantity, image FROM jeux_video";

            bool isConnected = false;
            List<String> listTitleJeuxVideo = new List<string>();

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    command = new MySqlCommand(requete, connexion);
                    reader = command.ExecuteReader();
                    reader.Read();

                    do
                    {
                        listTitleJeuxVideo.Add(Convert.ToString(reader["title"]));

                    }while (reader.Read());
                }
            }
            catch (Exception ex)
            {
                //Erreur de récupération
                listTitleJeuxVideo = null;
            }

            Deconnecter();
            return listTitleJeuxVideo;

        }

    }
}