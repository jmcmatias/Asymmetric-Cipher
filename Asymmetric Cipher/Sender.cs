using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Asymmetric_Cipher
{
    class Sender
    {
        static Blocks blocks = new();
        static Receiver Receiver = new();
        static KeyPair PU = new();
       
        public static void encrypt(string message)
        {
            int blockSize = (int)((Math.Log2((double)PU.getN()) + 1) / ((double)PU.getK()));
            //int blockSize = 3;
            //var encryptedBigIntBlock = new List<decimal>();
            var MessageBlocks = blocks.MessageToByteBlock( message, blockSize); // transforma a mensagem em blocos tipo lista de strings para poderem ser cifrados

            Console.WriteLine("Message Blocks Em Encrypt = ");

            foreach (byte[] Mblock in MessageBlocks)                              // Ciclo que vai cifrar cada bloco e transmitir cada um para o destino
            {
                Console.Write(" PlainText | ");
                blocks.PrintByteBlock(Mblock);
                Console.Write(" | -> ");
                BigInteger block = new(Mblock);
                byte[] encBlock = BigInteger.ModPow(block, PU.getK(), PU.getN()).ToByteArray();

                Console.Write(" Cifrado | ");
                blocks.PrintByteBlock(encBlock);
                Console.Write(" | -> ");
                Receiver.RecieveBlock(encBlock);

            }
                        

            //var test = BigIntBlockToMessage(MessageBlocks,n);


            //Console.WriteLine(" TESTE ENCRYPTED " + encrypted);

        }
        
        public void TransmitBlock(byte[] block) 
        {
            Receiver.RecieveBlock(block);
        }

        public void ReceiveKeyPair(BigInteger e, BigInteger n)
        {
            PU.setPair(e, n);
        }


        // Para formatação de Output
        static int CountDigits( BigInteger n)
        {
            int count = 0;
            while (n > 0)
            {
                n /= 10;
                count++;
            }
            return count;
        }

    }
}
