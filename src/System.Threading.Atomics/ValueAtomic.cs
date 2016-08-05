using System.Runtime.CompilerServices;

namespace System.Threading.Atomic
{
    public class ValueAtomic<T> : IAtomic<T> where T : struct, IEquatable<T>
    {
        private T _data;
        // allocated if not supported lock-free natively
        private object _lock;

        private object Lock => _lock ?? InitalizeLock();

        [MethodImpl(MethodImplOptions.NoInlining)]
        private object InitalizeLock() => Interlocked.CompareExchange(ref _lock, new object(), null) ?? _lock;

        public static bool IsLockFree { get; } = false;

        public ValueAtomic(T value)
        {
            _data = value;
            _lock = IsLockFree ? null : new object();
        }

        public T Read()
        {
            lock (Lock)
            {
                return _data;
            }
        }

        public T Read(MemoryOrder order) => Read();

        public void Write(T value)
        {
            lock (Lock)
            {
                _data = value;
            }
        }

        public void Write(T value, MemoryOrder order) => Write(value);
        public T Exchange(T value)
        {
            lock (Lock)
            {
                var current = _data;
                _data = value;
                return current;
            }
        }

        public T Exchange(T value, MemoryOrder order) => Exchange(value);
        public bool CompareExchangeStrong(T value, T comperand)
        {
            lock (Lock)
            {
                var current = _data;
                var same = current.Equals(comperand);
                if (same)
                {
                    _data = value;
                }

                return same;
            }

        }
        public bool CompareExchangeStrong(T value, T comperand, MemoryOrder order) => CompareExchangeStrong(value, comperand);
        public bool CompareExchangeWeak(T value, T comperand) => CompareExchangeStrong(value, comperand);
        public bool CompareExchangeWeak(T value, T comperand, MemoryOrder order) => CompareExchangeStrong(value, comperand);
        public unsafe T UnsafeTransform(AtomicTransform<T> transformation)
            => transformation(ref _data);
        public unsafe T UnsafeTransform(AtomicTransformParam<T> transformation, T val)
            => transformation(ref _data, val);
        public T Transform(Func<T, T> transformation)
        {
            lock (Lock)
            {
                _data = transformation(_data);
                return _data;
            }
        }
        public T Transform<TState>(Func<T, TState, T> transformation, TState state)
        {
            lock (Lock)
            {
                _data = transformation(_data, state);
                return _data;
            }
        }
        public T Transform(Func<T, T> transformation, Func<T, bool> condition)
        {
            lock (Lock)
            {
                var changed = transformation(_data);
                if (condition(changed))
                {
                    _data = changed;
                }
                return _data;
            }
        }
        public T Transform<TState>(Func<T, T> transformation, Func<T, TState, bool> condition, TState state)
        {
            //T current = Read();
            //while (condition(current, state))
            //{
            //    T next = transformation(current);
            //    T prev = Interlocked.CompareExchange(ref _data, next, current);
            //    if (prev.Equals(current))
            //    {
            //        return next;
            //    }
            //    current = prev;
            //}
            lock (Lock)
            {
                var changed = transformation(_data);
                if (condition(changed, state))
                {
                    _data = changed;
                }
                return _data;
            }
        }
        public T Transform(Func<T, T> transformation, T comperand)
        {
            lock (Lock)
            {
                if (_data.Equals(comperand))
                {
                    _data = transformation(_data);
                }
                return _data;
            }
        }
        public T Transform<TState>(Func<T, TState, T> transformation, TState state, T comperand)
        {
            lock (Lock)
            {
                if (_data.Equals(comperand))
                {
                    _data = transformation(_data, state);
                }
                return _data;
            }
        }
        public T Transform<TState, TComp>(Func<T, TState, T> transformation, Func<T, TComp, bool> condition, TState state, TComp comperand)
        {
            lock (Lock)
            {
                if (condition(_data, comperand))
                {
                    _data = transformation(_data, state);
                }
                return _data;
            }
        }

        public static implicit operator T(ValueAtomic<T> atom) => atom.Read();
    }
}
