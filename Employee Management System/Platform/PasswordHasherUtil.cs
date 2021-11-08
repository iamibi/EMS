using System;
using System.Security;
using System.Security.Cryptography;

namespace Employee_Management_System.Platform
{
    public class PasswordHasherUtil
    {
        private const int SALT_SIZE = 64;
        private const int HASH_SIZE = 64;
        private const int PBKDF2_ITERATIONS = 100000;

        public string Salt { get; set; }
        public string Digest { get; set; }

        public PasswordHasherUtil(string password, byte[] providedSalt = null)
        {
            byte[] Salt = providedSalt;
            if (providedSalt == null)
                Salt = GenerateRandomSalt();

            byte[] Digest = ApplyPBKDF2Algo(password, Salt);
            this.Salt = Convert.ToBase64String(Salt);
            this.Digest = Convert.ToBase64String(Digest);
        }

        private byte[] GenerateRandomSalt()
        {
            // Generate a random salt.
            RNGCryptoServiceProvider RNGProvider = new RNGCryptoServiceProvider();
            byte[] Salt = new byte[SALT_SIZE];
            RNGProvider.GetBytes(Salt);

            return Salt;
        }

        private byte[] ApplyPBKDF2Algo(string Password, byte[] Salt)
        {
            // Generate hash.
            Rfc2898DeriveBytes Pbkdf2 = new Rfc2898DeriveBytes(Password, Salt, PBKDF2_ITERATIONS);
            return Pbkdf2.GetBytes(HASH_SIZE);
        }

    }
}
