namespace SharpDoop.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class MapProcessJob<K1, V1, K, V, R> where R : new()
    {
        private Action<K1, V1, MapProcessContext<K, V, R>> map;
        private Action<K, V, R> process;
        private MapProcessContext<K, V, R> mapcontext;
        private IDictionary<K, R> results = new Dictionary<K, R>();

        public MapProcessJob(Action<K1, V1, MapProcessContext<K, V, R>> map, Action<K, V, R> process)
        {
            this.map = map;
            this.process = process;
            this.mapcontext = new MapProcessContext<K, V, R>(this.results, process);
        }

        public void MapKeyValue(K1 key, V1 value)
        {
            this.map(key, value, this.mapcontext);
        }

        public void MapValue(V1 value)
        {
            this.map(default(K1), value, this.mapcontext);
        }

        public void Map(IEnumerable<Pair<K1, V1>> keyvalues)
        {
            foreach (var keyvalue in keyvalues)
                this.MapKeyValue(keyvalue.Key, keyvalue.Value);
        }

        public IEnumerable<Pair<K, R>> Process()
        {
            foreach (var key in this.results.Keys)
                yield return new Pair<K, R>() { Key = key, Value = this.results[key] };
        }
    }
}
