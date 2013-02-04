namespace SharpDoop.Core.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ReduceContextTests
    {
        [TestMethod]
        public void EmptyKeyValues()
        {
            ReduceContext<string, int> context = new ReduceContext<string, int>();

            Assert.IsNotNull(context.Pairs);
            Assert.AreEqual(0, context.Pairs.Count());
        }

        [TestMethod]
        public void EmitKeyValue()
        {
            ReduceContext<string, int> context = new ReduceContext<string, int>();

            context.Emit("word", 1);

            Assert.IsNotNull(context.Pairs);
            Assert.AreEqual(1, context.Pairs.Count());

            var pair = context.Pairs.First();
            Assert.IsNotNull(pair);
            Assert.AreEqual("word", pair.Key);
            Assert.AreEqual(1, pair.Value);
        }

        [TestMethod]
        public void EmitSaveKeyValues()
        {
            ReduceContext<string, int> context = new ReduceContext<string, int>();

            context.Emit("a", 2);
            context.Emit("word", 2);
            context.Emit("is", 1);

            Assert.IsNotNull(context.Pairs);
            Assert.AreEqual(3, context.Pairs.Count());

            var pair1 = context.Pairs.FirstOrDefault(p => p.Key == "word");
            Assert.IsNotNull(pair1);
            Assert.AreEqual(2, pair1.Value);

            var pair2 = context.Pairs.FirstOrDefault(p => p.Key == "a");
            Assert.IsNotNull(pair2);
            Assert.AreEqual(2, pair2.Value);

            var pair3 = context.Pairs.FirstOrDefault(p => p.Key == "is");
            Assert.IsNotNull(pair3);
            Assert.AreEqual(1, pair3.Value);
        }
    }
}
