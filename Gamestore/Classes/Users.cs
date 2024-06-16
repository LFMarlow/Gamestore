using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gamestore.Classes
{
    public class Users
    {
        public String nom;
        public String prenom;
        public String email;
        public String password;
        public String roleUsers;
        public String postalAdress;

        public Users()
        {

        }

        public Users(string prmMail, string prmPassword)
        {
            email = prmMail;
            password = prmPassword;
        }

        public Users(string prmNom, string prmPrenom, string prmEmail, string prmPassword, string prmRoles, string prmPostal_Adress)
        {
            nom = prmNom;
            prenom = prmPrenom;
            email = prmEmail;
            password = prmPassword;
            roleUsers = prmRoles;
            postalAdress = prmPostal_Adress;
        }

    }
}