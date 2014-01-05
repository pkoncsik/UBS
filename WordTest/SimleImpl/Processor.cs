using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordTest.Shared.Interfaces;
using WordTest.SimleImpl.Model;

namespace WordTest.SimleImpl
{
    public class Processor: IProcessor
    {
        private readonly Store _store = new Store();

        public void GetInput(string input)
        {

            CheckInput(input);
            CompileStatistics(input);
        }

        public void ShowStatistics(Action<Shared.Model.WordUsage> wordUsage)
        {
            foreach (var usage in _store.List())
            {
                wordUsage(usage);
            }
        }
        private void CheckInput(string input)
        {
            if (String.IsNullOrEmpty(input) || String.IsNullOrWhiteSpace(input))
                throw new ArgumentNullException("Input does not contain text!");
            if (!input.EndsWith(".") && !input.EndsWith("?") && !input.EndsWith("!"))
                throw new ArgumentException("Input is not a sentence!");
        }
        private void CompileStatistics(string input)
        {
            //spliting the sentence into words by white spaces
            string[] words = input.Split(null);
            CleanedUpWords(words).ForEach(_store.AddWord);
        }
        private List<string> CleanedUpWords(string[] wordList)
        {
            List<string> words = new List<string>();
            foreach(var word in wordList)
            {
               //Easier to handle char List
                List<char> charList = word.ToLower().ToList<char>();
                RemovePunctuation(charList);
                RemoveQuotation(charList);
                CheckNumber(charList);
                CheckSymbols(charList);
                if (charList.Count > 0)
                    _store.AddWord(new string(charList.ToArray()));


            }
            return words;
        }
        /// <summary>
        /// Check if the word start or finishes with an alphanumeric character
        /// </summary>
        /// <param name="word">The word</param>
        private void CheckSymbols(List<char> word)
        {
            if (word.Count == 0) return;
            if (!Char.IsLetterOrDigit(word[word.Count - 1]) || !Char.IsLetterOrDigit(word[0]))
                word.Clear();
        }
        /// <summary>
        /// Removing punctuation from the end of the string, recursively to include ... for example
        /// </summary>
        /// <param name="word">The word</param>
        private void RemovePunctuation(List<char> word)
        {
            if (word.Count == 0) return;
            if (Char.IsPunctuation(word[word.Count-1]))
            {
                word.RemoveAt(word.Count - 1);
                RemovePunctuation(word);
            }
        }
        /// <summary>
        /// Removing quotation marks from the end and beginningof the string: ' and "
        /// </summary>
        /// <param name="word">The word</param>
        private void RemoveQuotation(List<char> word)
        {
            if (word.Count == 0) return;
            RemoveCharacter(word, '\'', 0);
            RemoveCharacter(word, '\'', word.Count-1);
            RemoveCharacter(word, '"', 0);
            RemoveCharacter(word, '"', word.Count - 1);
              
            
        }
        private void RemoveCharacter(List<char> word,char charToRemove,int position)
        {
            if (word[position] == charToRemove)
                word.RemoveAt(position);
        }
        private void CheckNumber(List<char> word)
        {
            if (word.Where(x => !Char.IsDigit(x)).Count() == 0)
                word.Clear();
        }

    }
}
