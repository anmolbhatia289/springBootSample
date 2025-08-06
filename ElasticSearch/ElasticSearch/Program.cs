using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

public class MapReduce
{
    static BlockingCollection<KeyValuePair<string, string>> intermediate = new BlockingCollection<KeyValuePair<string, string>>();

    public class Mapper
    {
        public void Map(object input)
        {
            string document = (string)input;
            string[] words = document.Split(new char[] { ' ', '.', ',', ';', ':', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string word in words)
            {
                intermediate.Add(new KeyValuePair<string, string>(word.ToLower(), document));
            }
        }
    }

    public class Reducer
    {
        public void Reduce()
        {
            var groups = intermediate.GroupBy(kv => kv.Key);
            foreach (var group in groups)
            {
                Console.WriteLine($"{group.Key}: {string.Join(", ", group.Select(kv => kv.Value))}");
            }
        }
    }

    public static void Main(string[] args)
    {
        var input = new List<string>
        {
            "Document 1: This is the first document.",
            "Document 2: This document is the second one.",
            "Document 3: And finally, this is the third document."
        };

        var mapper = new Mapper();
        var reducer = new Reducer();

        Thread[] mapperThreads = new Thread[input.Count];
        for (int i = 0; i < input.Count; i++)
        {
            mapperThreads[i] = new Thread(new ParameterizedThreadStart(mapper.Map));
            mapperThreads[i].Start(input[i]);
        }

        foreach (Thread t in mapperThreads)
        {
            t.Join();
        }

        reducer.Reduce();
    }
}
