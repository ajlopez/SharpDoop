namespace SharpDoop.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class MapProcessContext<K, V, R> where R : new()
    {
        private IDictionary<K, R> results;
        private Action<K, V, R> process;

        public MapProcessContext(IDictionary<K, R> results, Action<K, V, R> process)
        {
            this.results = results;
            this.process = process;
        }

        public void Emit(K key, V value)
        {
            if (!this.results.ContainsKey(key))
                this.results[key] = new R();

            this.process(key, value, this.results[key]);
        }
    }
}
