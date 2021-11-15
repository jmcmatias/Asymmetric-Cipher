using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Asymmetric_Cipher
{
    class EncryptDecrypt
    {
    
        public static bool validation(BigInteger e, BigInteger d, BigInteger n)
        {
            // Requisitos para que o algoritmo possa ser satizfatório
            // 1 - É possivel encontrar valores de e,d e n tal que M^(ed) mod n = M para todo M < n
            return false;

        }
        public static string encrypt(string message, BigInteger e , BigInteger n)
        {
            string encrypted;
            byte[] bytes = Encoding.Latin1.GetBytes(message);
            byte[] encryptedBytes = new byte[bytes.Length];

            for (int i = 0; i < bytes.Length; i++)
            {
                encryptedBytes[i] = (byte)(BigInteger.ModPow(bytes[i], e, n));
            }

            encrypted = Encoding.Latin1.GetString(encryptedBytes);

            return encrypted;
        }


        public static string decrypt(string message, BigInteger d, BigInteger n)
        {
            string decrypted;
            byte[] bytes = Encoding.Latin1.GetBytes(message);
            byte[] decryptedBytes = new byte[bytes.Length];

            for (int i = 0; i < bytes.Length; i++)
            {

                decryptedBytes[i] = (byte)BigInteger.ModPow(bytes[i],d,n);
            }

            decrypted = Encoding.Latin1.GetString(decryptedBytes);

            return decrypted;

        }
    }
}
