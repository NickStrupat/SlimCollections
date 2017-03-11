using System;
using System.Collections;
using System.Collections.Generic;

namespace SlimCollections
{
    public struct SlimList<T> : IList<T>, IReadOnlyList<T>, IEnumerator<T>
    {
        private const Int32 StackItemCount = 10;
        private Int32 count;

        private T _0;
        private T _1;
        private T _2;
        private T _3;
        private T _4;
        private T _5;
        private T _6;
        private T _7;
        private T _8;
        private T _9;

        private List<T> list;

        public T this[Int32 index] { get => GetItem(index); set => SetItem(index, value); }

        public Int32 Count => count;

        public Boolean IsReadOnly => false;

        public void Add(T item)
        {
            switch (count)
            {
                case 0: _0 = item; break;
                case 1: _1 = item; break;
                case 2: _2 = item; break;
                case 3: _3 = item; break;
                case 4: _4 = item; break;
                case 5: _5 = item; break;
                case 6: _6 = item; break;
                case 7: _7 = item; break;
                case 8: _8 = item; break;
                case 9: _9 = item; break;
                default:
                    if (list == null)
                        list = new List<T>(8);
                    list.Add(item);
                    break;
            }
            ++count;
        }

        private T GetItem(Int32 index)
        {
            if (index < 0 || index >= count)
                throw new ArgumentOutOfRangeException(nameof(index));
            switch (index)
            {
                case 0: return _0;
                case 1: return _1;
                case 2: return _2;
                case 3: return _3;
                case 4: return _4;
                case 5: return _5;
                case 6: return _6;
                case 7: return _7;
                case 8: return _8;
                case 9: return _9;
                default: return list[index - StackItemCount];
            }
        }

        private void SetItem(Int32 index, T value)
        {
            if (index < 0 || index >= count)
                throw new ArgumentOutOfRangeException(nameof(index));
            switch (index)
            {
                case 0: _0 = value; break;
                case 1: _1 = value; break;
                case 2: _2 = value; break;
                case 3: _3 = value; break;
                case 4: _4 = value; break;
                case 5: _5 = value; break;
                case 6: _6 = value; break;
                case 7: _7 = value; break;
                case 8: _8 = value; break;
                case 9: _9 = value; break;
                default: list[index - StackItemCount] = value; break;
            }
        }

        public void Clear()
        {
            count = 0;
            list = null;
        }

        private static readonly EqualityComparer<T> DefaultEqualityComparer = EqualityComparer<T>.Default;

        public Int32 IndexOf(T item)
        {
            for (var i = 0; i != Count; i++)
                if (DefaultEqualityComparer.Equals(this[i], item))
                    return i;
            return -1;
        }

        public Boolean Contains(T item) => IndexOf(item) != -1;

        public void CopyTo(T[] array, Int32 arrayIndex)
        {
            if (array == null)
                throw new NullReferenceException(nameof(array));
            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            if (count > array.Length - arrayIndex)
                throw new ArgumentException();
            for (var i = 0; i != count; i++)
                array[i + arrayIndex] = this[i];
        }

        public void Insert(Int32 index, T item)
        {
            if (index < 0 || index > count)
                throw new ArgumentOutOfRangeException(nameof(index));
            if (index == count)
            {
                Add(item);
                return;
            }
            Add(this[count - 1]);
            for (var i = index; i != count - 2; i++)
                this[i + 1] = this[i];
            this[index] = item;
        }

        public Boolean Remove(T item)
        {
            var index = IndexOf(item);
            if (index == -1)
                return false;
            RemoveAt(index);
            return true;
        }

        public void RemoveAt(Int32 index)
        {
            if (index < 0 || index >= count)
                throw new IndexOutOfRangeException();
            for (var i = index; i != count - 1; i++)
                this[i] = this[i + 1];
            if (list != null)
                RemoveLast();
            --count;
        }

        private void RemoveLast()
        {
            if (list.Count > 0)
                list.RemoveAt(list.Count - 1);
            if (list.Count == 0)
                list = null;
        }

        public SlimList<T> GetEnumerator()            => this;
        IEnumerator<T> IEnumerable<T>.GetEnumerator() => this;
        IEnumerator IEnumerable.GetEnumerator()       => this;

        public SlimList<T> Reverse()
        {
            reverse = true;
            return this;
        }

        private Int32 index;
        private Boolean reverse;

        public T Current => GetItem(reverse ? (count - index) : (index - 1));
        Object IEnumerator.Current => Current;
        public Boolean MoveNext() => index++ < count;
        public void Reset() {}
        void IDisposable.Dispose() {
            index = 0;
            reverse = false;
        }
    }
}
