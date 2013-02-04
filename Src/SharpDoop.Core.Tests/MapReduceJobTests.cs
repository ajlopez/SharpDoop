namespace SharpDoop.Core.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MapReduceJobTests
    {
        [TestMethod]
        public void RunJobMapReduce()
        {
            MapReduceJob<int, string, string, int, string, int> job = new MapReduceJob<int, string, string, int, string, int>(Map, Reduce);
            var items = new string[] { "a", "word", "is", "a", "word" };
            var pairs = items.Select((item, k) => new Pair<int, string>() { Key = k, Value = item });
            job.Map(pairs);

            var result = job.Reduce();

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());

            var pair1 = result.FirstOrDefault(p => p.Key == "word");
            Assert.IsNotNull(pair1);
            Assert.AreEqual(2, pair1.Value);

            var pair2 = result.FirstOrDefault(p => p.Key == "a");
            Assert.IsNotNull(pair2);
            Assert.AreEqual(2, pair2.Value);

            var pair3 = result.FirstOrDefault(p => p.Key == "is");
            Assert.IsNotNull(pair3);
            Assert.AreEqual(1, pair3.Value);
        }

        [TestMethod]
        public void RunJobMapValueReduce()
        {
            MapReduceJob<int, string, string, int, string, int> job = new MapReduceJob<int, string, string, int, string, int>(Map, Reduce);
            var items = new string[] { "a", "word", "is", "a", "word" };

            foreach (var item in items)
                job.MapValue(item);

            var result = job.Reduce();

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());

            var pair1 = result.FirstOrDefault(p => p.Key == "word");
            Assert.IsNotNull(pair1);
            Assert.AreEqual(2, pair1.Value);

            var pair2 = result.FirstOrDefault(p => p.Key == "a");
            Assert.IsNotNull(pair2);
            Assert.AreEqual(2, pair2.Value);

            var pair3 = result.FirstOrDefault(p => p.Key == "is");
            Assert.IsNotNull(pair3);
            Assert.AreEqual(1, pair3.Value);
        }

        [TestMethod]
        public void RunJobTwoMapsAndReduce()
        {
            MapReduceJob<int, string, string, int, string, int> job = new MapReduceJob<int, string, string, int, string, int>(Map, Reduce);
            var items1 = new string[] { "a", "word", "is" };
            var pairs1 = items1.Select((item, k) => new Pair<int, string>() { Key = k, Value = item });
            var items2 = new string[] { "a", "word" };
            var pairs2 = items2.Select((item, k) => new Pair<int, string>() { Key = k, Value = item });
            job.Map(pairs1);
            job.Map(pairs2);
            var result = job.Reduce();

            Assert.IsNotNull(result);

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());

            var pair1 = result.FirstOrDefault(p => p.Key == "word");
            Assert.IsNotNull(pair1);
            Assert.AreEqual(2, pair1.Value);

            var pair2 = result.FirstOrDefault(p => p.Key == "a");
            Assert.IsNotNull(pair2);
            Assert.AreEqual(2, pair2.Value);

            var pair3 = result.FirstOrDefault(p => p.Key == "is");
            Assert.IsNotNull(pair3);
            Assert.AreEqual(1, pair3.Value);
        }

        private static void Map(int key, string value, MapContext<string, int> context)
        {
            context.Emit(value, 1);
        }

        private static void Reduce(string key, IList<int> values, ReduceContext<string, int> context)
        {
            int total = values.Sum();

            context.Emit(key, total);
        }
    }
}
