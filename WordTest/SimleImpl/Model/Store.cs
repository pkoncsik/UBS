using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordTest.Shared.Model;

namespace WordTest.SimleImpl.Model
{
    internal class Store
    {
        private readonly Dictionary<string, int> _storage = new Dictionary<string, int>();
        /// <summary>
        /// Case insensitive, only lowercase
        /// </summary>
        /// <param name="word">input word</param>
        public void AddWord(string word)
        {
            if (String.IsNullOrEmpty(word)) return;
            String lowerCaseWord = word.ToLower();
            if (_storage.ContainsKey(lowerCaseWord))
                _storage[lowerCaseWord] += 1;
            else
                _storage[lowerCaseWord] = 1;

        }


        public IEnumerable<WordUsage> List()
        {
            foreach (var wordCount in _storage)
            {
                yield return new WordUsage { Word = wordCount.Key, Count = wordCount.Value };
            }
        }
    }
}
