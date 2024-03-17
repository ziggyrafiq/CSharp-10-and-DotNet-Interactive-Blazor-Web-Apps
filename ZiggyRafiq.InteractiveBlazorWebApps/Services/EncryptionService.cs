using System.Security.Cryptography;
using System.Text;

namespace ZiggyRafiq.InteractiveBlazorWebApps.Services;
public class EncryptionService
{
    public string Encrypt(string data)
    {
        using (var aes = Aes.Create())
        {
            if (aes == null)
            {
                throw new InvalidOperationException("AES encryption algorithm is not supported.");
            }

            aes.GenerateIV();

            string ivString = Convert.ToBase64String(aes.IV);

            byte[] plainTextBytes = Encoding.UTF8.GetBytes(data);


            using (var encryptor = aes.CreateEncryptor())
            {
                byte[] encryptedBytes = encryptor.TransformFinalBlock(plainTextBytes, 0, plainTextBytes.Length);


                string encryptedData = Convert.ToBase64String(encryptedBytes);

                return ivString + ":" + encryptedData;
            }
        }
    }


    public string Decrypt(string encryptedData)
    {
        string[] parts = encryptedData.Split(':');
        if (parts.Length != 2)
        {
            throw new ArgumentException("Invalid encrypted data format.");
        }

        byte[] ivBytes = Convert.FromBase64String(parts[0]);

        byte[] encryptedBytes = Convert.FromBase64String(parts[1]);

        using (var aes = Aes.Create())
        {
            if (aes == null)
            {
                throw new InvalidOperationException("AES encryption algorithm is not supported.");
            }

            aes.IV = ivBytes;

            using (var decryptor = aes.CreateDecryptor())
            {
                byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);

                string decryptedData = Encoding.UTF8.GetString(decryptedBytes);

                return decryptedData;
            }
        }
    }

}
