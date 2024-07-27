using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gamestore.Classes
{
    public class Users
    {
        public String nom { get; set; }
        public String prenom { get; set; }
        public String email { get; set; }
        public String password { get; set; }
        public String roleUsers { get; set; }
        public String postalAdress { get; set; }
        public String tokenUsers { get; set; }

        public Users()
        {

        }
    }
}