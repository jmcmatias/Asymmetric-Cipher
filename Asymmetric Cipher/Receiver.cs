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
                Console.WriteLine();
                Console.WriteLine(" Chaves escolhidas:");
                Console.Write(" PU = { " + e + ", " + n + " } e PR = { " + d + ", " + n + " } ");
                Console.WriteLine();
                return true;
            }
            return false;
            
        }

        //Função que transmite a chave publica ao sender
        public static void TransmitKeyPair(BigInteger e, BigInteger n)
        {
            sender.ReceiveKeyPair(e, n);
        }

        // Função que vai decifrar os blocos
        public static void decrypt(byte[] CBlock)
        {
            int blockSize = (int)((Math.Log2((double)PU.getN()) + 1) / ((double)PU.getK()));  // Calculo do tamanho dos blocos em função da chave publica para o Sender também o poder calcular
            BigInteger block = new(CBlock);                                                   // Cria um BigInt a partir do bloco de Butes recebido
            var decryptedBigInt = BigInteger.ModPow(block, PV.getK(), PV.getN());             // Calcula M = C^d mod n e coloca num BigInt

            Console.Write(" Decifrado -> | ");
            blocks.PrintByteBlock(decryptedBigInt.ToByteArray());                             // imprime o bloco decifrado  
            Console.Write(" |");

            // Este objeto recievedBigIntStream recebe a stream de BigInts decifrados
            recievedBigIntStream = BigInteger.Multiply(recievedBigIntStream, BigInteger.Pow(10, blockSize)); // stream = stream*10^blockSize - Faz um "shift" á esquerda na stream do tamanho de um bloco para se poder inserir o novo bloco decifrado  
            recievedBigIntStream = BigInteger.Add(recievedBigIntStream, decryptedBigInt);                    // Insere o bloco decifrado na stream
            //Console.WriteLine(recievedBigIntStream); // para DEBUG
            Console.WriteLine("");

        }

        // Função que recebe um bloco de dados, imprime-o e chama a função decrypt que irá tratar o bloco
        public void RecieveBlock (byte[] block)
        {
            Console.Write(" Recebido -> | ");
            blocks.PrintByteBlock(block);
            Console.Write(" | ");
            decrypt(block);
        }

        // Função que calcula os Pares de chaves e pede a sua validação
        public static bool CalculateKeys(BigInteger p,BigInteger q)
        {
            var fi = calc.Phi(p, q);
            var n = BigInteger.Multiply(p, q);
            var e = calc.Select_e(fi,n);
            var d = calc.ModInverse(e, calc.Phi(p,q));
            return Validation(e, d, n);
        }

        // função que imprime o Strem de BigInts, serve apenas para debug.
        public static void PrintBigIntStream()
        {
            /*//Para DEBUG
            Console.WriteLine("BIGINTSTREAM CRIADA = " + blocks.getStreamer() + " <- Enviada ");
            Console.WriteLine("BIGINTSTREAM CRIADA = " + recievedBigIntStream + " <- Recebida ");
            */
            //blocks.clearStreamer();
        }

        // Função que imprime a mensagem decifrada a partir do stream de BigInts após serem tratados em ConvertBigIntStreamToString
        public static void PrintMessageReceived()
        {
            receivedMessage = blocks.ConvertBigIntStreamToString(recievedBigIntStream); 
            Console.WriteLine("A Mensagem Recebida foi: "+receivedMessage );
            Console.WriteLine();
        }

    }
}
