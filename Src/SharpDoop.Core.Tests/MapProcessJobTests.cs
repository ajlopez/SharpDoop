namespace SharpDoop.Core.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MapProcessJobTests
    {
        [TestMethod]
        public void RunJobMapReduce()
        {
            MapProcessJob<int, string, string, int, Result> job = new MapProcessJob<int, string, string, int, Result>(Map, Process);
            var items = new string[] { "a", "word", "is", "a", "word" };
            var pairs = items.Select((item, k) => new Pair<int, string>() { Key = k, Value = item });
            job.Map(pairs);

            var result = job.Process();

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());

            var pair1 = result.FirstOrDefault(p => p.Key == "word");
            Assert.IsNotNull(pair1);
            Assert.AreEqual(2, pair1.Value.Counter);

            var pair2 = result.FirstOrDefault(p => p.Key == "a");
            Assert.IsNotNull(pair2);
            Assert.AreEqual(2, pair2.Value.Counter);

            var pair3 = result.FirstOrDefault(p => p.Key == "is");
            Assert.IsNotNull(pair3);
            Assert.AreEqual(1, pair3.Value.Counter);
        }

        [TestMethod]
        public void RunJobMapValueReduce()
        {
            MapProcessJob<int, string, string, int, Result> job = new MapProcessJob<int, string, string, int, Result>(Map, Process);
            var items = new string[] { "a", "word", "is", "a", "word" };

            foreach (var item in items)
                job.MapValue(item);

            var result = job.Process();

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());

            var pair1 = result.FirstOrDefault(p => p.Key == "word");
            Assert.IsNotNull(pair1);
            Assert.AreEqual(2, pair1.Value.Counter);

            var pair2 = result.FirstOrDefault(p => p.Key == "a");
            Assert.IsNotNull(pair2);
            Assert.AreEqual(2, pair2.Value.Counter);

            var pair3 = result.FirstOrDefault(p => p.Key == "is");
            Assert.IsNotNull(pair3);
            Assert.AreEqual(1, pair3.Value.Counter);
        }

        [TestMethod]
        public void RunJobTwoMapsAndReduce()
        {
            MapProcessJob<int, string, string, int, Result> job = new MapProcessJob<int, string, string, int, Result>(Map, Process);
            var items1 = new string[] { "a", "word", "is" };
            var pairs1 = items1.Select((item, k) => new Pair<int, string>() { Key = k, Value = item });
            var items2 = new string[] { "a", "word" };
            var pairs2 = items2.Select((item, k) => new Pair<int, string>() { Key = k, Value = item });
            job.Map(pairs1);
            job.Map(pairs2);
            var result = job.Process();

            Assert.IsNotNull(result);

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());

            var pair1 = result.FirstOrDefault(p => p.Key == "word");
            Assert.IsNotNull(pair1);
            Assert.AreEqual(2, pair1.Value.Counter);

            var pair2 = result.FirstOrDefault(p => p.Key == "a");
            Assert.IsNotNull(pair2);
            Assert.AreEqual(2, pair2.Value.Counter);

            var pair3 = result.FirstOrDefault(p => p.Key == "is");
            Assert.IsNotNull(pair3);
            Assert.AreEqual(1, pair3.Value.Counter);
        }

        private static void Map(int key, string value, MapProcessContext<string, int, Result> context)
        {
            context.Emit(value, 1);
        }

        private static void Process(string key, int value, Result result)
        {
            result.Counter += value;
        }

        private class Result
        {
            public int Counter { get; set; }
        }
    }
}
