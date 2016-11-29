using System.Collections.Generic;

namespace SudokuCS
{
    class Node
    {
        public int value;
        public HashSet<int> possible;
        public HashSet<Node> adj;

        public Node()
        {
            possible = new HashSet<int>();
            adj = new HashSet<Node>();
            ResetNode();
        }

        public void ResetNode()
        {
            value = 0;
            possible.Clear();
            for (int i = 1; i <= 9; i++)
            {
                possible.Add(i);
            }
        }

        public void RemovePossible(int p)
        {
            possible.Remove(p);
        }

        public void AddAdj(Node n)
        {
            adj.Add(n);
        }
    }
}
