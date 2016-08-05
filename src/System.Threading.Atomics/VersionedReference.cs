namespace System.Threading.Atomic
{
    public struct VersionedReference<TRef> : IEquatable<VersionedReference<TRef>>
        where TRef : class
    {
        public TRef Reference { get; set; }
        public long Version { get; set; }

        public bool Equals(VersionedReference<TRef> other)
            => ReferenceEquals(Reference, other.Reference)
                && Version == other.Version;

        public static implicit operator TRef(VersionedReference<TRef> atom) => atom.Reference;
    }

    public static class AtomicVersionedReferenceExtensions
    {
        public static VersionedReference<TRef> Increment<TAtomic, TRef>(
                        this TAtomic atom)
            where TAtomic : IAtomic<VersionedReference<TRef>>
            where TRef : class;
        public static VersionedReference<TRef> Increment<TAtomic, TRef>(
                        this TAtomic atom,
                        TRef expectedReference)
            where TAtomic : IAtomic<VersionedReference<TRef>>
            where TRef : class;
        public static VersionedReference<TRef> UpdateIncrement<TAtomic, TRef>(
                        this TAtomic atom,
                        VersionedReference<TRef> newRefExpectedVersion)
            where TAtomic : IAtomic<VersionedReference<TRef>>
            where TRef : class;
    }
}
