using System;
using System.Collections;
using System.Collections.Generic;

namespace SlimCollections
{
    public struct SlimDeque<T> : IEnumerable<T>, IEnumerator<T>
    {
        private SlimList<T> front;
        private SlimList<T> back;

        public Int32 Count => front.Count + back.Count;

        public void Clear()
        {
            front.Clear();
            back.Clear();
        }

        public void EnqueueFront(T value) => front.Add(value);
        public void EnqueueBack(T value) => back.Add(value);

        public T DequeueFront() => Dequeue(ref front, ref back);
        public T DequeueBack() => Dequeue(ref back, ref front);

        private T Dequeue(ref SlimList<T> a, ref SlimList<T> b)
        {
            if (a.Count > 0)
                return PopLast(ref a);
            if (b.Count > 0)
                return PopFirst(ref b);
            throw new EmptySlimDequeException();
        }

        private static T PopFirst(ref SlimList<T> x) => Pop(ref x, 0);
        private static T PopLast(ref SlimList<T> x) => Pop(ref x, x.Count - 1);

        private static T Pop(ref SlimList<T> slimList, Int32 index)
        {
            var result = slimList[index];
            slimList.RemoveAt(index);
            return result;
        }

        public T PeekFront() => TryPeekFront(out T value) ? value : throw new EmptySlimDequeException();
        public T PeekBack() => TryPeekBack(out T value) ? value : throw new EmptySlimDequeException();

        public bool TryPeekFront(out T value) => TryPeekLast(ref front, out value) || TryPeekFirst(ref back, out value);
        public bool TryPeekBack(out T value) => TryPeekLast(ref back, out value) || TryPeekFirst(ref front, out value);

        private static Boolean TryPeekFirst(ref SlimList<T> x, out T value) => TryPeek(ref x, out value, 0);
        private static Boolean TryPeekLast(ref SlimList<T> x, out T value) => TryPeek(ref x, out value, x.Count - 1);

        private static Boolean TryPeek(ref SlimList<T> slimList, out T value, Int32 index)
        {
            if (slimList.Count > 0)
                return TryResult(true, out value, slimList[index]);
            else
                return TryResult(false, out value, default(T));
        }

        private static Boolean TryResult<TResult>(Boolean @return, out TResult result, TResult value)
        {
            result = value;
            return @return;
        }


        public SlimDeque<T> GetEnumerator() => this;
        IEnumerator<T> IEnumerable<T>.GetEnumerator() => this;
        IEnumerator IEnumerable.GetEnumerator() => this;

        public SlimDeque<T> Reverse()
        {
            reverse = true;
            return this;
        }

        private Int32 index;
        private Boolean reverse;

        public T Current => GetItem(reverse ? (Count - index) : (index - 1));

        Object IEnumerator.Current => Current;
        public Boolean MoveNext() => index++ < Count;
        public void Reset() {}
        void IDisposable.Dispose() {
            index = 0;
            reverse = false;
        }

        private T GetItem(Int32 index)
        {
            if (index < front.Count)
                return front[front.Count - index - 1];
            else
                return back[index - front.Count];
        }
    }

    public class EmptySlimDequeException : InvalidOperationException {
        public EmptySlimDequeException() : base($"The {nameof(SlimDeque<Object>)} is empty.") {}
    }
}
