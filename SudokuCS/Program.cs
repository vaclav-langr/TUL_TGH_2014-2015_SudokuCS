using System;
using System.Diagnostics;

namespace SudokuCS
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch s = new Stopwatch();
            s.Start();
            //int pocet_vstupu = Int32.Parse(Console.ReadLine());
            Board b = new Board();
            b.PrepareBoard(ReadInput());
            //for(int i = 0; i < pocet_vstupu; i++)
            //{

            //}
            Console.Write(b.CreateOutput());
            s.Stop();
            Console.WriteLine(s.ElapsedMilliseconds + " ms");
            Console.Read();
        }

        public static string ReadInput()
        {
            string s = "";
            //s = s + "0 0 0 0 0 0 0 0 0\n";
            //s = s + "0 0 0 0 0 3 0 8 5\n";
            //s = s + "0 0 1 0 2 0 0 0 0\n";

            //s = s + "0 0 0 5 0 7 0 0 0\n";
            //s = s + "0 0 4 0 0 0 1 0 0\n";
            //s = s + "0 9 0 0 0 0 0 0 0\n";

            //s = s + "5 0 0 0 0 0 0 7 3\n";
            //s = s + "0 0 2 0 1 0 0 0 0\n";
            //s = s + "0 0 0 0 4 0 0 0 9\n";
            s = s + "1 0 0 0 0 7 0 9 0\n";
            s = s + "0 3 0 0 2 0 0 0 8\n";
            s = s + "0 0 9 6 0 0 5 0 0\n";

            s = s + "0 0 5 3 0 0 9 0 0\n";
            s = s + "0 1 0 0 8 0 0 0 2\n";
            s = s + "6 0 0 0 0 4 0 0 0\n";

            s = s + "3 0 0 0 0 0 0 1 0\n";
            s = s + "0 4 0 0 0 0 0 0 7\n";
            s = s + "0 0 7 0 0 0 3 0 0\n";
            return s;
        }
    }
}
