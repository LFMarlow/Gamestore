using Gamestore.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace Gamestore
{
    public partial class Inscription : System.Web.UI.Page
    {
        DALGamestore objDal = new DALGamestore();
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void BtnInscription_Click(object sender, EventArgs e)
        {
            //Variables d'inscription
            String nom;
            String prenom;
            String passwordBrut;
            String email;
            String postalAdress;
            String roleUsers = "Utilisateur";
            bool inscritOK = false;

            //Création d'un objet de Classe Users pour plusieurs vérification
            Classes.Users objUsers = new Users();

            //Enregistrement des informations rentrées dans les variables
            nom = TxtBoxNom.Text;
            prenom = TxtBoxPrenom.Text;
            passwordBrut = TxtBoxMDP.Text;
            email = TxtBoxMail.Text;
            postalAdress = TxtBoxAP.Text;

            var passwordHash = SecurePassword.Hash(passwordBrut);
            

            if(nom != "" && prenom != "" && email != "" && passwordBrut != "" && postalAdress != "")
            {
                try
                {
                    objUsers = objDal.VerifDoublonMailInscription(email);
                    if(objUsers == null)
                    {
                        inscritOK = objDal.Inscription(nom, prenom, email, passwordHash, roleUsers, postalAdress);
                        if(inscritOK == true)
                        {
                            MessageBox.Show("Inscription Réussi ! Vous pouvez dès à présent vous connecter", "Réussi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Response.Redirect("~/Connexion", false);
                        }
                        else
                        {
                            MessageBox.Show("Inscription impossible, veuillez contacter le support");
                        }
                        
                    }
                    else
                    {
                        MessageBox.Show("Cette adresse email existe déjà", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Connexion echoué, veuillez contacter le support");
                }
            }
            else
            {
                MessageBox.Show("Tout les champs doivent être remplis", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}