using System;
using System.Diagnostics;
using System.Numerics;
using System.Text;

namespace Asymmetric_Cipher
{
    class Program
    {
        static void Main(string[] args)
        {
            var cmd = new Process
            {
                StartInfo =
                {
                    FileName = "cmd.exe",
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true,
                    UseShellExecute = false
                }
            };
            cmd.Start();

            cmd.StandardInput.WriteLine("chcp 1252");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();

            string control = "start";

            while (control != "x")
            {
                
                //Console.OutputEncoding = System.Text.Encoding.GetEncoding(1252);
                PrimeCalc Euler = new PrimeCalc();
                PrimeCalc RandomPrime = new PrimeCalc();
                PrimeCalc Euclid = new PrimeCalc();

                RSA asymEncript = new RSA();

                Console.WriteLine("Irão ser gerados dois números primos num intervalo (p e q), Insira o valor máximo do Intervalo.");

                int max;
                while (!Int32.TryParse(Console.ReadLine(),out max))
                    Console.WriteLine("Insira um número inteiro");

                Console.Write("A gerar e testar números primos ...");
                Console.WriteLine();

                // Gera dois números primos aleatórios e diferentes entre si
               
                int p = RandomPrime.GetRandomPrime(max);
                int q = RandomPrime.GetRandomPrime(max);
                while (p == q)
                {
                    q = RandomPrime.GetRandomPrime(max);
                }



                //p = 17;
               // q = 11;
                Console.Write("p= " + p + " e q= " + q);

                BigInteger n = BigInteger.Multiply(p,q);
               // BigInteger n = 17 * 11;
               while (n > 255)
                {
                    Console.Write(" !!!Recusados por n (p*q) ser Superior a 255 (8bits)!!!");
                    p = RandomPrime.GetRandomPrime(max);
                    q = RandomPrime.GetRandomPrime(max);

                    while (p == q)
                    {
                        q = RandomPrime.GetRandomPrime(max);
                    }

                    Console.Write("p= " + p + " e q= " + q);

                    //p = 17;
                    // q = 11;


                    n = BigInteger.Multiply(p, q);
                }
                Console.WriteLine(" foram os números primos seleccionados.");

                BigInteger e = Euler.Select_e(n);
                //BigInteger e = 11;

                BigInteger d = Euclid.ModInverse(e, Euler.Phi(n));

                Console.WriteLine();
                Console.WriteLine("Chaves escolhidas:");
                Console.Write("PU = { " + e + ", " + n + " } e PR = { " + d + ", " + n + " } ");
                //Console.WriteLine(" n= " + n + " phi é: " + Euler.Phi(n) + " e = " + e + " d= " + d);


                Console.WriteLine();
                Console.Write("Inserir a mensagem a encriptar: ");
                string message = Console.ReadLine();

                Console.WriteLine("A mensagem original é: " + message);

                string encryptedMessage = RSA.encrypt(message, e, n);

                Console.WriteLine("A mensagem encriptada é: " + encryptedMessage);

                string decryptedMessage = RSA.decrypt(encryptedMessage, d, n);

                Console.WriteLine("Pressione a tecla 'Enter' para ver a mensagem desencriptada");

                Console.ReadLine();

                Console.WriteLine("A mensagem desencriptada é: " + decryptedMessage);

                Console.WriteLine("Press x to exit");
                control = Console.ReadLine();
            }
        }
    }
}
