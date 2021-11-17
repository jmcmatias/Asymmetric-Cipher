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

            var MessageBlocks = blocks.MessageToByteBlock( message, blockSize); // transforma a mensagem em blocos tipo lista de strings para poderem ser cifrados

            Console.WriteLine(" Message Blocks ");

            foreach (byte[] Mblock in MessageBlocks)                              // Ciclo que vai cifrar cada bloco e transmitir cada um para o destino
            {
                Console.Write(" Block | ");
                blocks.PrintByteBlock(Mblock);
                Console.Write(" | -> ");
                BigInteger block = new(Mblock);                                   // Torna o bloco atual em BigInteger
                byte[] encBlock = BigInteger.ModPow(block, PU.getK(), PU.getN()).ToByteArray(); // executa a cifra C = M^e mod n e converte-a num array de bytes

                Console.Write(" Cifrado | ");
                blocks.PrintByteBlock(encBlock);
                Console.Write(" | -> ");

                TransmitBlock(encBlock);                                          // Envia o bloco cifrado para o receiver.
            }
        }
        
        // Função para colocar o bloco cifrado no receiver
        public static void TransmitBlock(byte[] block) 
        {
            Receiver.RecieveBlock(block);
        }

        // Função que recebe a chave publica
        public void ReceiveKeyPair(BigInteger e, BigInteger n)
        {
            PU.setPair(e, n);
        }
    }
}
