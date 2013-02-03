namespace SharpDoop.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Job<K1,V1,K2,V2,K3,V3>
    {
        private Action<K1, V1, Context<K2, V2>> map;
        private Action<K2, IList<V2>, Context<K3, V3>> reduce;

        public Job(Action<K1, V1, Context<K2, V2>> map, Action<K2, IList<V2>, Context<K3, V3>> reduce)
        {
            this.map = map;
            this.reduce = reduce;
        }

        public IDictionary<K3, IList<V3>> Process(IEnumerable<Pair<K1, V1>> keyvalues)
        {
            var mapcontext = new Context<K2, V2>();
            var redcontext = new Context<K3, V3>();

            foreach (var keyvalue in keyvalues)
                map(keyvalue.Key, keyvalue.Value, mapcontext);

            foreach (var key in mapcontext.KeyValues.Keys)
                reduce(key, mapcontext.KeyValues[key], redcontext);

            return redcontext.KeyValues;
        }
    }
}
