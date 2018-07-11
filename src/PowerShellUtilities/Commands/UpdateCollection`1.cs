using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Net;
using System.Threading;
using PowerShellUtilities.Utilities;
using WUApiLib;

namespace PowerShellUtilities.Commands
{


    public class UpdateCollection<TUpdate> : UpdateCollection, IList<TUpdate>, IReadOnlyList<TUpdate>
        where TUpdate : IUpdate
    {
        private readonly UpdateCollection _collection;

        public UpdateCollection(UpdateCollection collection)
        {
            _collection = collection;
        }

        int IUpdateCollection.Add(IUpdate value)
        {
            return _collection.Add(value);
        }

        public void Clear()
        {
            _collection.Clear();
        }

        UpdateCollection IUpdateCollection.Copy()
        {
            return _collection.Copy();
        }

        void IUpdateCollection.Insert(int index, IUpdate value)
        {
            _collection.Insert(index, value);
        }

        public void RemoveAt(int index)
        {
            _collection.RemoveAt(index);
        }

        IUpdate IUpdateCollection.this[int index]
        {
            get
            {
                return _collection[index];
            }
            set
            {
                _collection[index] = value;
            }
        }

        public int Count { get; }
        public bool ReadOnly => false;

        public bool IsReadOnly { get; }

        TUpdate IList<TUpdate>.this[int index]
        {
            get
            {
                return this[index];
            }
            set
            {
                this[index] = value;
            }
        }

        public TUpdate this[int index]
        {
            get
            {
                return (TUpdate)_collection[index];
            }
            set
            {
                _collection[index] = value;
            }
        }

        IEnumerator IUpdateCollection.GetEnumerator()
        {
            return _collection.GetEnumerator();
        }


        public void Insert(int index, TUpdate item)
        {
            _collection.Insert(index, item);
        }


        public void Add(TUpdate item)
        {
            _collection.Add(item);
        }


        public bool Contains(TUpdate item)
        {
            foreach (TUpdate update in this)
            {
                if (update.Equals(item))
                {
                    return true;
                }
            }
            return false;
        }

        public List<TUpdate> ToList()
        {
            List<TUpdate> list = new List<TUpdate>();
            foreach (TUpdate item in this)
            {
                list.Add(item);
            }
            return list;
        }

        public void CopyTo(TUpdate[] array, int arrayIndex)
        {
            ToList().CopyTo(array, arrayIndex);
        }

        public int IndexOf(TUpdate item)
        {
            for (int i = 0; i < Count; i++)
            {
                TUpdate current = this[i];
                if (current.Equals(item))
                {
                    return i;
                }
            }

            return -1;
        }

        public bool Remove(TUpdate item)
        {
            int index = IndexOf(item);
            if (index != -1)
            {
                RemoveAt(index);
                return true;
            }
            return false;
        }


        public Reversed Reverse() => new Reversed(this);

        public Enumerator GetEnumerator() => new Enumerator(this);

        IEnumerator<TUpdate> IEnumerable<TUpdate>.GetEnumerator() => new EnumeratorImpl(this);

        IEnumerator IEnumerable.GetEnumerator() => new EnumeratorImpl(this);

        public struct Enumerator
        {
            private readonly UpdateCollection<TUpdate> _list;
            private int _index;

            internal Enumerator(UpdateCollection<TUpdate> list)
            {
                _list = list;
                _index = -1;
            }

            public bool MoveNext()
            {
                int newIndex = _index + 1;
                if (newIndex < _list.Count)
                {
                    _index = newIndex;
                    return true;
                }

                return false;
            }

            public TUpdate Current
            {
                get
                {
                    return (TUpdate)_list[_index];
                }
            }

            public void Reset()
            {
                _index = -1;
            }

            public override bool Equals(object obj)
            {
                throw new NotSupportedException();
            }

            public override int GetHashCode()
            {
                throw new NotSupportedException();
            }
        }

        private class EnumeratorImpl : IEnumerator<TUpdate>
        {
            private Enumerator _e;

            internal EnumeratorImpl(UpdateCollection<TUpdate> list)
            {
                _e = new Enumerator(list);
            }

            public bool MoveNext()
            {
                return _e.MoveNext();
            }

            public TUpdate Current
            {
                get
                {
                    return _e.Current;
                }
            }

            void IDisposable.Dispose()
            {
            }

            object IEnumerator.Current
            {
                get
                {
                    return _e.Current;
                }
            }

            void IEnumerator.Reset()
            {
                _e.Reset();
            }
        }

        public struct Reversed : IEnumerable<TUpdate>, IEquatable<Reversed>
        {
            private readonly UpdateCollection<TUpdate> _collection;
            private readonly int _count;

            internal Reversed(UpdateCollection<TUpdate> collection)
            {
                _collection = collection;
                _count = collection.Count;
            }

            public Enumerator GetEnumerator()
            {
                return new Enumerator(_collection);
            }

            IEnumerator<TUpdate> IEnumerable<TUpdate>.GetEnumerator()
            {
                if (_collection == null)
                {
                    return new List<TUpdate>.Enumerator();
                }

                return new EnumeratorImpl(_collection);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                if (_collection == null)
                {
                    return new List<TUpdate>.Enumerator();
                }

                return new EnumeratorImpl(_collection);
            }

            public bool Equals(Reversed other)
            {
                return _collection == other._collection
                    && _count == other._count;
            }

            public struct Enumerator
            {
                private readonly UpdateCollection<TUpdate> _collection;
                private readonly int _count;
                private int _index;

                internal Enumerator(UpdateCollection<TUpdate> collection)
                {
                    _collection = collection;
                    _count = _collection.Count;
                    _index = _count;
                }

                public bool MoveNext()
                {
                    return --_index >= 0;
                }

                public TUpdate Current
                {
                    get
                    {
                        return _collection[_index];
                    }
                }

                public void Reset()
                {
                    _index = _count;
                }
            }

            private class EnumeratorImpl : IEnumerator<TUpdate>
            {
                private Enumerator _enumerator;

                internal EnumeratorImpl(UpdateCollection<TUpdate> collection)
                {
                    _enumerator = new Enumerator(collection);
                }

                public TUpdate Current
                {
                    get { return _enumerator.Current; }
                }

                object IEnumerator.Current
                {
                    get { return _enumerator.Current; }
                }

                public bool MoveNext()
                {
                    return _enumerator.MoveNext();
                }

                public void Reset()
                {
                    _enumerator.Reset();
                }

                public void Dispose()
                {
                }
            }
        }
    }
}
