using System;
using System.Collections.Generic;
using System.Text;
using SkipListSharp.Nodes;

namespace SkipListSharp
{
    public class SkipTower<T, T2>
    {
        public SkipNode<T, T2>[] Nodes { get; private set; }
        public int Height => Nodes.Length;
        public T Key { get; private set; }
        public T2 Value { get; private set; }

        private SkipTower(int height, T key, T2 value)
        {
            Nodes = new SkipNode<T, T2>[height];
            Key = key;
            Value = value;
        }
        private SkipTower(int height)
        {
            Nodes = new SkipNode<T, T2>[height];
        }

        public static SkipTower<T, T2> CreateTower(int height, T key, T2 value)
        {
            var tower = new SkipTower<T, T2>(height, key, value);
            for (var i=0; i<height; i++)
            {
                tower.Nodes[i] = new SkipNode<T, T2>(tower);
            }
            return tower;
        }
        public static SkipTower<T, T2> CreateStartTower(int height)
        {
            var tower = new SkipTower<T, T2>(height);
            for (var i = 0; i < height; i++)
            {
                tower.Nodes[i] = new StartingSkipNode<T, T2>(tower);
            }
            return tower;
        }
        public static SkipTower<T, T2> CreateTerminalTower(int height)
        {
            var tower = new SkipTower<T, T2>(height);
            for (var i = 0; i < height; i++)
            {
                tower.Nodes[i] = new TerminalSkipNode<T, T2>(tower);
            }
            return tower;
        }

    }
}
