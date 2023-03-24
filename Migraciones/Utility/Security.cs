namespace Sistema_De_Tutorias.Utility;

using System.Security.Cryptography;
using System.Text;

public class Security
{
    public static string HashSHA256(string input)
    {
        using (SHA256Managed sha256 = new SHA256Managed())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            byte[] hash = sha256.ComputeHash(bytes);
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                stringBuilder.Append(hash[i].ToString("x2"));
            }

            return stringBuilder.ToString();
        }
    }
}