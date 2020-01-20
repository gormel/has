using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.View.Common
{
    internal class CollectionChangedEventArgs<T> : EventArgs
    {
        public T Elem { get; }

        public CollectionChangedEventArgs(T elem)
        {
            Elem = elem;
        }
    }

    internal class CollectionObserver<T>
    {
        private readonly IEnumerable<T> mCollection;
        public event EventHandler<CollectionChangedEventArgs<T>> Added;
        public event EventHandler<CollectionChangedEventArgs<T>> Removed;
        private List<T> mLastUpdated = new List<T>();

        public CollectionObserver(IEnumerable<T> collection)
        {
            mCollection = collection;
        }

        public void Update()
        {
            var added = mCollection.Except(mLastUpdated);
            foreach (var elem in added)
                Added?.Invoke(this, new CollectionChangedEventArgs<T>(elem));

            var removed = mLastUpdated.Except(mCollection);
            foreach (var elem in removed)
                Removed?.Invoke(this, new CollectionChangedEventArgs<T>(elem));

            mLastUpdated.Clear();
            mLastUpdated.AddRange(mCollection);
        }
    }
}