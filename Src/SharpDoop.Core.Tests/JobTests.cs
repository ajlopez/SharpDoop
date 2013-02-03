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
            Assert.IsTrue(result.Values.All(v => v.Count == 1));
            Assert.IsTrue(result.ContainsKey("a"));
            Assert.IsTrue(result.ContainsKey("word"));
            Assert.IsTrue(result.ContainsKey("is"));
            Assert.AreEqual(2, result["a"][0]);
            Assert.AreEqual(2, result["word"][0]);
            Assert.AreEqual(1, result["is"][0]);
        }

        private static void Map(int key, string value, Context<string, int> context)
        {
            context.Emit(value, 1);
        }

        private static void Reduce(string key, IList<int> values, Context<string, int> context)
        {
            int total = values.Sum();

            context.Emit(key, total);
        }
    }
}
