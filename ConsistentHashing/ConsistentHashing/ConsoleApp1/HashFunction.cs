using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public static class HashFunction
    {
        public static long createHash(string input)
        {
            long hash = 7;
            for (int i = 0; i < input.Length; i++)
            {
                hash = hash * 31 + input[i];
            }

            return hash;
        }
    }
}
