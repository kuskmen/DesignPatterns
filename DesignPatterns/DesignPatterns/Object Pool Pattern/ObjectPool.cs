using System;
using System.Diagnostics;
using System.Threading;

namespace DesignPatterns.Object_Pool_Pattern
{
    public class ObjectPool<T> : IObjectPool<T> where T : class
    {
        private readonly Func<T> _factory;
        private T _firstItem;
        private readonly T[] _items;

        public ObjectPool(Func<T> factory) : this(factory, Environment.ProcessorCount * 2) { }
        public ObjectPool(Func<T> factory, int size)
        {
            Debug.Assert(size >= 1);
            _factory = factory;
            _items = new T[size - 1];
        }

        public virtual T Allocate()
        {
            // Try to get first object first.
            var inst = _firstItem;
            if (inst == null || inst != Interlocked.CompareExchange(ref _firstItem, null, inst))
            {
                // If first is null simply search for the next non-null object in the items.
                var items = _items;
                for (var i = 0; i < items.Length; i++)
                {
                    var inst2 = items[i];
                    // return first object that we find it is not null
                    if (inst2 != null && inst2 == Interlocked.CompareExchange(ref items[i], null, inst2))
                    {
                        return inst2;
                    }
                }
                // If we fail to find initialized object, create one ourselves.
                return _factory();
            }
            // return instance of the first object (this is not null for sure).
            return inst;
        }
        public virtual void Free(T obj)
        {
            Debug.Assert(obj != null);
            Debug.Assert(obj != _firstItem);

            // Try to return object as first first.
            if (_firstItem == null)
            {
                _firstItem = obj;
            }
            // If we fail we return it to the next best place.
            else
            {
                var items = _items;
                for (var i = 0; i < items.Length; i++)
                {
                    if (items[i] == null)
                    {
                        items[i] = obj;
                        break;
                    }
                }
            }
        }
    }

    public interface IObjectPool<T>
    {
        /// <summary>
        ///  Produces an instance of <see cref="T"/>.
        /// </summary>
        /// <returns> Newly created object of <see cref="T"/>. </returns>
        T Allocate();

        /// <summary>
        ///  Returns object to the pool.
        /// </summary>
        /// <param name="obj"> Object to return. </param>
        void Free(T obj);
    }
}
