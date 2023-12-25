using System.Security.Cryptography;
namespace RentSaaS.API.Helpers;
public class PasswordHasher
{
    private static readonly RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
    private static readonly int SaltSise = 16;
    private static readonly int HashSise = 20;
    private static readonly int Iterations = 10000;

    public static string HashPassword(string password)
    {
        byte[] salt;
        rng.GetBytes(salt = new byte[SaltSise]);
        var key = new Rfc2898DeriveBytes(password, salt, Iterations);
        var hash = key.GetBytes(HashSise);

        var hashBytes = new byte[SaltSise + HashSise];
        Array.Copy(salt, 0, hashBytes, 0, SaltSise);
        Array.Copy(hash, 0, hashBytes, SaltSise, HashSise);

        var base64hash = Convert.ToBase64String(hashBytes);

        return base64hash;
    }

    public static bool VerifyPassword(string password, string base64Hash)
    {
        var hashBytes = Convert.FromBase64String(base64Hash);
        var salt = new byte[SaltSise];
        Array.Copy(hashBytes, 0, salt, 0, SaltSise);

        var key = new Rfc2898DeriveBytes(password, salt, Iterations);
        byte[] hash = key.GetBytes(HashSise);

        for (int i = 0; i < HashSise; i++)
        {
            if (hashBytes[i + SaltSise] != hash[i])
            {
                return false;
            }
        }
        return true;
    }
}
