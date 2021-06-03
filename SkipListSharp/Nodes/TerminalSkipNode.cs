using System;
using System.Collections.Generic;
using System.Text;

namespace SkipListSharp.Nodes
{
    class TerminalSkipNode<T, T2> : SkipNode<T, T2>
    {
        public TerminalSkipNode(SkipTower<T, T2> parent) : base(parent)
        {
        }
    }
}
