namespace SharpDoop.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Context<K, V>
    {
        private IDictionary<K, IList<V>> keyvalues = new Dictionary<K, IList<V>>();

        public IDictionary<K, IList<V>> KeyValues { get { return this.keyvalues; } }

        public void Emit(K key, V value)
        {
            if (!this.keyvalues.ContainsKey(key))
                this.keyvalues[key] = new List<V>();

            this.keyvalues[key].Add(value);
        }
    }
}
