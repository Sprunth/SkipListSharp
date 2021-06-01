using System;
using SkipListSharp;

namespace SkipListSharpExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var skipList = new SkipList<int, string>();
            skipList.Insert(5, "hey");
            skipList.Insert(15, "end");
            skipList.Insert(12, "third");
            skipList.Insert(7, "second");
            skipList.Insert(13, "fourth");
            skipList.ConsoleVisualize();
            Console.WriteLine($"Searching for 12: {skipList.Search(12)}");
            skipList.Delete(13);
            skipList.ConsoleVisualize();
            Console.ReadLine();
        }
    }
}
