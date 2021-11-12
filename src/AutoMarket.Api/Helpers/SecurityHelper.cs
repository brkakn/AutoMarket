using AutoMarket.Api.Constants;
using System;
using System.Security.Cryptography;
using System.Text;

namespace AutoMarket.Api.Helpers
{
    public static class SecurityHelper
    {
        public static string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] rawDataBytes = Encoding.UTF8.GetBytes(rawData);
                byte[] saltBytes = Encoding.UTF8.GetBytes(CommonConstants.ENCRYPT_KEY);
                byte[] saltedInput = new byte[saltBytes.Length + rawDataBytes.Length];

                saltBytes.CopyTo(saltedInput, 0);
                rawDataBytes.CopyTo(saltedInput, saltBytes.Length);

                byte[] hashedBytes = sha256Hash.ComputeHash(saltedInput);

                return BitConverter.ToString(hashedBytes);
            }
        }
    }
}
