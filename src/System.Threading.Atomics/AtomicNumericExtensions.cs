namespace System.Threading.Atomic
{
    public static class AtomicNumericExtensions
    {    // For byte, short, ushort, uint, int, long, ulong, single, double

        public static int Add<TAtomic>(this TAtomic atom, int value) where TAtomic : IAtomic<int>
            => atom.UnsafeTransform((ref int current, int inc)
               => Interlocked.Add(ref current, inc), value);
        public static long Add<TAtomic>(this TAtomic atom, long value) where TAtomic : IAtomic<long>
            => atom.UnsafeTransform( (ref long current, long inc) 
                => Interlocked.Add(ref current, inc), value);

        public static int Subtract<TAtomic>(this TAtomic atom, int value) where TAtomic : IAtomic<int>
            => atom.Add(-value);
        public static long Subtract<TAtomic>(this TAtomic atom, long value) where TAtomic : IAtomic<long>
            => atom.Add(-value);

        public static int Increment<TAtomic>(this TAtomic atom, int value = 1) where TAtomic : IAtomic<int>
            => atom.Add(value);
        public static long Increment<TAtomic>(this TAtomic atom, long value = 1) where TAtomic : IAtomic<long>
            => atom.Add(value);

        public static int Decrement<TAtomic>(this TAtomic atom, int value = -1) where TAtomic : IAtomic<int>
            => atom.Add(value);
        public static long Decrement<TAtomic>(this TAtomic atom, long value = -1) where TAtomic : IAtomic<long>
            => atom.Add(value);

        public static int CappedIncrement<TAtomic>(this TAtomic atom, int maxValue) where TAtomic : IAtomic<int>
            => atom.Transform((current) => current + 1, (c, m) => c <= m, maxValue);
        public static long CappedIncrement<TAtomic>(this TAtomic atom, long maxValue) where TAtomic : IAtomic<long>
            => atom.Transform((current) => current + 1, (c, m) => c <= m, maxValue);

        public static int CappedDecrement<TAtomic>(this TAtomic atom, int minValue) where TAtomic : IAtomic<int>
            => atom.Transform((current) => current - 1, (c, m) => c >= m, minValue);
        public static long CappedDecrement<TAtomic>(this TAtomic atom, long minValue) where TAtomic : IAtomic<long>
            => atom.Transform((current) => current - 1, (c, m) => c >= m, minValue);

        public static int And<TAtomic>(this TAtomic atom, int value) where TAtomic : IAtomic<int>
            => atom.Transform((current, v) => current & v, value);
        public static int Or<TAtomic>(this TAtomic atom, int value) where TAtomic : IAtomic<int>
            => atom.Transform((current, v) => current | v, value);
        public static int Xor<TAtomic>(this TAtomic atom, int value) where TAtomic : IAtomic<int>
            => atom.Transform((current, v) => current ^ v, value);
        public static int Not<TAtomic>(this TAtomic atom) where TAtomic : IAtomic<int>
            => atom.Transform((current) => ~current);
        //public static long Not<TAtomic>(this TAtomic atom) where TAtomic : IAtomic<long>
        //    => atom.Transform((current) => ~current);

        public static decimal Multiply<TAtomic>(this TAtomic atom, decimal value)
   where TAtomic : IAtomic<decimal>
        {
            return atom.Transform((current, m) => current * m, value);
        }
        
        //public static int Multiply(this Atomic<int> atom, int value);
        //public static int Divide(this Atomic<int> atom, int value);

    }
}
