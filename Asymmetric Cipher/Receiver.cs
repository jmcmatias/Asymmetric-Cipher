using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Asymmetric_Cipher
{
    class Receiver
    {
        static string receivedMessage = string.Empty;
        static public bool valid = false;
        static BigInteger recievedBigIntStream = 0;
        static AuxCalc calc = new();
        static Blocks blocks = new();

        static KeyPair PV = new();
        static KeyPair PU = new();

        static Sender sender = new();


        


        public static bool Validation(BigInteger e, BigInteger d, BigInteger n)
        {
            recievedBigIntStream = 0;
            // Requisitos para que o algoritmo possa ser satizfatório
            // 1 - É possivel encontrar valores de e,d e n tal que M^(ed) mod n = M para todo M < n
            char C = 'x';
            decimal M = C;

            Console.WriteLine(" Validação do primeiro requisito do algoritmo valor inteiro do char " + C + " = " + M +
                Environment.NewLine + " e= " + e + " d= " + d + " n= " + n + " então M^(ed) mod n = M para todo M < n "+
                Environment.NewLine + " A Validar...");

            int test = ((int)(BigInteger.Pow((BigInteger)M, (int)(BigInteger.Multiply(e, d))) % n));

            Console.WriteLine(" Valor calculado para "+ C +" é M = " + test);
            
            if( M == test)
            {
                PV.setPair(d, n);
                PU.setPair(e, n);
                TransmitKeyPair(e, n);
                Console.WriteLine(" Valores são válidos");
                Console.WriteLine(" Chaves escolhidas:");
                Console.Write(" PU = { " + e + ", " + n + " } e PR = { " + d + ", " + n + " } ");
                Console.WriteLine();
                return true;
            }
            return false;
            
        }


        public static void TransmitKeyPair(BigInteger e, BigInteger n)
        {
            sender.ReceiveKeyPair(e, n);
        }



        public static void decrypt(byte[] CBlock)
        {
            
            int blockSize = (int)((Math.Log2((double)PU.getN()) + 1) / ((double)PU.getK()));
            BigInteger block = new(CBlock);
            var decryptedBigInt = BigInteger.ModPow(block, PV.getK(), PV.getN());



            Console.Write(" Decifrado -> | ");
            blocks.PrintByteBlock(decryptedBigInt.ToByteArray());
            Console.Write(" | -> ");



            recievedBigIntStream = BigInteger.Multiply(recievedBigIntStream, BigInteger.Pow(10, blockSize));
            recievedBigIntStream = BigInteger.Add(recievedBigIntStream, decryptedBigInt);
            //Console.WriteLine(recievedBigIntStream); // para DEBUG
            Console.WriteLine("");

        }

        public void RecieveBlock (byte[] block)
        {
            Console.Write(" Recebido -> | ");
            blocks.PrintByteBlock(block);
            Console.Write(" | ");
            decrypt(block);
            
        }

        public static bool CalculateKeys(BigInteger p,BigInteger q)
        {
            var fi = calc.Phi(p, q);
            var n = BigInteger.Multiply(p, q);
            var e = calc.Select_e(fi,n);
            var d = calc.ModInverse(e, calc.Phi(p,q));
            Console.WriteLine("p= " + p + " e q= " + q + " n= " + p * q + " fi= " + fi);
            return Validation(e, d, n);
        }

        public static void DecodeReceivedMessage()
        {
           
        }

        public static void PrintBigIntStream()
        {
            /*//Para DEBUG
            Console.WriteLine("BIGINTSTREAM CRIADA = " + blocks.getStreamer() + " <- Enviada ");
            Console.WriteLine("BIGINTSTREAM CRIADA = " + recievedBigIntStream + " <- Recebida ");
            */
            blocks.clearStreamer();
        }
        public static void PrintMessageReceived()
        {
            receivedMessage = blocks.ConvertBigIntStreamToString(recievedBigIntStream);
            Console.WriteLine("A Mensagem Recebida foi: "+receivedMessage );
            Console.WriteLine();
        }

    }
}
