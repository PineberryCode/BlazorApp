using Isopoh.Cryptography.Argon2;
using System.Security.Cryptography;

namespace BlazorApp.Security
{
    public class ArgonService {
        public (string hash, string salt) GenerateHashAndSalt(string password)
            {
                byte[] saltBytes = RandomNumberGenerator.GetBytes(32);
                string salt = Convert.ToBase64String(saltBytes);

                var config = new Argon2Config
                {
                    Type = Argon2Type.DataIndependentAddressing,
                    Version = Argon2Version.Nineteen,
                    Password = System.Text.Encoding.UTF8.GetBytes(password),
                    Salt = saltBytes,
                    TimeCost = 4,
                    MemoryCost = 65536,
                    Lanes = 2,
                    Threads = 2,
                    HashLength = 32
                };

                using var argon2 = new Argon2(config);
                using var hash = argon2.Hash();
                string hashString = Argon2.Hash(config);

                return (hashString, salt);
            }   
    }
}