namespace SharpDoop.Core.Tests
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ContextTests
    {
        [TestMethod]
        public void EmptyKeyValues()
        {
            Context<string, int> context = new Context<string, int>();

            Assert.IsNotNull(context.KeyValues);
            Assert.AreEqual(0, context.KeyValues.Keys.Count);
        }

        [TestMethod]
        public void EmitKeyValue()
        {
            Context<string, int> context = new Context<string, int>();

            context.Emit("word", 1);

            Assert.IsNotNull(context.KeyValues);
            Assert.AreEqual(1, context.KeyValues.Keys.Count);
            Assert.IsTrue(context.KeyValues.ContainsKey("word"));
            Assert.IsNotNull(context.KeyValues["word"]);
            Assert.AreEqual(1, context.KeyValues["word"].Count);
            Assert.AreEqual(1, context.KeyValues["word"][0]);
        }

        [TestMethod]
        public void EmitSaveKeyValues()
        {
            Context<string, int> context = new Context<string, int>();

            context.Emit("a", 1);
            context.Emit("word", 1);
            context.Emit("is", 1);
            context.Emit("a", 1);
            context.Emit("word", 1);

            Assert.IsNotNull(context.KeyValues);
            Assert.AreEqual(3, context.KeyValues.Keys.Count);

            Assert.IsTrue(context.KeyValues.ContainsKey("word"));
            Assert.IsNotNull(context.KeyValues["word"]);
            Assert.AreEqual(2, context.KeyValues["word"].Count);
            Assert.AreEqual(1, context.KeyValues["word"][0]);
            Assert.AreEqual(1, context.KeyValues["word"][1]);

            Assert.IsTrue(context.KeyValues.ContainsKey("a"));
            Assert.IsNotNull(context.KeyValues["a"]);
            Assert.AreEqual(2, context.KeyValues["a"].Count);
            Assert.AreEqual(1, context.KeyValues["a"][0]);
            Assert.AreEqual(1, context.KeyValues["a"][1]);

            Assert.IsTrue(context.KeyValues.ContainsKey("is"));
            Assert.IsNotNull(context.KeyValues["is"]);
            Assert.AreEqual(1, context.KeyValues["is"].Count);
            Assert.AreEqual(1, context.KeyValues["is"][0]);
        }
    }
}
