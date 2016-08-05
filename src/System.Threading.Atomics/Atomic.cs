namespace System.Threading.Atomic
{
    public class Atomic<T> : IAtomic<T> where T : struct, IEquatable<T>
    {
        private ValueAtomic<T> _atom;
        public static bool IsLockFree => ValueAtomic<T>.IsLockFree;

        Atomic(T value)
        {
            _atom = new ValueAtomic<T>(value);
        }

        public T Read()
            => _atom.Read();
        public T Read(MemoryOrder order)
            => _atom.Read(order);
        public void Write(T value)
            => _atom.Write(value);
        public void Write(T value, MemoryOrder order)
            => _atom.Write(value, order);
        public T Exchange(T value)
            => _atom.Exchange(value);
        public T Exchange(T value, MemoryOrder order)
            => _atom.Exchange(value, order);
        public bool CompareExchangeWeak(T value, T comperand)
            => _atom.CompareExchangeWeak(value, comperand);
        public bool CompareExchangeWeak(T value, T comperand, MemoryOrder order)
            => _atom.CompareExchangeWeak(value, comperand, order);
        public bool CompareExchangeStrong(T value, T comperand)
            => _atom.CompareExchangeStrong(value, comperand);
        public bool CompareExchangeStrong(T value, T comperand, MemoryOrder order)
            => _atom.CompareExchangeStrong(value, comperand, order);
        public unsafe T UnsafeTransform(AtomicTransform<T> transformation)
            => _atom.UnsafeTransform(transformation);
        public unsafe T UnsafeTransform(AtomicTransformParam<T> transformation, T val)
            => _atom.UnsafeTransform(transformation, val);
        public T Transform(Func<T, T> transformation)
            => _atom.Transform(transformation);
        public T Transform<TState>(Func<T, TState, T> transformation, TState state)
        => _atom.Transform(transformation, state);
        public T Transform(Func<T, T> transformation, Func<T, bool> condition)
            => _atom.Transform(transformation, condition);
        public T Transform<TState>(Func<T, T> transformation, Func<T, TState, bool> condition, TState state)
            => _atom.Transform(transformation, condition, state);
        public T Transform(Func<T, T> transformation, T comperand)
            => _atom.Transform(transformation, comperand);
        public T Transform<TState>(Func<T, TState, T> transformation, TState state, T comperand)
            => _atom.Transform(transformation, state, comperand);
        public T Transform<TState, TComp>(Func<T, TState, T> transformation, Func<T, TComp, bool> condition, TState state, TComp comperand)
            => _atom.Transform(transformation, condition, state, comperand);

        public static implicit operator T(Atomic<T> atom) => atom.Read();
    }
}
