using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Asymmetric_Cipher
{
    class AuxCalc
    {
        // Calculo do ModInverse  d=(e^-1) (mod phi(n)) através de Extended Euclid Algorithm Baseado em https://tutorialspoint.dev/algorithm/mathematical-algorithms/multiplicative-inverse-under-modulo-m
        // Referencias sobre Extended Euclid Algorithm, teoria -> http://e-maxx.ru/algo/reverse_element, https://en.wikipedia.org/wiki/Modular_multiplicative_inverse e manual recomendado
        // Fonte sobre equações Diofantinas http://e-maxx.ru/algo/diofant_2_equation
        // Considerando uma equação auxiliar diofantina linear de segunda ordem ex + ny = 1 = gdc(e,n) (pois e é escolhido para ser co-primo de n) , esta equação tem uma solução que pode ser encontrada usando o Extended Euclid Algorithm
        // x irá ser o valor que queremos calcular, no caso deste programa será o nosso d. 
        public BigInteger ModInverse(BigInteger e, BigInteger n)
        {                                       
            BigInteger m0 = n;                 
            BigInteger y = 0, x = 1;        

            if (n == 1)
                return 0;

            while (e > 1)
            {
               
                BigInteger q = e / n;  // q é o quociente entre e e n

                BigInteger t = n; // t passa a ser n

                
                 
                n = e % n; // n passa a ser o resto
                e = t;     // e passa a ser t que era n anteriormente
                t = y;     // e t passa a ser y

                // Atualiza x e y para a nova iteração  
                y = x - q * y;  
                x = t;
            }

            // Torna x positivo 
            if (x < 0)
            {
                //Console.WriteLine("x deu negativo");
                x += m0;
            }

            return x;
        }

        //Função que escolhe o primeiro 'e' relativamente primo a  
        public int Select_e(BigInteger fi,BigInteger n)
        {
           
            for (int e=4 ; e < n; e++)          // e=4 pois o RSA torna-se vulneravel a um simples ataque com e muito baixo.
            {
                if (gcd(fi, e) == 1 )       //
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
        public int gcd(BigInteger a,BigInteger b)
        {
            while ( a!=0 && b!=0)
            {
                if (a > b)
                    a = a % b;
                else
                    b = b % a;

            }
            return (int)(a | b);
        }

        // Função que devolve fi de n (Euler Totient function)
        public BigInteger Phi(BigInteger p, BigInteger q)
        {
            return BigInteger.Multiply(BigInteger.Subtract(p,1),BigInteger.Subtract(q,1));
        }   

        // função para contar digitos
        public BigInteger CountDigits(BigInteger x)
        {
            BigInteger count = 0;
            while (x > 0)
            {
                x = BigInteger.Divide(x, 10);
                count++;
            }
            return count;
        }
    }
}
