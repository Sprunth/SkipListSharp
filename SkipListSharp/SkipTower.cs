using System;
using System.Collections.Generic;
using System.Text;

namespace SkipListSharp
{
    public class SkipTower<T, T2>
    {
        public SkipNode<T, T2>[] Nodes { get; private set; }
        public int Height => Nodes.Length;
        public T Key { get; private set; }
        public T2 Value { get; private set; }
        public SkipTower(int height, T key, T2 value)
        {
            Nodes = new SkipNode<T, T2>[height];
            Key = key;
            Value = value;
            for (var i=0; i<height; i++)
            {
                Nodes[i] = new SkipNode<T, T2>(this);
            }
        }
    }
}
