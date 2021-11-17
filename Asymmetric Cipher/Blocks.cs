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
        public static BigInteger streamer;
         public List<byte[]> MessageToByteBlock(string message, int blockSize)
        {
            var toDecrypt = Encoding.Unicode.GetBytes(message);     // Cria um Byte[] com os bytes de Message
            //PrintByteBlock(toDecrypt); //para DEBUG
            Console.WriteLine();

            BigInteger bigIntStream = new(toDecrypt);               // Cria um BigInteger com todos os bytes de Message

            // Console.WriteLine("BIGINTSTREAM CRIADA = " + bigIntStream ); // Para DEBUG

            streamer = bigIntStream;
            return StreamToByteBlock(bigIntStream,blockSize);
        }

        public List<byte[]> StreamToByteBlock(BigInteger stream,int blockSize)
        {
            var byteBlocks = new List<byte[]>();
            while (stream > blockSize)                            // Ciclo que vai criar os blocos da PlainText
            {
                BigInteger.DivRem(stream, BigInteger.Pow(10, (int)blockSize), out BigInteger remainer);               // Calcula o resto da divisão de BigIntStream por 10^n 
                stream = BigInteger.Divide(BigInteger.Subtract(stream, remainer), BigInteger.Pow(10, (int)blockSize));        // (bigIntStream-remainer) / 10^n
                byteBlocks.Insert(0, remainer.ToByteArray());
            }
            if (stream != 0)
                byteBlocks.Insert(0, stream.ToByteArray());
            return byteBlocks;
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

        public BigInteger getStreamer()
        {
            return streamer;
        }
        public void clearStreamer()
        {
            streamer = 0;
        }


    }
}
