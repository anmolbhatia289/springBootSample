using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public interface ICacheClient
    {
        public bool addServerToHashRing(Server server);

        public bool removeServerFromHashRing(Server server);

        public bool addEntryToCache(CacheEntry<string, string> entry);

        public string getValueToCache(string key);
    }
}
