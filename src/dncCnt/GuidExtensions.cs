using System;
using System.IO;

namespace dncCnt
{
    public static class GuidExtensions
    {
        private static Random _r = new Random();
        public static Guid NewIfEmpty(this Guid g)
        {
            if (g.Equals(Guid.Empty))
            {
                var b = new byte[16];
                _r.NextBytes(b);
                g = new Guid(b);

                if (g.Equals(Guid.Empty))
                {
                    Console.WriteLine($"Entropy problem? b={BitConverter.ToString(b)} g={g}");
                    g = Guid.NewGuid();
                    _r = new Random();
                }
            }
            if (g.Equals(Guid.Empty)) throw new InvalidDataException($"New Guid's are empty! {g}");
            return g;
        }
    }
}