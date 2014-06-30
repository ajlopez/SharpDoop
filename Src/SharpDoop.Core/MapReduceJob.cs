namespace SharpDoop.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class MapReduceJob<K1, V1, K2, V2, K3, V3>
    {
        private Action<K1, V1, MapContext<K2, V2>> map;
        private Action<K2, IList<V2>, ReduceContext<K3, V3>> reduce;
        private MapContext<K2, V2> mapcontext = new MapContext<K2, V2>();
        private ReduceContext<K3, V3> redcontext = new ReduceContext<K3, V3>();

        public MapReduceJob(Action<K1, V1, MapContext<K2, V2>> map, Action<K2, IList<V2>, ReduceContext<K3, V3>> reduce)
        {
            this.map = map;
            this.reduce = reduce;
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

        public IEnumerable<Pair<K3, V3>> Reduce()
        {
            foreach (var key in this.mapcontext.KeyValues.Keys)
                this.reduce(key, this.mapcontext.KeyValues[key], this.redcontext);

            return this.redcontext.Pairs;
        }
    }
}
