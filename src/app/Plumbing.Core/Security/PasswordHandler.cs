using System.Security.Cryptography;
using Plumbing.Extensions;

namespace Plumbing.Security
{
    public class PasswordHandler
    {
        static readonly HashAlgorithm hashAlgorithm = new SHA1Managed();
        static readonly RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

        public PasswordHandler(string password)
        {
            var salt = new byte[8];
            rng.GetBytes(salt);
            Salt = salt.ToBase64();
            PasswordHash = GetHash(password, Salt);
        }

        public PasswordHandler(string passwordHash, string salt)
        {
            Salt = salt;
            PasswordHash = passwordHash;
        }

        public string Salt { get; private set; }
        public string PasswordHash { get; private set; }

        public bool PasswordMatches(string password)
        {
            return PasswordHash == GetHash(password, Salt);
        }

        static string GetHash(string password, string salt)
        {
            return hashAlgorithm.ComputeHash((password + salt).ToBytes()).ToBase64();
        }
    }
}