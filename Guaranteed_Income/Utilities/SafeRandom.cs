using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

//got code from https://blogs.msdn.microsoft.com/pfxteam/2009/02/19/getting-random-numbers-in-a-thread-safe-way/
namespace Guaranteed_Income.Utilities
{
    /// <summary>
    /// Thread-safe uniform random generator implementation.
    /// </summary>
    public static class SafeRandom
    {
        private static RNGCryptoServiceProvider _global =
        new RNGCryptoServiceProvider();
        [ThreadStatic]
        private static Random _local;

        public static int Next()
        {
            Random inst = _local;
            if (inst == null)
            {
                byte[] buffer = new byte[4];
                _global.GetBytes(buffer);
                _local = inst = new Random(
                    BitConverter.ToInt32(buffer, 0));
            }
            return inst.Next();
        }
        public static double NextDouble(double minValue, double maxValue)
        {
            Random inst = _local;
            if (inst == null)
            {
                byte[] buffer = new byte[4];
                _global.GetBytes(buffer);
                _local = inst = new Random(
                    BitConverter.ToInt32(buffer, 0));
            }
            var r = inst.NextDouble() * (maxValue - minValue);
            return minValue + r;
        }
    }
}
