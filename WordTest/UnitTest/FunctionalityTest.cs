using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordTest.Shared.Model;
using System.Collections.Generic;
using WordTest.Shared.Interfaces;
using WordTest.SimleImpl;

namespace WordTest.UnitTest
{
    [TestClass]
    public class FunctionalityTest
    {
        private static readonly Dictionary<string, int> _storage = new Dictionary<string, int>();
        [TestMethod]
        public void TestNumberFilter()
        {
            _storage.Clear();
            Dictionary<string, int> testDict = new Dictionary<string, int>();
            testDict.Add("this", 2);
            testDict.Add("is", 2);
            testDict.Add("a", 1);
            testDict.Add("statement", 1);
            testDict.Add("including", 1);
            testDict.Add("and", 1);
            testDict.Add("so", 1);
            IProcessor processor = new Processor();
            string input = "This is a statement, and so is this, including 45.";
            processor.GetInput(input);
            processor.ShowStatistics(Display);
            Assert.IsTrue(StatisticCheck(testDict));
        }
         [TestMethod]
        public void TestPunctuationFilter()
        {
            _storage.Clear();
            Dictionary<string, int> testDict = new Dictionary<string, int>();
            testDict.Add("this", 2);
            testDict.Add("is", 2);
            testDict.Add("a", 1);
            testDict.Add("statement", 1);
            testDict.Add("including", 1);
            testDict.Add("and", 1);
            testDict.Add("so", 1);
            IProcessor processor = new Processor();
            string input = "This is a statement, and so: is this, including...";
            processor.GetInput(input);
            processor.ShowStatistics(Display);
            Assert.IsTrue(StatisticCheck(testDict));
        }
         [TestMethod]
         public void Test100CharLimit()
         {
             _storage.Clear();
             Dictionary<string, int> testDict = new Dictionary<string, int>();
             testDict.Add("this", 33);
             testDict.Add("is", 33);
             testDict.Add("a", 16);
             testDict.Add("statement", 16);
             testDict.Add("including5", 16);
             testDict.Add("and", 16);
             testDict.Add("so", 16);
             testDict.Add("but", 16);
             testDict.Add("not", 16);
             IProcessor processor = new Processor();
             string input = @"This is a statement, and so: is this, including5, but not #e@ 
This is a statement, and so: is this, including5, but not #e@ 
This is a statement, and so: is this, including5, but not #e@ 
This is a statement, and so: is this, including5, but not #e@ 
This is a statement, and so: is this, including5, but not #e@ 
This is a statement, and so: is this, including5, but not #e@ 
This is a statement, and so: is this, including5, but not #e@ 
This is a statement, and so: is this, including5, but not #e@ 
This is a statement, and so: is this, including5, but not #e@ 
This is a statement, and so: is this, including5, but not #e@ 
This is a statement, and so: is this, including5, but not #e@ 
This is a statement, and so: is this, including5, but not #e@ 
This is a statement, and so: is this, including5, but not #e@ 
This is a statement, and so: is this, including5, but not #e@ 
This is a statement, and so: is this, including5, but not #e@ 
This is a statement, and so: is this, including5, but not #e@ 
This is.";
             processor.GetInput(input);
             processor.ShowStatistics(Display);
             Assert.IsTrue(StatisticCheck(testDict));
         }
        private static void Display(WordUsage usage)
        {

            _storage[usage.Word] = usage.Count;
            Console.WriteLine(String.Format("{0} - {1}", usage.Word, usage.Count));
        }
        private static bool StatisticCheck(Dictionary<string, int> expected)
        {
            if (expected.Count != _storage.Count)
                return false;
            foreach(var element in expected)
            {
                if (!_storage.ContainsKey(element.Key))
                    return false;
                if (_storage[element.Key] != element.Value)
                    return false;
            }
            return true;
        }
    }
}
