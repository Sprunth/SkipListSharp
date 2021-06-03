using System;
using System.Collections.Generic;
using System.Text;

namespace SkipListSharp.Nodes
{
    class StartingSkipNode<T, T2> : SkipNode<T, T2>
    {
        public StartingSkipNode(SkipTower<T, T2> parent) : base(parent)
        {
        }
    }
}
