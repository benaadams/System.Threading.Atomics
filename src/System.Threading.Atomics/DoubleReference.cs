namespace System.Threading.Atomic
{
    public struct DoubleReference<TRef> : IEquatable<DoubleReference<TRef>>
        where TRef : class
    {
        public TRef ReferenceLeft { get; set; }
        public TRef ReferenceRight { get; set; }

        public bool Equals(DoubleReference<TRef> other)
            => ReferenceEquals(ReferenceLeft, other.ReferenceLeft) &&
                ReferenceEquals(ReferenceRight, other.ReferenceRight);
    }

    public static class DoubleReferenceExtensions
    {
        public static DoubleReference<TRef> ExchangeLeft<TAtomic, TRef>(
                        this TAtomic atom,
                        TRef newLeft)
            where TAtomic : IAtomic<DoubleReference<TRef>>
            where TRef : class
        => atom.Transform(
                (c, v) => { c.ReferenceLeft = v; return c; }, 
                newLeft);

        public static DoubleReference<TRef> ExchangeLeft<TAtomic, TRef>(
                        this TAtomic atom,
                        TRef newLeft,
                        TRef expectedRight)
            where TAtomic : IAtomic<DoubleReference<TRef>>
            where TRef : class
        => atom.Transform(
                (c, v) => { c.ReferenceLeft = v; return c; },
                (o, v) => ReferenceEquals(o.ReferenceRight, v),
                newLeft, expectedRight);


        public static DoubleReference<TRef> ExchangeRight<TAtomic, TRef>(
                        this TAtomic atom,
                        TRef newRight)
            where TAtomic : IAtomic<DoubleReference<TRef>>
            where TRef : class
        => atom.Transform(
                (c, v) => { c.ReferenceRight = v; return c; },
                newRight);

    public static DoubleReference<TRef> ExchangeRight<TAtomic, TRef>(
                        this TAtomic atom,
                        TRef newRight,
                        TRef expectedLeft)
            where TAtomic : IAtomic<DoubleReference<TRef>> 
            where TRef : class
        => atom.Transform(
                (c, v) => { c.ReferenceRight = v; return c; },
                (o, v) => ReferenceEquals(o.ReferenceRight, v),
                newRight, expectedLeft);
    }
}
