using System;
using SkipListSharp;

namespace SkipListSharpExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var skipList = new SkipList<int>();
            skipList.Insert(5);
            skipList.Insert(15);
            skipList.Insert(12);
            skipList.Insert(7);
            skipList.ConsoleVisualize();
            Console.ReadLine();
        }
    }
}
