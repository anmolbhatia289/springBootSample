using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class CacheClient : ICacheClient
    {
        int numReplicas = 1;
        public SortedDictionary<long, Server> ring;

        public CacheClient()
        {
            ring = new SortedDictionary<long, Server>();
        }

        public bool addEntryToCache(CacheEntry<string, string> entry)
        {
            long hashedKey = HashFunction.createHash(entry.getKey());
            bool entryAdded = false;
            foreach (var node in ring)
            {
                if (node.Key >= hashedKey)
                {
                    node.Value.addEntry(entry);
                    entryAdded = true;
                    break;
                }
            }

            if (!entryAdded) 
            {
                ring.First().Value.addEntry(entry);
            }

            return true;
        }
        public string getValueToCache(string key)
        {
            long hashedKey = HashFunction.createHash(key);
            bool entryAdded = false;
            foreach (var node in ring)
            {
                if (node.Key >= hashedKey)
                {
                    return node.Value.getEntry(key);
                }
            }

            return ring.First().Value.getEntry(key);
        }

        public bool addServerToHashRing(Server server)
        {
            for (int i = 0; i < numReplicas; i++)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(server.getIp());
                sb.Append(":");
                sb.Append(i);
                long hashKey = HashFunction.createHash(sb.ToString());
                ring.Add(hashKey, server);
            }

            return true;
        }

        public bool removeServerFromHashRing(Server server)
        {
            throw new NotImplementedException();
        }
    }
}
