namespace System.Threading.Atomic
{
    public struct FlaggedReference<TRef> : IEquatable<FlaggedReference<TRef>>
    where TRef : class
    {
        public TRef Reference { get; set; }
        public bool Flag { get; set; }
        public bool Equals(FlaggedReference<TRef> other)
            => ReferenceEquals(Reference, other.Reference)
                && Flag == other.Flag;

        public static implicit operator TRef(FlaggedReference<TRef> atom) => atom.Reference;
    }

    public static class AtomicFlaggedReferenceExtensions
    {
        public static bool TestAndSet<TAtomic, TRef>(this TAtomic atom)
                    where TAtomic : IAtomic<FlaggedReference<TRef>>
                    where TRef : class;
        public static bool TestAndSet<TAtomic, TRef>(
                            this TAtomic atom,
                            TRef expectedReference)
                    where TAtomic : IAtomic<FlaggedReference<TRef>>
                    where TRef : class;
        public static bool Clear<TAtomic, TRef>(this TAtomic atom)
                    where TAtomic : IAtomic<FlaggedReference<TRef>>
                    where TRef : class;
        public static bool Clear<TAtomic, TRef>(
                            this TAtomic atom,
                            TRef expectedReference)
                    where TAtomic : IAtomic<FlaggedReference<TRef>>
                    where TRef : class;
    }
}
