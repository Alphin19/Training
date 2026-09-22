// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2026.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------------------------------------
// Program.cs
// 8 Queens - Find all and unique solutions
// ------------------------------------------------------------------------------------------------
using System.Text;

#region Program -----------------------------------------------------------------------------------
class Program {
   static int N = 8;
   static void Main () {
      Console.OutputEncoding = Encoding.UTF8;
      Console.Write ("Enter A for all solutions or U for unique solutions: ");
      string choice = Console.ReadLine ()?.ToUpper () ?? "";
      int[] board = new int[N];
      List<int[]> solns = [];
      Solve (board, 0, solns);
      if (choice == "A") {
         Console.WriteLine ($"\nTotal solutions: {solns.Count}");
         for (int i = 0; i < solns.Count; i++) {
            Console.WriteLine ($"\nSolution {(i + 1)}");
            PrintBoard (solns[i]);
         }
      } else if (choice == "U") {
         List<int[]> uniSoln = GetUniqueSolutions (solns);
         Console.WriteLine ($"\nUnique solutions: {uniSoln.Count}");
         for (int i = 0; i < uniSoln.Count; i++) {
            Console.WriteLine ($"\nUnique Solution {(i + 1)}");
            PrintBoard (uniSoln[i]);
         }
      } else Console.WriteLine ("Invalid choice.Enter A or U");
   }

   #region - Implementation -----------------------------------------
   //Find unique solns
   static List<int[]> GetUniqueSolutions (List<int[]> solutions) {
      List<int[]> uniSlns = [];
      HashSet<string> seen = [];
      foreach (int[] soln in solutions) {
         List<string> v = GetVariants (soln);
         string smallest = v[0];
         foreach (string variant in v)
            if (variant.CompareTo (smallest) < 0) smallest = variant;
         if (seen.Add (smallest)) uniSlns.Add (soln);
      }
      return uniSlns;
   }

   // Generate all rotation and mirror variations
   static List<string> GetVariants (int[] board) {
      List<string> variants = [];
      int[] current = (int[])board.Clone ();
      for (int i = 0; i < 4; i++) {
         variants.Add (ToString (current));
         variants.Add (ToString (Mirror (current)));
         current = Rotate90 (current);
      }
      return variants;

      // Rotating board by 90 degree
      int[] Rotate90 (int[] board) {
         int[] r = new int[N];
         for (int row = 0; row < N; row++) {
            int col = board[row];
            r[col] = N - 1 - row;
         }
         return r;
      }

      // Mirror the soln
      int[] Mirror (int[] board) => [.. board.Reverse ()];

      // Converts array to string
      string ToString (int[] board) => string.Join (",", board);
   }

   // Solving 8x8 queens using backtracking
   static void Solve (int[] board, int row, List<int[]> solutions) {
      if (row == N) {
         solutions.Add ((int[])board.Clone ());
         return;
      }
      for (int col = 0; col < N; col++) {
         if (IsSafe (board, row, col)) {
            board[row] = col;
            Solve (board, row + 1, solutions);
         }
      }

      // Check whether the position is safe
      static bool IsSafe (int[] board, int row, int col) {
         for (int r = 0; r < row; r++)
            if (board[r] == col || Math.Abs (r - row) == Math.Abs (board[r] - col)) return false;
         return true;
      }
   }

   // Displays the board for the given soln.
   static void PrintBoard (int[] solution) {
      Console.WriteLine (Border (TOP));
      for (int i = 0; i < S; i++) {
         int placedQueen = solution[i];
         Console.Write (VERTICAL);
         for (int j = 0; j < S; j++) {
            Console.Write (placedQueen == j ? QUEEN : EMPTY);
            Console.Write (VERTICAL);
         }
         Console.WriteLine ();
         if (i < S - 1) Console.WriteLine (Border (MID));
      }
      Console.WriteLine (Border (BOTTOM));

      // Builds a horizontal border line from the given corner and joint characters.
      static string Border (string pattern)
         => pattern[0] + string.Join (pattern[0], Enumerable.Repeat (HORIZONTAL, S)) + pattern[2];
   }
   #endregion

   #region Constants ------------------------------------------------
   const string TOP = "┌┬┐";
   const string MID = "├┼┤";
   const string BOTTOM = "└┴┘";
   const string VERTICAL = "│";
   const string HORIZONTAL = "────";
   const string EMPTY = "    ";
   const string QUEEN = " ♕  ";
   const int S = 8;
   #endregion
}
#endregion