using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Server
    {
        List<CacheEntry<string, string>> cache;
        string ipAddress;

        public Server(string ipAddress) 
        {
            this.ipAddress = ipAddress;
            cache = new List<CacheEntry<string, string>>();
        }

        public string getEntry(string key)
        {
            foreach (var cacheEntry in cache)
            {
                if (cacheEntry.getKey() == key)
                {
                    return cacheEntry.getValue();
                }
            }

            return null;
        }

        public void addEntry(CacheEntry<string, string> entry) 
        {
            cache.Add(entry);
        }

        public string getIp() 
        {
            return ipAddress;
        }

    }
}
