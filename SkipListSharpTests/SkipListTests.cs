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
            Assert.True(skipList.Insert(123, "test"));
        }

        [Fact(DisplayName = "Inserting existing key fails")]
        public void InsertDuplicateKey()
        {
            Assert.True(skipList.Insert(22, "test"));
            Assert.False(skipList.Insert(22, "test2"));
        }

        [Fact()]
        public void SearchTest()
        {
            skipList.Insert(2, "test");
            skipList.Insert(5, "test2");
            skipList.Insert(3, "goal");
            Assert.Equal("goal", skipList.Search(3));
        }

        [Fact(DisplayName = "Basic Deletion")]
        public void DeleteTest()
        {
            skipList.Insert(3, "goal");
            Assert.True(skipList.Delete(3));
            
        }

        [Fact(DisplayName = "Delete non existant key")]
        public void DeleteLastElementTest()
        {
            Assert.False(skipList.Delete(4));
            skipList.Insert(20, "test");
            Assert.False(skipList.Delete(19));
            skipList.Insert(25, "test2");
            Assert.False(skipList.Delete(23));
            Assert.False(skipList.Delete(26));
        }
    }
}