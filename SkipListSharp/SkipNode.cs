using System;

namespace SkipListSharp
{
    public class SkipNode<T>
    {
        public T Value { get; private set; }
        public SkipNode<T> Next { get; set; }
        public SkipTower<T> ParentTower { get; private set; }
        public SkipNode(T value, SkipTower<T> parent)
        {
            Value = value;
            ParentTower = parent;
        }
    }
}
