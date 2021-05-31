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
            skipList.Insert(9);
            skipList.Insert(6);
            Console.ReadLine();
        }
    }
}
