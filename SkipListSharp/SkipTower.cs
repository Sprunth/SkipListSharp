using System;
using System.Collections.Generic;
using System.Text;

namespace SkipListSharp
{
    public class SkipTower<T>
    {

        public SkipNode<T>[] Nodes { get; private set; }
        public int Height => Nodes.Length;
        public SkipTower(int height, T value)
        {
            Nodes = new SkipNode<T>[height];
            for (var i=0; i<height; i++)
            {
                Nodes[i] = new SkipNode<T>(value, this);
            }
        }
    }
}
