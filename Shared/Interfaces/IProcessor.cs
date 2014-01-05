using System;
using WordTest.Shared.Model;

namespace WordTest.Shared.Interfaces
{
    public interface IProcessor
    {
        void GetInput(string input);
        void ShowStatistics(Action<WordUsage> wordUsage);

    }
}
