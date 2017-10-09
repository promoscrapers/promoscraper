﻿using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace Utilities.Cryptography
{
    /// <summary>
    /// Utility class that handles encryption
    /// </summary>
    public static class AESEncryption
    {
        #region Static Functions        

        /// <summary>
        /// Encrypts a string
        /// </summary>
        /// <param name="PlainText">Text to be encrypted</param>
        /// <param name="Password">Password to encrypt with</param>
        /// <param name="Salt">Salt to encrypt with</param>
        /// <param name="HashAlgorithm">Can be either SHA1 or MD5</param>
        /// <param name="PasswordIterations">Number of iterations to do</param>
        /// <param name="InitialVector">Needs to be 16 ASCII characters long</param>
        /// <param name="KeySize">Can be 128, 192, or 256</param>
        /// <returns>An encrypted string</returns>
        public static string Encrypt(string PlainText, string Password,
            string Salt, string HashAlgorithm,
            int PasswordIterations, string InitialVector,
            int KeySize)
        {
            try
            {
                if (string.IsNullOrEmpty(PlainText))
                    return string.Empty;
                byte[] InitialVectorBytes = Encoding.ASCII.GetBytes(InitialVector);
                byte[] SaltValueBytes = Encoding.ASCII.GetBytes(Salt);
                byte[] PlainTextBytes = Encoding.UTF8.GetBytes(PlainText);
                PasswordDeriveBytes DerivedPassword = new PasswordDeriveBytes(Password, SaltValueBytes, HashAlgorithm, PasswordIterations);
                byte[] KeyBytes = DerivedPassword.GetBytes(KeySize / 8);
                RijndaelManaged SymmetricKey = new RijndaelManaged();
                SymmetricKey.Mode = CipherMode.CBC;
                byte[] CipherTextBytes = null;
                using (ICryptoTransform Encryptor = SymmetricKey.CreateEncryptor(KeyBytes, InitialVectorBytes))
                {
                    using (MemoryStream MemStream = new MemoryStream())
                    {
                        using (CryptoStream CryptoStream = new CryptoStream(MemStream, Encryptor, CryptoStreamMode.Write))
                        {
                            CryptoStream.Write(PlainTextBytes, 0, PlainTextBytes.Length);
                            CryptoStream.FlushFinalBlock();
                            CipherTextBytes = MemStream.ToArray();
                            MemStream.Close();
                            CryptoStream.Close();
                        }
                    }
                }
                SymmetricKey.Clear();
                return Convert.ToBase64String(CipherTextBytes);
            }
            catch { throw; }
        }

        /// <summary>
        /// Decrypts a string
        /// </summary>
        /// <param name="CipherText">Text to be decrypted</param>
        /// <param name="Password">Password to decrypt with</param>
        /// <param name="Salt">Salt to decrypt with</param>
        /// <param name="HashAlgorithm">Can be either SHA1 or MD5</param>
        /// <param name="PasswordIterations">Number of iterations to do</param>
        /// <param name="InitialVector">Needs to be 16 ASCII characters long</param>
        /// <param name="KeySize">Can be 128, 192, or 256</param>
        /// <returns>A decrypted string</returns>
        public static string Decrypt(string CipherText, string Password,
            string Salt, string HashAlgorithm,
            int PasswordIterations, string InitialVector,
            int KeySize)
        {
            try
            {
                if (string.IsNullOrEmpty(CipherText))
                    return string.Empty;
                byte[] InitialVectorBytes = Encoding.ASCII.GetBytes(InitialVector);
                byte[] SaltValueBytes = Encoding.ASCII.GetBytes(Salt);
                byte[] CipherTextBytes = Convert.FromBase64String(CipherText);
                PasswordDeriveBytes DerivedPassword = new PasswordDeriveBytes(Password, SaltValueBytes, HashAlgorithm, PasswordIterations);
                byte[] KeyBytes = DerivedPassword.GetBytes(KeySize / 8);
                RijndaelManaged SymmetricKey = new RijndaelManaged();
                SymmetricKey.Mode = CipherMode.CBC;
                byte[] PlainTextBytes = new byte[CipherTextBytes.Length];
                int ByteCount = 0;
                using (ICryptoTransform Decryptor = SymmetricKey.CreateDecryptor(KeyBytes, InitialVectorBytes))
                {
                    using (MemoryStream MemStream = new MemoryStream(CipherTextBytes))
                    {
                        using (CryptoStream CryptoStream = new CryptoStream(MemStream, Decryptor, CryptoStreamMode.Read))
                        {

                            ByteCount = CryptoStream.Read(PlainTextBytes, 0, PlainTextBytes.Length);
                            MemStream.Close();
                            CryptoStream.Close();
                        }
                    }
                }
                SymmetricKey.Clear();
                return Encoding.UTF8.GetString(PlainTextBytes, 0, ByteCount);
            }
            catch { throw; }
        }

        /// <summary>
        /// Encrypts a string
        /// </summary>
        /// <param name="PlainText">Text to be encrypted</param>
        /// <param name="Password">Password to encrypt with</param>        
        /// <returns>An encrypted string</returns>
        public static string Encrypt(string PlainText, string Password)
        {
            string Salt, HashAlgorithm, InitialVector;
            int PasswordIterations, KeySize;
            string CipherText;

            try
            {
                Salt = "@LAX!";
                HashAlgorithm = "SHA1";
                PasswordIterations = 2;
                InitialVector = "L47EFE7265931GSN";
                KeySize = 128;

                CipherText = Encrypt(PlainText, Password, Salt, HashAlgorithm, PasswordIterations, InitialVector, KeySize);
            }
            catch { throw; }

            return (CipherText);
        }

        /// <summary>
        /// Encrypts a string
        /// </summary>
        /// <param name="PlainText">Text to be encrypted</param>
        /// <param name="Password">Password to encrypt with</param>        
        /// <returns>An encrypted string</returns>
        public static string Decrypt(string CipherText, string Password)
        {
            string Salt, HashAlgorithm, InitialVector;
            int PasswordIterations, KeySize;
            string PlainText;

            try
            {
                //--Should match the values in Encrypt function above if changed--\\
                Salt = "@LAX!";
                HashAlgorithm = "SHA1";
                PasswordIterations = 2;
                InitialVector = "L47EFE7265931GSN";
                KeySize = 128;
                //-----------------------------------------------------------------\\

                PlainText = Decrypt(CipherText, Password, Salt, HashAlgorithm, PasswordIterations, InitialVector, KeySize);
            }
            catch { throw; }

            return (PlainText);
        }
        #endregion


        public static String GetMD5Hash(String s)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            Byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(s));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

    }
}
