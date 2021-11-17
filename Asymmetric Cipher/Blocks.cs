using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Asymmetric_Cipher
{
    class Blocks
    {
        //public static BigInteger streamer; // para DEBUG
         public List<byte[]> MessageToByteBlock(string message, int blockSize)
        {
            var toDecrypt = Encoding.Unicode.GetBytes(message);     // Cria um Byte[] com os bytes de Message
            //PrintByteBlock(toDecrypt); //para DEBUG
            Console.WriteLine();

            BigInteger bigIntStream = new(toDecrypt);               // Cria um BigInteger com todos os bytes de Message

            // Console.WriteLine("BIGINTSTREAM CRIADA = " + bigIntStream ); // Para DEBUG

            //streamer = bigIntStream;       // Apenas para DEBUG
            return StreamToByteBlock(bigIntStream,blockSize);
        }

        public List<byte[]> StreamToByteBlock(BigInteger stream,int blockSize)
        {
            var byteBlocks = new List<byte[]>();
            while (stream > blockSize)                            // Ciclo que vai criar os blocos da PlainText enquanto a stream dor maior que o blocksize
            {   // Calcula o resto da divisão da stream por 10^blockSize de modo a recolher o valor do bloco
                // ex: stream=32156749835448795 tamanho do bloco=3, o resto será 795 e colocado em remain
                // Seguidamente subtrai o valor do remain pela stream e divide-a por 10^blockSize 
                // ex: stream=32156749835448 795, blocksize=3 , após a subtração fica stream=32156749835448 000 e após a divisão fica stream=32156749835448 
                // ficando o stream pronto ser retirado outro bloco
                BigInteger.DivRem(stream, BigInteger.Pow(10, (int)blockSize), out BigInteger remainer);               // Calcula o resto da divisão de BigIntStream por 10^n 
                stream = BigInteger.Divide(BigInteger.Subtract(stream, remainer), BigInteger.Pow(10, (int)blockSize));        // (bigIntStream-remainer) / 10^n
                byteBlocks.Insert(0, remainer.ToByteArray());     // Insere o bloco na lista no Index zero
            }
            if (stream != 0)       // se ao acabar o ciclo a stream ainda ainda não for zero, então ainda falta mais um bloco
                byteBlocks.Insert(0, stream.ToByteArray());
            return byteBlocks;     // retorna a lista de blocos de bytes até encrypt no sender
        }


        public string ConvertBigIntStreamToString(BigInteger stream)
        {
            byte[] bytes = stream.ToByteArray();
            return Encoding.Latin1.GetString(bytes);   
        }

        public void PrintByteBlock(byte[] block)
        {
            foreach (byte c in block)
                Console.Write(c.ToString());
        }


        /*//Para DEBUG
        public BigInteger getStreamer()
        {
            return streamer;
        }
        public void clearStreamer()
        {
            streamer = 0;
        }
        */

    }
}
