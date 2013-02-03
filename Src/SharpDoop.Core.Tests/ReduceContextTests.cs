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

            Assert.IsNotNull(context.KeyValues);
            Assert.AreEqual(0, context.KeyValues.Keys.Count);
        }

        [TestMethod]
        public void EmitKeyValue()
        {
            ReduceContext<string, int> context = new ReduceContext<string, int>();

            context.Emit("word", 1);

            Assert.IsNotNull(context.KeyValues);
            Assert.AreEqual(1, context.KeyValues.Keys.Count);
            Assert.IsTrue(context.KeyValues.ContainsKey("word"));
            Assert.IsNotNull(context.KeyValues["word"]);
            Assert.AreEqual(1, context.KeyValues["word"]);
        }

        [TestMethod]
        public void EmitSaveKeyValues()
        {
            ReduceContext<string, int> context = new ReduceContext<string, int>();

            context.Emit("a", 2);
            context.Emit("word", 2);
            context.Emit("is", 1);

            Assert.IsNotNull(context.KeyValues);
            Assert.AreEqual(3, context.KeyValues.Keys.Count);

            Assert.IsTrue(context.KeyValues.ContainsKey("word"));
            Assert.IsNotNull(context.KeyValues["word"]);
            Assert.AreEqual(2, context.KeyValues["word"]);

            Assert.IsTrue(context.KeyValues.ContainsKey("a"));
            Assert.IsNotNull(context.KeyValues["a"]);
            Assert.AreEqual(2, context.KeyValues["a"]);

            Assert.IsTrue(context.KeyValues.ContainsKey("is"));
            Assert.IsNotNull(context.KeyValues["is"]);
            Assert.AreEqual(1, context.KeyValues["is"]);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RaiseIfDuplicatedKey()
        {
            ReduceContext<string, int> context = new ReduceContext<string, int>();

            context.Emit("a", 2);
            context.Emit("word", 2);
            context.Emit("is", 1);
            context.Emit("a", 1);
        }
    }
}
