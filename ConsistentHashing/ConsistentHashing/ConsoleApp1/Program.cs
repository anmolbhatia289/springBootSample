using ConsoleApp1;

var server = new Server("10.0.0.1");
var server2 = new Server("10.0.0.3");

var cacheClient = new CacheClient();
cacheClient.addServerToHashRing(server);
cacheClient.addServerToHashRing(server2);

cacheClient.addEntryToCache(new CacheEntry<string, string>("key1", "value1"));
string value = cacheClient.getValueToCache("key1");

cacheClient.addEntryToCache(new CacheEntry<string, string>("10.0.0.2", "value2"));
value = cacheClient.getValueToCache("10.0.0.2");
