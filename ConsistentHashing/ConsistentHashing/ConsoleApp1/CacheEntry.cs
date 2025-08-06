using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class CacheEntry<K, V>
    {
        K key;
        V value;
        public CacheEntry(K key, V value)
        {
            this.key = key;
            this.value = value;
        }

        public K getKey() { return key; }

        public V getValue() { return value; }
    }
}
