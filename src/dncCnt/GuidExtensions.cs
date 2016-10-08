using System;
using System.IO;

namespace dncCnt
{
    public static class GuidExtensions
    {
        private static readonly Random R = new Random();
        private static readonly Guid EmptyGuid = new Guid("00000000-0000-0000-0000-000000000000");
        public static Guid NewIfEmpty(this Guid g)
        {
            if (g.Equals(Guid.Empty))
            {
                var b = new byte[16];
                R.NextBytes(b);
                g = new Guid(b);
            }
            if (g.Equals(Guid.Empty))
            {
                Console.WriteLine("Entropy problem?");
                g = Guid.NewGuid();
            }
            if (g.Equals(Guid.Empty)) throw new InvalidDataException($"New Guid's are empty! {g}");
            if (g == EmptyGuid) throw new InvalidDataException($"Empty Guid's ane NOT empty! {Guid.Empty}");
            if (Guid.Empty != EmptyGuid) throw new Exception("1+1=23 -- The word as you know it has ended!");
            return g;
        }
    }
}