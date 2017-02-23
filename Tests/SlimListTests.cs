using SlimCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Tests
{
    public class SlimListTests
    {
        [Fact]
        public void Tests()
        {
            var values = new [] { 0, 1 };
            var slimList = new SlimList<Int32>();
            Assert.Equal(0, slimList.Count);
            Assert.Throws<IndexOutOfRangeException>(() => slimList[0]);
            Assert.Throws<IndexOutOfRangeException>(() => slimList[1]);

            slimList.Add(values[0]);
            Assert.Equal(1, slimList.Count);
            Assert.Equal(values[0], slimList[0]);
            Assert.Throws<IndexOutOfRangeException>(() => slimList[1]);

            slimList[0] = values[0] = 42;
            Assert.Equal(1, slimList.Count);
            Assert.Equal(values[0], slimList[0]);
            Assert.Throws<IndexOutOfRangeException>(() => slimList[1]);

            slimList.Add(values[1]);
            Assert.Equal(2, slimList.Count);
            Assert.Equal(values[0], slimList[0]);
            Assert.Equal(values[1], slimList[1]);
            Assert.Throws<IndexOutOfRangeException>(() => slimList[2]);

            var i = 0;
            foreach (var x in slimList)
                Assert.Equal(values[i++], x);
        }

        [Fact]
        public void HeapAndEnumerableTest()
        {
            var values = Enumerable.Range(0, 15).ToArray();
            var slimList = new SlimList<Int32>();
            foreach (var value in values)
                slimList.Add(value);
            for (var i = 0; i != values.Length; i++)
                Assert.Equal(values[i], slimList[i]);
            var j = 0;
            foreach (var x in slimList)
                Assert.Equal(values[j++], x);
            var k = 14;
            foreach (var x in ((IEnumerable<Int32>)slimList).Reverse())
                Assert.Equal(values[k--], x);
            k = 14;
            foreach (var x in slimList.Reverse())
                Assert.Equal(values[k--], x);
        }

        [Fact]
        public void RemoveAtTest()
        {
            var values = Enumerable.Range(0, 15).ToList();
            var slimList = new SlimList<Int32>();
            foreach (var value in values)
                slimList.Add(value);
            slimList.RemoveAt(5);
            values.RemoveAt(5);
            for (var i = 0; i != values.Count; i++)
                Assert.Equal(values[i], slimList[i]);
            slimList.RemoveAt(12);
            values.RemoveAt(12);
            for (var i = 0; i != values.Count; i++)
                Assert.Equal(values[i], slimList[i]);
        }

        [Fact]
        public void InsertTest()
        {
            var slimList = new SlimList<Int32>();
            var list = new List<Int32>();
            list.Insert(0, 12);
            slimList.Insert(0, 12);
            list.Insert(1, 13);
            slimList.Insert(1, 13);
            list.Insert(0, 42);
            slimList.Insert(0, 42);
            Assert.Equal(list, slimList);
            Assert.Throws<ArgumentOutOfRangeException>(() => list.Insert(42, 123));
            Assert.Throws<ArgumentOutOfRangeException>(() => slimList.Insert(42, 123));
        }

        [Fact]
        public void LinqTest()
        {
            var slimList = new SlimList<Int32> { 1, 2, 3, 42, 1337 };
            var what = slimList.Where(x => x < 10);
            Assert.Equal(new[] { 1, 2, 3 }, what);
        }
    }
}
