using System;
using System.Diagnostics;
using System.Numerics;
using System.Text;

namespace Asymmetric_Cipher
{
    class Asymetric_Cipher
    {
        static void Main(string[] args)
        {
          

            Console.InputEncoding = Encoding.Unicode;

            string control = "start";

            while (control != "x")
            {
                //Console.OutputEncoding = System.Text.Encoding.GetEncoding(1252);
                AuxCalc RandomPrime = new();
                    
                Console.WriteLine(" Algoritmo de cifra assimétrico baseado no RSA." + 
                    Environment.NewLine + "     Irão ser gerados dois números primos (p e q), num intervalo entre 3 e um valor máximo." +
                    Environment.NewLine + "     Insira o valor máximo do Intervalo maior que 20 até 1024.");

                int max=0;
                while (max > 1024 || max < 20)
                {
                    while (!Int32.TryParse(Console.ReadLine(), out max))
                        Console.WriteLine(" Insira um número inteiro entre 20 e 1024");
                    if (max > 1024 || max < 20)
                        Console.WriteLine(" Insira um número inteiro entre 20 e 1024");
                }
                
                Console.Write(" A gerar e testar números primos ...");
                Console.WriteLine();
                var valid = false;
                // Gera dois números primos aleatórios e diferentes entre si
                do
                {
                    int p = RandomPrime.GetRandomPrime(max);
                    int q = RandomPrime.GetRandomPrime(max);
                    while (p == q)
                    {
                        q = RandomPrime.GetRandomPrime(max);
                    }

                    Console.Write(" Seleção automática de p = " + p + " e q = " + q + " com  n = " + p * q + " fi =" + (p - 1) * (q - 1));

                    valid=Receiver.CalculateKeys(p, q);
                    if (!valid)
                    {
                        Console.WriteLine(" Os números primos seleccionados não são válidos a gerar outros");
                    }
                } while (!valid);

                Console.Write(" Inserir a mensagem a encriptar: ");
                string message = Console.ReadLine();
                Sender.encrypt(message);
                Receiver.PrintBigIntStream();
                Console.WriteLine("Pressione a tecla 'Enter' para ver a mensagem desencriptada");
                Console.ReadLine();
                Receiver.PrintMessageReceived();
                Console.WriteLine("Press x to exit, Press ENTER to try again");
                control = Console.ReadLine();
            }
        }
    }
}
