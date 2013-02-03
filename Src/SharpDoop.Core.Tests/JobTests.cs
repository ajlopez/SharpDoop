namespace SharpDoop.Core.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class JobTests
    {
        [TestMethod]
        public void RunJob()
        {
            Job<int, string, string, int, string, int> job = new Job<int, string, string, int, string, int>(Map, Reduce);
            var items = new string[] { "a", "word", "is", "a", "word" };
            var pairs = items.Select((item, k) => new Pair<int, string>() { Key = k, Value = item });
            var result = job.Process(pairs);

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Keys.Count);
            Assert.IsTrue(result.ContainsKey("a"));
            Assert.IsTrue(result.ContainsKey("word"));
            Assert.IsTrue(result.ContainsKey("is"));
            Assert.AreEqual(2, result["a"]);
            Assert.AreEqual(2, result["word"]);
            Assert.AreEqual(1, result["is"]);
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
