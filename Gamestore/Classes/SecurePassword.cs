using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace Gamestore.Classes
{
    public static class SecurePassword
    {
        // <summary>
        /// Taille du "Sel"
        /// </summary>
        private const int SaltSize = 16;

        /// <summary>
        /// Taille de Hachage
        /// </summary>
        private const int HashSize = 20;

        /// <summary>
        /// Création d'un Hash à partir du mot de passe de l'utilisateur
        /// </summary>
        /// <param name="password">Le mot de Passe.</param>
        /// <param name="iterations">Nombre d'itérations.</param>
        /// <returns>The hash.</returns>
        public static string Hash(string password, int iterations)
        {
            //Initialisation du "Sel" avec une valeur PRNG(Pseudo Random Number Generator) Cryptographique
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[SaltSize]);

            //Création du PBKDF2 (Password-Bases Key Derivation Function 2) et obtention de la valeur de Hachage
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            var hash = pbkdf2.GetBytes(HashSize);

            //On Combine le "Sel" et le Hachage
            var hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

            //On convertit le hachage en Base 64
            var base64Hash = Convert.ToBase64String(hashBytes);

            //On Formate le hachage avec des informations supplémentaire
            return string.Format("$MYHASH$V1${0}${1}", iterations, base64Hash);
        }


        /// <summary>
        /// Création du hachage à partir d'un mot de passe avec 10000 itérations
        /// </summary>
        /// <param name="password">Mot de Passe.</param>
        /// <returns>Hachage</returns>
        public static string Hash(string password)
        {
            return Hash(password, 10000);
        }

        /// <summary>
        /// On verifie que le Hachage est supporté
        /// </summary>
        /// <param name="hashString">Le Hachage</param>
        /// <returns>Is supported?</returns>
        public static bool IsHashSupported(string hashString)
        {
            return hashString.Contains("$MYHASH$V1$");
        }

        /// <summary>
        /// On vérifie le mot de passe par rapport au Hachage
        /// </summary>
        /// <param name="password">Mot de passe.</param>
        /// <param name="hashedPassword">Hachage.</param>
        /// <returns>Vérification mot de passe + hachage</returns>
        public static bool Verify(string password, string hashedPassword)
        {
            //Vérification du Hashage
            if (!IsHashSupported(hashedPassword))
            {
                throw new NotSupportedException("The hashtype is not supported");
            }

            //Extraction des itérations et de la Base 64
            var splittedHashString = hashedPassword.Replace("$MYHASH$V1$", "").Split('$');
            var iterations = int.Parse(splittedHashString[0]);
            var base64Hash = splittedHashString[1];

            //Récupération des octets du hachage
            var hashBytes = Convert.FromBase64String(base64Hash);

            //Récupération du "Sel"
            byte[] salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            //Création du Hachage avec le "Sel"
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            byte[] hash = pbkdf2.GetBytes(HashSize);

            //Récupération du résultat
            for (var i = 0; i < HashSize; i++)
            {
                if (hashBytes[i + SaltSize] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }
    }

    
}