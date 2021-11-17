using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Asymmetric_Cipher
{
    class KeyPair
    {
        private BigInteger k, n;

        public void setPair(BigInteger a, BigInteger b)
        {
            this.k = a;
            this.n = b;
        }

        public BigInteger getK() { return this.k; }
        public BigInteger getN() { return this.n; }
    }
}
