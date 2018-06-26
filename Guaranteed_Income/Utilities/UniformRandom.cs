using System;
using System.Security.Cryptography;

namespace Guaranteed_Income.Utilities
{
    //got code from https://www.olegtarasov.me/thread-safe-random-number-generator-with-unit-testing-support-in-c/
    // <summary>
    /// Standard .NET random generator with uniform probability distribution.
    /// Random seed is generated using high-quality RNG from <see cref="RNGCryptoServiceProvider "/>
    /// </summary>
    internal class UniformRandom : IRandom
    {
        private static readonly RNGCryptoServiceProvider _global = new RNGCryptoServiceProvider();

        private readonly Random _rnd;

        public UniformRandom()
        {
            byte[] buffer = new byte[4];
            _global.GetBytes(buffer);
            _rnd = new Random(BitConverter.ToInt32(buffer, 0));
        }

        public int Next()
        {
            return _rnd.Next();
        }

        public int Next(int maxValue)
        {
            return _rnd.Next(maxValue);
        }

        public int Next(int minValue, int maxValue)
        {
            return _rnd.Next(minValue, maxValue);
        }

        public double NextDouble()
        {
            return _rnd.NextDouble();
        }

        public double NextDouble(double minValue, double maxValue)
        {
            var r = _rnd.NextDouble() * (maxValue - minValue);
            return minValue + r;
        }
    }
}