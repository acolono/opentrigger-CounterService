using System;
using System.IO;

namespace dncCnt
{
    public static class GuidExtensions
    {
        private static readonly Random _r = new Random();
        public static Guid NewIfEmpty(this Guid g)
        {
            if (g.Equals(Guid.Empty))
            {
                var b = new byte[16];
                _r.NextBytes(b);
                g = new Guid(b);
            }
            if (g.Equals(Guid.Empty))
            {
                Console.WriteLine("Entropy problem?");
                g = Guid.NewGuid();
            }
            if (g.Equals(Guid.Empty)) throw new InvalidDataException($"New Guid's are Empty! {g}");
            return g;
        }
    }
}