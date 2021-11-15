using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Asymmetric_Cipher
{
    class PrimeCalc
    {
        // Calculo do ModInverse  d=(e^-1) (mod phi(n))  através de Extended Euclid Algorithm Baseado em https://tutorialspoint.dev/algorithm/mathematical-algorithms/multiplicative-inverse-under-modulo-m
        public BigInteger ModInverse(BigInteger e, BigInteger n)
        {
            BigInteger m0 = n;
            BigInteger y = 0, x = 1;

            if (n == 1)
                return 0;

            while (e > 1)
            {
                // q é o quociente
                BigInteger q = e / n;

                BigInteger t = n;

                // n passa a ser o resto
                 
                n = e % n;
                e = t;
                t = y;

                // Atualiza x e y 
                y = x - q * y;
                x = t;
            }

            // Torna x positivo 
            if (x < 0)
                x += m0;

            return x;
        }

        //Função que escolhe o primeiro 'e' relativamente primo a  
        public int Select_e(BigInteger n)
        {
           
            for (int e=4 ; e < n; e++)          // e=4 pois o RSA torna-se vulneravel a um simples ataque com e muito baixo.
            {
                if (gcd(Phi(n), e) == 1 )       //
                {
                    return e;
                }
            }
            return -1;
        }


        //Função que devolve um número primo aleatório até max
        public int GetRandomPrime(int max)
        {
            return PickRandomPrime(GenPrimeArrayMaxN(max),max);
        }

        //Função que escolhe um número primo aleatório de uma lista de números primos até n
        public int PickRandomPrime(List<int> primes,int n)
        {
            Random rand = new Random();
            int RandomIndex,RandomPrime;

            RandomIndex = rand.Next(0, primes.Count);
            RandomPrime = primes[RandomIndex];

            return RandomPrime;
        }

        //Função que cria uma lista de números primos
        public List<int> GenPrimeArrayMaxN(int n)
        {
            List<int> primes = new();
            for (int i = 3; i <= n; i+=2)
            {
                if (IsPrime(i))
                    primes.Add(i);
            }

            Console.WriteLine();
            return primes;
           
        }

        // Função que verifica se um número é primo
        private bool IsPrime(int num)
        {
            if (num <= 1)
                return false;
            if (num == 2)           // Não vamos considerar o 2
                return false;
            if (num % 2 == 0)
                return false;

            for (int i = 2; i < (num/2); i++)
            {
                if (num % i == 0)
                    return false;
            }

            return true;
        }

        // Função que devolve o Máximo Divisor Comum 
        public int gcd(BigInteger a,BigInteger n)
        {
            if (a == 0)
                return (int)n;
            return gcd(n % a, a);
        }




        // Função que devolve fi de n (Euler Totient function)
        public int Phi(BigInteger n)
        {
            int result = 1;
            for (int i = 2; i < n; i++)
            {
                if (gcd(i, n) == 1)
                    result++;
            }
            return result;
        }
    

    
    }
}
