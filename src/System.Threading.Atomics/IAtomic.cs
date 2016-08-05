namespace System.Threading.Atomic
{
    public delegate T AtomicTransform<T>(ref T input);
    public delegate T AtomicTransformParam<T>(ref T input, T val);

    public interface IAtomic<T> where T : struct, IEquatable<T>
    {
        T Read();
        T Read(MemoryOrder order);
        void Write(T value);
        void Write(T value, MemoryOrder order);

        T Exchange(T value);
        T Exchange(T value, MemoryOrder order);

        bool CompareExchangeWeak(T value, T comperand);
        bool CompareExchangeWeak(T value, T comperand, MemoryOrder order);

        bool CompareExchangeStrong(T value, T comperand);
        bool CompareExchangeStrong(T value, T comperand, MemoryOrder order);

        // MemoryOrder variants skipped for brevity

        // Unsafe transformations, bypass the atomicity
        T UnsafeTransform(AtomicTransform<T> transformation);
        T UnsafeTransform(AtomicTransformParam<T> transformation, T val);

        // Atomic transformations, Func should be pure and repeatable in case of retry

        // Pure transform
        T Transform(Func<T, T> transformation);
        T Transform<TState>(Func<T, TState, T> transformation, TState state);

        // Conditional transforms e.g. Increment but only while < N
        T Transform(Func<T, T> transformation, Func<T, bool> condition);
        T Transform<TComp>(Func<T, T> transformation, Func<T, TComp, bool> condition, TComp state);
        T Transform<TState, TComp>(Func<T, TState, T> transformation, Func<T, TComp, bool> condition, TState state, TComp comperand);

        // Same data transform, apply if value is what is expected
        T Transform(Func<T, T> transformation, T comperand);
        T Transform<TState>(Func<T, TState, T> transformation, TState state, T comperand);
    }
}
