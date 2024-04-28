// actors: user, system
// classes: Trie, TrieNode, TrieConstructor, Frequency

using System.Runtime.ExceptionServices;

namespace SearchAutoComplete
{
    public class Program
    {
        public static void Main()
        {
            var service = SearchService.GetInstance();
            service.Hit("catterpillar");
            service.Hit("catastrophic");
            var suggestions = service.Search("cat");
        }
    }
    

    public class Trie
    {
        TrieNode root;
        public Trie()
        {
            root = new TrieNode('#');
        }

        public TrieNode GetRoot() { return root; }
    }

    public class TrieNode
    {
        public char data;
        public TrieNode[] children;
        public bool isEndOfWord;
        public int frequency;

        public char GetData() { return data; }
        public TrieNode(char data)
        {
            children = new TrieNode[26];
            isEndOfWord = false;
            frequency = 0;
            this.data = data;
        }
    }

    public class SearchService
    {
        private Trie trie;
        private static SearchService _searchServiceInstance;
        private SearchService()
        {
            trie = new Trie();
        }

        public static SearchService GetInstance()
        {
            if (_searchServiceInstance == null)
            {
                _searchServiceInstance = new SearchService();
            }
            return _searchServiceInstance;
        }

        public List<string> Search(string prefix)
        {
            var root = trie.GetRoot();
            TrieNode current = root;
            for (int i = 0; i < prefix.Length; i++)
            {
                int index = prefix[i] - 'a';
                if (current.children[index] == null)
                {
                    return new List<string>();
                }
                current = current.children[index];
            }

            var list = new List<string>();
            SearchHelper(current, prefix, list);
            return list;
        }

        private void SearchHelper(TrieNode current, string prefix, List<string> list)
        {
            if (current.isEndOfWord)
            {
                list.Add(prefix);
            }

            for (int i = 0; i < 26; i++)
            {
                if (current.children[i] != null)
                {
                    prefix += current.children[i].GetData();
                    SearchHelper(current.children[i], prefix, list);
                    prefix.Remove(prefix.Length - 1);
                }
            }

            return;
        }

        public void Hit(string word)
        {
            var current = trie.GetRoot();
            for (int i = 0; i < word.Length; i++)
            {
                int index = word[i] - 'a';
                if (current.children[index] == null)
                {
                    current.children[index] = new TrieNode(word[i]);
                }

                current = current.children[index];
            }

            current.isEndOfWord = true;
        }
    }
}