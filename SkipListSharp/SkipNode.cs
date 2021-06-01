using System;

namespace SkipListSharp
{
    public class SkipNode<T, T2>
    {
        public SkipNode<T, T2> Next { get; set; }
        public SkipTower<T, T2> ParentTower { get; private set; }
        public SkipNode(SkipTower<T, T2> parent)
        {
            ParentTower = parent;
        }
    }
}
