using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordTest.Shared.Interfaces;
using WordTest.SimleImpl;
using WordTest.Shared.Model;

namespace WordTest.UnitTest
{
    [TestClass]
    public class InputTest
    {
        [TestMethod]
        public void TestSentenceSuccess()
        {
            IProcessor processor = new Processor();           
            string input = "This is a statement, and so is this.";
            processor.GetInput(input);
            processor.ShowStatistics(Display);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestSentenceFailure1()
        {
            IProcessor processor = new Processor();
            string input = "This is a statement, and so is this";
            processor.GetInput(input);
            processor.ShowStatistics(Display);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSentenceFailure2()
        {
            IProcessor processor = new Processor();
            string input = "";
            processor.GetInput(input);
            processor.ShowStatistics(Display);
        }
        private static void Display(WordUsage usage)
        {
            Console.WriteLine(String.Format("{0} - {1}", usage.Word, usage.Count));
        }
    }
}
