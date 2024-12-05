using Web;
using System.Text;
using System.Security.Cryptography;

namespace Extensions
{
    public static class Crypto
    {

        public static byte[] GetHash(IdKeys keys, string password)
        {

            Encoding encoding = Encoding.UTF8;


            using SHA512 sha = SHA512.Create();


            byte[] passwordBytes = encoding.GetBytes(password);

            byte[] hashedPass = sha.ComputeHash(passwordBytes);


            string hashedString = encoding.GetString(hashedPass);

            hashedString += keys.Salt;


            byte[] saltedPass = encoding.GetBytes(hashedString);

            return sha.ComputeHash(saltedPass);
        }
    }
}
