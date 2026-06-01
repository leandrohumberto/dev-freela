using DevFreela.Core.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace DevFreela.Infrastructure.Security
{
    public class PasswordHasher : IPasswordHasher
    {
        /// <summary>
        /// Hashes the specified password using BCrypt.
        /// </summary>
        public string Hash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        /// <summary>
        /// Verifies that the password matches the BCrypt hash.
        /// </summary>
        public bool Verify(string hash, string providedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(providedPassword, hash);
        }

        /// <summary>
        /// Computes a SHA256 hash of the password.
        /// </summary>
        /// <remarks>
        /// Deprecated. Use <see cref="Hash(string)"/> instead.
        /// </remarks>
        [Obsolete("This method is deprecated and will be removed in a future release. Use 'Hash' instead.")]
        public string ComputeHash(string password)
        {
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var hashBytes = SHA256.HashData(passwordBytes);
            var builder = new StringBuilder();

            for (var i = 0; i < hashBytes.Length; i++)
            {
                builder.Append(hashBytes[i].ToString("x2"));
            }

            return builder.ToString();
        }
    }
}
