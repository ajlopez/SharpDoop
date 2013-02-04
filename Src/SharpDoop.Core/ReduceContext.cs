namespace SharpDoop.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ReduceContext<K, V>
    {
        private IList<Pair<K, V>> pairs = new List<Pair<K, V>>();

        public IEnumerable<Pair<K, V>> Pairs { get { return this.pairs; } }

        public void Emit(K key, V value)
        {
            this.pairs.Add(new Pair<K, V>() { Key = key, Value = value });
        }
    }
}
