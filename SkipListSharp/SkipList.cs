using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SkipListSharp
{
    public class SkipList<T> where T : IComparable<T>
    {
        readonly int towerHeight = 3;
        SkipTower<T> Start;
        readonly Random rand;
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

        public T Search(T value)
        {
            var tower = SearchForTower(value);
            return tower.Nodes[0].Value; // until we start bundling extra data in the nodes
        }

        private SkipTower<T> SearchForTower(T value)
        {
            var level = towerHeight;
            var currentNode = Start.Nodes[towerHeight - 1];

            while (true)
            {
                while (currentNode.Next.Value.CompareTo(value) <= 0)
                {
                    currentNode = currentNode.Next;
                }

                if (level == 0)
                {
                    return currentNode.ParentTower;
                }
                else
                {
                    level--;
                    currentNode = currentNode.ParentTower.Nodes[level];
                }
            }
        }

        public void Delete(T value)
        {
            // nodes prior to the deleted one that we need to update
            var predecessorNodes = new SkipNode<T>[towerHeight];

            {
                var level = towerHeight - 1;
                var currentNode = Start.Nodes[level];

                var foundAll = false;
                while (!foundAll)
                {
                    while (currentNode.Next.Value.CompareTo(value) < 0)
                    {
                        currentNode = currentNode.Next;
                    }

                    if (level == 0)
                    {
                        predecessorNodes[level] = currentNode;
                        foundAll = true;
                    }
                    else
                    {
                        predecessorNodes[level] = currentNode;
                        level--;
                        currentNode = currentNode.ParentTower.Nodes[level];
                    }
                }

                foreach (var node in predecessorNodes)
                {
                    node.Next = node.Next.Next;
                }
            }
        }

        public void ConsoleVisualize()
        {
            var node = Start.Nodes[0];
            var towers = new List<SkipTower<T>>();

            while (node != null)
            {
                towers.Add(node.ParentTower);
                node = node.Next;
            }

            var rows = new String[towers[0].Height];
            for (var i = 0; i < towers[0].Height; i++) { rows[i] = ""; }

            for(var i=0; i<towers.Count; i++)
            {
                var tower = towers[i];
                var contentSize = tower.Nodes[0].Value.ToString().Length;
                for (var level = 0; level < towerHeight; level++) 
                {
                    if (level >= tower.Height)
                    {
                        rows[level] += new string('-', contentSize);
                    } 
                    else
                    {
                        rows[level] += tower.Nodes[level].Value;
                    }

                    if (i != towers.Count - 1)
                    {
                        rows[level] += " - ";
                    }
                }
            }

            foreach(var row in rows.Reverse())
            {
                Console.WriteLine(row);
            }
        }
    }
}
