using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SkipListSharp.Nodes;

namespace SkipListSharp
{
    public class SkipList<T, T2> where T : IComparable<T>
    {
        readonly int towerHeight = 3;
        SkipTower<T, T2> Start;
        readonly Random rand;
        public SkipList()
        {
            rand = new Random();
            Start = SkipTower<T, T2>.CreateStartTower(towerHeight);
            var end = SkipTower<T, T2>.CreateTerminalTower(towerHeight);
            for (var i=0; i<towerHeight; i++)
            {
                Start.Nodes[i].Next = end.Nodes[i];
            }
        }

        public bool Insert(T key, T2 item)
        {
            var tower = SkipTower<T, T2>.CreateTower(GenerateTargetLevel() + 1, key, item);
            return RecursiveInsert(Start.Nodes[Start.Height - 1], tower, Start.Height);
            
        }

        private bool RecursiveInsert(SkipNode<T, T2> root, SkipTower<T, T2> tower, int level)
        {
            // all items in the tower are the same so any will do for comparison
            if (!(root.Next is TerminalSkipNode<T, T2>) && root.Next.ParentTower.Key.CompareTo(tower.Key) < 0)
            {
                return RecursiveInsert(root.Next, tower, level);
            }
            
            if (level == 0)
            {
                if (root.ParentTower.Key.CompareTo(tower.Key) == 0 || root.Next.ParentTower.Key.CompareTo(tower.Key) == 0)
                {
                    // node exists, we can't have duplicates!
                    return false;
                }
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
                if (level == towerHeight - 1)
                {
                    break;
                }
            }
            return level;
        }

        public T2 Search(T key)
        {
            var tower = SearchForTower(key);
            return tower.Value;
        }

        private SkipTower<T, T2> SearchForTower(T key)
        {
            var level = towerHeight;
            var currentNode = Start.Nodes[towerHeight - 1];

            while (true)
            {
                while (!(currentNode.Next is TerminalSkipNode<T, T2>) && currentNode.Next.ParentTower.Key.CompareTo(key) <= 0)
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

        public bool Delete(T key)
        {
            // nodes prior to the deleted one that we need to update
            var predecessorNodes = new SkipNode<T, T2>[towerHeight];

            {
                var level = towerHeight - 1;
                var currentNode = Start.Nodes[level];

                var foundAll = false;
                while (!foundAll)
                {
                    while (!(currentNode.Next is TerminalSkipNode<T, T2>) && currentNode.Next.ParentTower.Key.CompareTo(key) < 0)
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

                if (predecessorNodes[0].Next.ParentTower.Key.CompareTo(key) != 0)
                {
                    return false;
                }

                foreach (var node in predecessorNodes)
                {
                    node.Next = node.Next.Next;
                }
                return true;
            }
        }

        public void ConsoleVisualize()
        {
            var node = Start.Nodes[0];
            var towers = new List<SkipTower<T, T2>>();

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
                if (tower.Nodes[0] is StartingSkipNode<T, T2> || tower.Nodes[0] is TerminalSkipNode<T, T2>)
                {
                    for (var level = 0; level < towerHeight; level++)
                    {
                        rows[level] += "x";
                        if (i != towers.Count - 1)
                        {
                            rows[level] += " - ";
                        }
                    }
                }
                else
                {
                    var contentSize = tower.Nodes[0].ParentTower.Key.ToString().Length;
                    for (var level = 0; level < towerHeight; level++)
                    {
                        if (level >= tower.Height)
                        {
                            rows[level] += new string('-', contentSize);
                        }
                        else
                        {
                            rows[level] += tower.Key;
                        }

                        if (i != towers.Count - 1)
                        {
                            rows[level] += " - ";
                        }
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
