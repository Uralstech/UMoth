#nullable enable
using System;
using System.Security.Cryptography;
using System.Text;

namespace Uralstech.UMoth
{
    /// <summary>
    /// Utilities for cryptographically secure hashing.
    /// </summary>
    public static class HashUtils
    {
        private static readonly char[] s_nonceCharset = "0123456789ABCDEFGHIJKLMNOPQRSTUVXYZabcdefghijklmnopqrstuvwxyz-._".ToCharArray();

        /// <summary>
        /// Generates a cryptographically secure nonce of length <paramref name="length"/>.
        /// </summary>
        /// <remarks>
        /// Loosely based on this Firebase example:
        /// <a href="https://firebase.google.com/docs/auth/ios/apple#sign_in_with_apple_and_authenticate_with_firebase"/>
        /// </remarks>
        /// <param name="length">The length of the nonce.</param>
        /// <returns>The generated nonce.</returns>
        /// <exception cref="ArgumentException">Thrown if <paramref name="length"/> is &lt;= 0.</exception>
        public static string RandomNonceString(int length = 32)
        {
            if (length <= 0)
                throw new ArgumentException("Length must be greater than 0.", nameof(length));

            byte[] randomBytes = new byte[length];
            RandomNumberGenerator.Fill(randomBytes);

            return string.Create(length, randomBytes, (span, data) =>
            {
                int dataLength = data.Length;
                int charsetLength = s_nonceCharset.Length;
                for (int i = 0; i < dataLength; i++)
                    span[i] = s_nonceCharset[data[i] % charsetLength];
            });
        }

        /// <summary>
        /// Hashes a string using SHA256 and returns it encoded as a HEX string.
        /// </summary>
        /// <param name="content">The content to hash.</param>
        /// <returns>The hashed data as a HEX string.</returns>
        /// <exception cref="ArgumentException">Thrown if <paramref name="content"/> is <see langword="null"/> or empty.</exception>
        public static string Sha256Hash(string content)
        {
            if (string.IsNullOrEmpty(content))
                throw new ArgumentException("Content cannot be null or empty.", nameof(content));

            byte[] utf8Data = Encoding.UTF8.GetBytes(content);

            using SHA256 sha256 = SHA256.Create();
            byte[] hashedData = sha256.ComputeHash(utf8Data);

            return string.Create(hashedData.Length * 2, hashedData, (span, data) =>
            {
                int dataLength = data.Length;
                for (int i = 0; i < dataLength; i++)
                    data[i].TryFormat(span[(i * 2)..], out _, "X2");
            });
        }
    }
}