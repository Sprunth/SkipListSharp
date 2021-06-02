using Xunit;
using SkipListSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace SkipListSharp.Tests
{
    public class SkipListTests
    {
        SkipList<int, string> skipList;
        public SkipListTests()
        {
            skipList = new SkipList<int, string>();
        }

        [Fact()]
        public void InsertTest()
        {
            skipList.Insert(123, "test");
        }

        [Fact()]
        public void SearchTest()
        {
            skipList.Insert(2, "test");
            skipList.Insert(5, "test2");
            skipList.Insert(3, "goal");
            Assert.Equal("goal", skipList.Search(3));
        }

        [Fact()]
        public void DeleteTest()
        {
            skipList.Insert(2, "test");
            skipList.Insert(5, "test2");
            skipList.Insert(3, "goal");
            skipList.Insert(4, "goal2");
            skipList.Delete(3);
            //skipList.Delete(234); // test deletion when you can't find the item
        }
    }
}