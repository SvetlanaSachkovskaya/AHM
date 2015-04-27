using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace AHM.Common
{
	public static class CipherMaker
	{
		private const int KeySize = 256;
		// InitVector must be equal KeySize / 8. In this case 32 bytes.
		private const string InitVector = "ahmanagement2015";
		private const string PasswordPhrase = "!Pass$Otr@@";


		public static string Encrypt(string plainText, string salt)
		{
			if (string.IsNullOrWhiteSpace(salt))
			{
				throw new ArgumentNullException("salt", "Unique salt is required to encrypt password");
			}
			var initVectorBytes = Encoding.ASCII.GetBytes(InitVector);
			var saltBytes = Encoding.UTF8.GetBytes(salt);
			var plainTextBytes = Encoding.UTF8.GetBytes(plainText);

			var password = new Rfc2898DeriveBytes(PasswordPhrase, saltBytes, 2);
			var keyBytes = password.GetBytes(KeySize / 8);
			var symmetricKey = new RijndaelManaged { Mode = CipherMode.CBC };

			var encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
			var memoryStream = new MemoryStream();
			var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
			cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
			cryptoStream.FlushFinalBlock();
			var cipherTextBytes = memoryStream.ToArray();
			memoryStream.Close();
			cryptoStream.Close();

			var cipherText = Convert.ToBase64String(cipherTextBytes);
			return cipherText;
		}


		public static string Decrypt(string encryptedText, string salt)
		{
			if (string.IsNullOrWhiteSpace(salt))
			{
				throw new ArgumentNullException("salt", "Unique salt is required to decrypt password");
			}
            var initVectorBytes = Encoding.ASCII.GetBytes(InitVector);
			var saltBytes = Encoding.UTF8.GetBytes(salt);
			var encryptedTextBytes = Convert.FromBase64String(encryptedText);

			var password = new Rfc2898DeriveBytes(PasswordPhrase, saltBytes, 2);
			var keyBytes = password.GetBytes(KeySize / 8);
			var symmetricKey = new RijndaelManaged { Mode = CipherMode.CBC };

            var decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
			var memoryStream = new MemoryStream(encryptedTextBytes);
			var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
			var plainTextBytes = new byte[encryptedTextBytes.Length];
			var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
			memoryStream.Close();
			cryptoStream.Close();

			var plainText = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
			return plainText;
		}
	}
}