using System;
using WordTest.Shared.Interfaces;
using WordTest.Shared.Model;
using WordTest.SimleImpl;

namespace WordTest.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //This can be wired via spring to accomodate extended use cases as mentioned in README.docs
            //which can take filename as an input and process the stream event based
            IProcessor processor = new Processor();
            Console.Write("Please enter the sentence to analyse:");
            string input = Console.ReadLine();
            processor.GetInput(input);
            processor.ShowStatistics(Display);
            Console.ReadKey();
        }
        private static void Display(WordUsage usage)
        {
            Console.WriteLine(String.Format("{0} - {1}", usage.Word, usage.Count));
        }
    }
}
