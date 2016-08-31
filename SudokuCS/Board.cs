using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuCS
{
    class Board
    {
        Node[,] board;
        HashSet<Node> queue;
        int[,] unique;
        decimal solutions;

        public Board()
        {
            board = new Node[9, 9];
            queue = new HashSet<Node>();
            unique = new int[9, 9];
            solutions = 0;
            CreateLinks();
        }

        private void ResetBoard()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    board[i, j].ResetNode();
                }
            }
            queue.Clear();
            solutions = 0;
        }

        private void CreateLinks()
        {
            for(int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    board[i, j] = new Node();
                }
            }
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    for (int k = 0; k < 9; k++)
                    {
                        if (i != k)
                        {
                            board[i, j].AddAdj(board[k, j]);
                        }
                        if (j != k)
                        {
                            board[i, j].AddAdj(board[i, k]);
                        }
                    }
                    switch(i % 3)
                    {
                        case 0:
                            switch (j % 3)
                            {
                                case 0:
                                    CreateLinksInsideSquare(board[i, j], i + 1, i + 2, j + 1, j + 2);
                                    break;
                                case 1:
                                    CreateLinksInsideSquare(board[i, j], i + 1, i + 2, j - 1, j + 1);
                                    break;
                                case 2:
                                    CreateLinksInsideSquare(board[i, j], i + 1, i + 2, j - 1, j - 2);
                                    break;
                            }
                            break;
                        case 1:
                            switch (j % 3)
                            {
                                case 0:
                                    CreateLinksInsideSquare(board[i, j], i - 1, i + 1, j + 1, j + 2);
                                    break;
                                case 1:
                                    CreateLinksInsideSquare(board[i, j], i - 1, i + 1, j - 1, j + 1);
                                    break;
                                case 2:
                                    CreateLinksInsideSquare(board[i, j], i - 1, i + 1, j - 1, j - 2);
                                    break;
                            }
                            break;
                        case 2:
                            switch (j % 3)
                            {
                                case 0:
                                    CreateLinksInsideSquare(board[i, j], i - 1, i - 2, j + 1, j + 2);
                                    break;
                                case 1:
                                    CreateLinksInsideSquare(board[i, j], i - 1, i - 2, j - 1, j + 1);
                                    break;
                                case 2:
                                    CreateLinksInsideSquare(board[i, j], i - 1, i - 2, j - 1, j - 2);
                                    break;
                            }
                            break;
                    }
                }
            }
        }

        private void CreateLinksInsideSquare(Node n, int i1, int i2, int j1, int j2)
        {
            n.AddAdj(board[i1, j1]);
            n.AddAdj(board[i1, j2]);
            n.AddAdj(board[i2, j1]);
            n.AddAdj(board[i2, j2]);
        }

        public void PrepareBoard(string v)
        {
            ResetBoard();
            v = v.Replace(" ", "").Replace(System.Environment.NewLine, "").Replace("\n", "");
            for (int i = 0; i < v.Length; i++)
            {
                board[i / 9, i % 9].value = Int32.Parse(v[i] + "");
                if (board[i / 9, i % 9].value == 0)
                {
                    queue.Add(board[i / 9, i % 9]);
                }
                else
                {
                    foreach (Node n in board[i / 9, i % 9].adj) {
                        n.RemovePossible(board[i / 9, i % 9].value);
                    }
                }
            }
            SolveBoard();
        }

        private void FoundNewSolution()
        {
            if (++solutions == 1)
            {
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        unique[i, j] = board[i, j].value;
                    }
                }
            }
        }

        public string CreateOutput()
        {
            StringBuilder s = new StringBuilder();
            s.Append(solutions + System.Environment.NewLine);
            if (solutions == 1) {
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        s.Append(unique[i, j] + " ");
                    }
                    s.Append(unique[i, 8] + System.Environment.NewLine);
                }
            }
            return s.ToString();
        }

        private void SolveBoard()
        {
            if (queue.Count == 0)
            {
                FoundNewSolution();
            } else
            {
                Node n = FindMinimum();
                if (n != null && n.possible.Count > 0)
                {
                    queue.Remove(n);
                    HashSet<Node> restoreAdj = new HashSet<Node>();
                    foreach(int v in n.possible.ToArray())
                    {
                        n.value = v;
                        foreach(Node adj in n.adj)
                        {
                            if (adj.possible.Contains(v) && adj.value == 0)
                            {
                                adj.possible.Remove(v);
                                restoreAdj.Add(adj);
                            }
                        }
                        SolveBoard();
                        foreach(Node adj in restoreAdj)
                        {
                            adj.possible.Add(v);
                        }
                        restoreAdj.Clear();
                    }
                    queue.Add(n);
                    n.value = 0;
                }
            }
        }

        private Node FindMinimum()
        {
            Node temp = null;
            int min = 99;
            foreach(Node n in queue)
            {
                if (n.possible.Count < min) {
                    temp = n;
                    min = temp.possible.Count;
                }
            }
            return temp;
        }
    }
}
