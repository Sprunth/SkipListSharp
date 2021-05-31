using System;
using System.Collections.Generic;
using System.Text;

namespace SkipListSharp
{
    public class SkipList<T> where T : IComparable<T>
    {
        readonly int towerHeight = 3;
        SkipTower<T> Start;
        Random rand;
        public SkipList()
        {
            rand = new Random();
        }

        public void Insert(T item)
        {
            if (Start == null)
            {
                Start = new SkipTower<T>(towerHeight, item);
            }
            else if (Start.Nodes[0].Next == null)
            {
                var end = new SkipTower<T>(towerHeight, item);
                for (var i=0; i<towerHeight; i++)
                {
                    Start.Nodes[i].Next = end.Nodes[i];
                }
            }
            else
            {
                var tower = new SkipTower<T>(GenerateTargetLevel() + 1, item);
                RecursiveInsert(Start.Nodes[Start.Height - 1], tower, Start.Height);
            }
        }

        private bool RecursiveInsert(SkipNode<T> root, SkipTower<T> tower, int level)
        {
            // all items in the tower are the same so any will do for comparison
            if (root.Next.Value.CompareTo(tower.Nodes[0].Value) < 0)
            {
                return RecursiveInsert(root.Next, tower, level);
            }
            
            if (level == 0)
            {
                var old = root.Next;
                root.Next = tower.Nodes[level]; // should be 0
                tower.Nodes[level].Next = old;
                return true;
            }

            if (level < tower.Height) // height is 1-based, level is 0-based
            {
                var old = root.Next;
                root.Next = tower.Nodes[level];
                tower.Nodes[level].Next = old;
            }
            return RecursiveInsert(root.ParentTower.Nodes[level - 1], tower, level - 1);
        }

        private int GenerateTargetLevel()
        {
            var level = 0;
            while(rand.Next(2) == 0)
            {
                level++;
            }
            return level;
        }
    }
}
