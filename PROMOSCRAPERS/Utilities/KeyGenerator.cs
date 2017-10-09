using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;


namespace Utilities
{
    class KeyGenerator
    {
        public KeyGenerator(int maxSize, bool NumbersOnly)
        {
            char[] chars = new char[62];
            if (NumbersOnly)
                chars = "1234567890".ToCharArray();
            else
                chars = "abcdefghijklmnopqrstuvwxyz1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890~!@#$%^&*()_-+=}{][|;:.?".ToCharArray();
            byte[] data = new byte[1];

            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();


                crypto.GetNonZeroBytes(data);
                data = new byte[maxSize];
                crypto.GetNonZeroBytes(data);
           
            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }

            RandomString = result.ToString();
        }



        string _RandomString;
        public string RandomString
        {
            get
            {
                return _RandomString;
            }
            set
            {
                _RandomString = value;
            }
        }
    }
}
