namespace System.Threading.Atomic
{
    public struct TaggedReference<TRef, TTag> : IEquatable<TaggedReference<TRef, TTag>>
        where TRef : class
        where TTag : struct, IEquatable<TTag>
    {
        public TRef Reference { get; set; }
        public TTag Tag { get; set; }
        public bool Equals(TaggedReference<TRef, TTag> other)
            => ReferenceEquals(Reference, other.Reference)
                && Tag.Equals(other.Tag);

        public static implicit operator TRef(TaggedReference<TRef, TTag> atom) => atom.Reference;
    }

    public static class AtomicTaggedReferenceExtensions
    {
        public static TAtomic Update<TAtomic, TRef, TTag>(
                        this TAtomic atom,
                        TAtomic newTaggedReference,
                        TRef expectedReference)
                    where TAtomic : IAtomic<TaggedReference<TRef, TTag>>
                    where TRef : class
                    where TTag : struct, IEquatable<TTag>;
        public static TAtomic Update<TAtomic, TRef, TTag>(
                        this TAtomic atom,
                        TRef newReference,
                        TRef expectedReference)
                    where TAtomic : IAtomic<TaggedReference<TRef, TTag>>
                    where TRef : class
                    where TTag : struct, IEquatable<TTag>;
        public static TAtomic Update<TAtomic, TRef, TTag>(
                        this TAtomic atom,
                        TTag newTag,
                        TRef expectedReference)
                    where TAtomic : IAtomic<TaggedReference<TRef, TTag>>
                    where TRef : class
                    where TTag : struct, IEquatable<TTag>;
        public static TAtomic Update<TAtomic, TRef, TTag>(
                        this TAtomic atom,
                        TAtomic newTaggedReference,
                        TTag expectedTag)
                    where TAtomic : IAtomic<TaggedReference<TRef, TTag>>
                    where TRef : class
                    where TTag : struct, IEquatable<TTag>;
        public static TAtomic Update<TAtomic, TRef, TTag>(
                        this TAtomic atom,
                        TRef newReference,
                        TTag expectedTag)
                    where TAtomic : IAtomic<TaggedReference<TRef, TTag>>
                    where TRef : class
                    where TTag : struct, IEquatable<TTag>;
        public static TAtomic Update<TAtomic, TRef, TTag>(
                        this TAtomic atom,
                        TTag newTag,
                        TTag expectedTag)
                    where TAtomic : IAtomic<TaggedReference<TRef, TTag>>
                    where TRef : class
                    where TTag : struct, IEquatable<TTag>;
        public static TAtomic Update<TAtomic, TRef, TTag>(
                        this TAtomic atom,
                        TRef newReference,
                        TAtomic expectedTaggedReference)
                    where TAtomic : IAtomic<TaggedReference<TRef, TTag>>
                    where TRef : class
                    where TTag : struct, IEquatable<TTag>;
        public static TAtomic Update<TAtomic, TRef, TTag>(
                        this TAtomic atom,
                        TTag newTag,
                        TAtomic expectedTaggedReference)
                    where TAtomic : IAtomic<TaggedReference<TRef, TTag>>
                    where TRef : class
                    where TTag : struct, IEquatable<TTag>;
        // essentially CompareExchange
        public static TAtomic Update<TAtomic, TRef, TTag>(
                        this TAtomic atom,
                        TAtomic newTaggedReference,
                        TAtomic expectedTaggedReference)
                    where TAtomic : IAtomic<TaggedReference<TRef, TTag>>
                    where TRef : class
                    where TTag : struct, IEquatable<TTag>;
    }
}
