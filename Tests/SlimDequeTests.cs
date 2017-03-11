using SlimCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Tests
{
    public class SlimDequeTests
    {
        [Fact]
        public void EmptySlimDequeExceptionTests()
        {
            var slimDeque = new SlimDeque<Int32>();
            Assert.Throws<EmptySlimDequeException>(() => slimDeque.DequeueFront());
            Assert.ThrowsAny<InvalidOperationException>(() => slimDeque.DequeueFront());
            Assert.Throws<EmptySlimDequeException>(() => slimDeque.DequeueBack());
            Assert.ThrowsAny<InvalidOperationException>(() => slimDeque.DequeueBack());

            slimDeque = new SlimDeque<Int32>();
            slimDeque.EnqueueFront(1);
            Assert.Equal(1, slimDeque.DequeueFront());

            slimDeque = new SlimDeque<Int32>();
            slimDeque.EnqueueBack(1);
            Assert.Equal(1, slimDeque.DequeueBack());
        }

        [Fact]
        public void EnumerateTest()
        {
            var slimDeque = new SlimDeque<Int32>();
            var frontNumbers = Enumerable.Range(0, 15);
            var backNumbers = Enumerable.Range(42, 15);
            foreach (var frontNumber in frontNumbers)
                slimDeque.EnqueueFront(frontNumber);
            foreach (var backNumber in backNumbers)
                slimDeque.EnqueueBack(backNumber);
            var enumerateNumbers = frontNumbers.Reverse().Concat(backNumbers);
            var slimDequeEnumerated = new List<Int32>(slimDeque.Count);
            foreach (var x in slimDeque)
                slimDequeEnumerated.Add(x);
            Assert.True(enumerateNumbers.SequenceEqual(slimDequeEnumerated));
            var slimDequeReverseEnumerated = new List<Int32>(slimDeque.Count);
            foreach (var x in slimDeque.Reverse())
                slimDequeReverseEnumerated.Add(x);
            Assert.True(enumerateNumbers.Reverse().SequenceEqual(slimDequeReverseEnumerated));
        }

        [Fact]
        public void FullTests()
        {
            foreach (var action in new Action<Int32[], Int32[]>[] { Helper })
                for (var i = 0; i != 15; i++)
                    for (var j = 0; j != 15; j++)
                        action(Enumerable.Range(0, i).ToArray(), Enumerable.Range(0, j).ToArray());
        }

        private static void Helper(Int32[] a, Int32[] b)
        {
            var slimDeque = new SlimDeque<Int32>();

            for (var i = 0; i != a.Length; i++)
            {
                slimDeque.EnqueueFront(a[i]);
                Assert.Equal(i + 1, slimDeque.Count);
            }

            for (var i = 0; i != b.Length; i++)
            {
                slimDeque.EnqueueFront(b[i]);
                Assert.Equal(i + a.Length + 1, slimDeque.Count);
            }
        }

        private static void Helper2(Int32[] a, Int32[] b)
        {
            var slimDeque = new SlimDeque<Int32>();

            for (var i = 0; i != a.Length; i++)
            {
                slimDeque.EnqueueFront(a[i]);
                Assert.Equal(i + 1, slimDeque.Count);
            }

            for (var i = 0; i != a.Length; i++)
            {
                slimDeque.DequeueFront();
                //Assert.Equal(i + 1, slimDeque.Count);
            }

            for (var i = 0; i != b.Length; i++)
            {
                slimDeque.EnqueueBack(b[i]);
                Assert.Equal(i + a.Length + 1, slimDeque.Count);
            }

            for (var i = 0; i != b.Length; i++)
            {
                slimDeque.DequeueBack();
            }
        }
    }
}
