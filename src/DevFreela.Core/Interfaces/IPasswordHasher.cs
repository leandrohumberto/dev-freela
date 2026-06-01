namespace DevFreela.Core.Interfaces
{
    public interface IPasswordHasher
    {
        /// <summary>
        /// Hashes the specified password using a secure algorithm.
        /// </summary>
        /// <param name="password">The plaintext password to hash.</param>
        /// <returns>The hashed password.</returns>
        string Hash(string password);

        /// <summary>
        /// Verifies that a plaintext password matches the specified hash.
        /// </summary>
        /// <param name="hash">The hash to compare against.</param>
        /// <param name="providedPassword">The plaintext password to verify.</param>
        /// <returns>True if the password matches the hash; otherwise, false.</returns>
        bool Verify(string hash, string providedPassword);

        /// <summary>
        /// Computes a SHA256 hash of the password.
        /// </summary>
        /// <remarks>
        /// Deprecated. Use <see cref="Hash(string)"/> instead for enhanced security.
        /// </remarks>
        [Obsolete("This method is deprecated and will be removed in a future release. Use 'Hash' instead.")]
        string ComputeHash(string password);
    }
}
