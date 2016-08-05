namespace System.Threading.Atomic
{
    public static class AtomicBoolExtensions
    {
        public static bool TestAndSet<TAtomic>(this Atomic<bool> atom)
            where TAtomic : IAtomic<bool>
            => Change(ref atom, true);

        public static bool Clear<TAtomic>(this Atomic<bool> atom)
            where TAtomic : IAtomic<bool>
            => Change(ref atom, false);

        private unsafe static bool Change<TAtomic>(ref TAtomic atom, bool value)
            where TAtomic : IAtomic<bool>
            => atom.Transform((current, v) => v, value);
    }
}
