using System;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace Utilities.Cryptography
{
    public static class DESEncryption
    {
        private const string DESIV = "IMXALLFF";

        public static string Encrypt(string PlainText, string Password)
        {
            byte[] key;
            byte[] IV;

            byte[] inputByteArray;
            try
            {

                key = Convert2ByteArray(Password);

                IV = Convert2ByteArray(DESIV);

                inputByteArray = Encoding.UTF8.GetBytes(PlainText);
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();

                MemoryStream ms = new MemoryStream(); CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);

                cs.FlushFinalBlock();

                return Convert.ToBase64String(ms.ToArray());
            }

            catch (System.Exception ex)
            {

                throw ex;
            }
        }

        public static string Decrypt(string CipherText, string Password)
        {
            byte[] key;
            byte[] IV;

            byte[] inputByteArray;
            try
            {

                key = Convert2ByteArray(Password);

                IV = Convert2ByteArray(DESIV);

                int len = CipherText.Length;
                inputByteArray = Convert.FromBase64String(CipherText);

                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);

                cs.FlushFinalBlock();

                Encoding encoding = Encoding.UTF8; return encoding.GetString(ms.ToArray());
            }

            catch (System.Exception ex)
            {

                throw ex;
            }
        }

        static byte[] Convert2ByteArray(string strInput)
        {

            int intCounter; char[] arrChar;
            arrChar = strInput.ToCharArray();

            byte[] arrByte = new byte[arrChar.Length];

            for (intCounter = 0; intCounter <= arrByte.Length - 1; intCounter++)
                arrByte[intCounter] = Convert.ToByte(arrChar[intCounter]);

            return arrByte;
        }
    }
}
