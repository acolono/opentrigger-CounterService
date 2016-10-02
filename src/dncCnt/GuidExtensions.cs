using System;

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
            }
            return g;
        }
    }
}