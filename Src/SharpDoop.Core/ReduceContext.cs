namespace SharpDoop.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ReduceContext<K, V>
    {
        private IDictionary<K, V> keyvalues = new Dictionary<K, V>();

        public IDictionary<K, V> KeyValues { get { return this.keyvalues; } }

        public void Emit(K key, V value)
        {
            if (this.keyvalues.ContainsKey(key))
                throw new InvalidOperationException(string.Format("Key '{0}' already exists", key.ToString()));

            this.keyvalues[key] = value;
        }
    }
}
