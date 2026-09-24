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
   static void Main () {
      Console.OutputEncoding = Encoding.UTF8;
      Print ("Enter A for all solutions or U for unique solutions: ");
      string choice = Console.ReadLine ()?.ToUpper () ?? "";
      int[] board = new int[N];
      List<int[]> solns = [];
      Solve (board, 0, solns);
      if (choice == "A") {
         PrintLine ($"\nTotal solutions: {solns.Count}");
         for (int i = 0; i < solns.Count; i++) {
            PrintLine ($"\nSolution {(i + 1)}");
            PrintBoard (solns[i]);
         }
      } else if (choice == "U") {
         List<int[]> unique = GetUniqueSolutions (solns);
         PrintLine ($"\nUnique solutions: {unique.Count}");
         for (int i = 0; i < unique.Count; i++) {
            PrintLine ($"\nUnique Solution {(i + 1)}");
            PrintBoard (unique[i]);
         }
      } else {
         PrintLine ("Invalid choice. Enter A or U");
      }
   }

   #region Implementations -----------------------------------------
   // Find unique solutions
   static List<int[]> GetUniqueSolutions (List<int[]> solutions) {
      List<int[]> unique = [];
      HashSet<string> seen = [];
      foreach (int[] sln in solutions) {
         List<string> variants = GetVariants (sln);
         string smallest = variants[0];
         foreach (string v in variants)
            if (v.CompareTo (smallest) < 0)
               smallest = v;
         if (seen.Add (smallest))
            unique.Add (sln);
      }
      return unique;
   }

   // Generate all rotation and mirror variations
   static List<string> GetVariants (int[] board) {
      List<string> variants = [];
      int[] current = (int[])board.Clone ();
      for (int i = 0; i < 4; i++) {
         variants.Add (ToS (current));
         variants.Add (ToS (Mirror (current)));
         current = Rotate90 (current);
      }
      return variants;

      #region Helper Methods --------------------
      // Rotating board by 90 degree
      int[] Rotate90 (int[] board) {
         int[] rot = new int[N];
         for (int row = 0; row < N; row++) {
            int col = board[row];
            rot[col] = N - 1 - row;
         }
         return rot;
      }

      // Mirror the solution
      int[] Mirror (int[] board) => [.. board.Reverse ()];

      // Converts array to string
      string ToS (int[] board) => string.Join (",", board);
      #endregion
   }

   // Solving 8x8 queens using backtracking
   static void Solve (int[] board, int row, List<int[]> solutions) {
      if (row == N) {
         solutions.Add ((int[])board.Clone ());
         return; // Base condition for recursion
      }
      for (int col = 0; col < N; col++) {
         if (IsSafe (board, row, col)) {
            board[row] = col;
            Solve (board, row + 1, solutions); // Backtracking through recursion
         }
      }

      // Check whether the position is safe
      static bool IsSafe (int[] board, int row, int col) {
         for (int r = 0; r < row; r++)
            if (board[r] == col || Math.Abs (r - row) == Math.Abs (board[r] - col)) return false;
         return true;
      }
   }

   // Displays the board for the given solution
   static void PrintBoard (int[] solution) {
      PrintLine (Border (TOP));
      for (int i = 0; i < N; i++) {
         int placedQueen = solution[i];
         Print (VERTICAL);
         for (int j = 0; j < N; j++) {
            Print (placedQueen == j ? QUEEN : EMPTY);
            Print (VERTICAL);
         }
         PrintLine ("");
         if (i < N - 1) PrintLine (Border (MID));
      }
      PrintLine (Border (BOTTOM));

      // Builds a horizontal border line from the given corner and joint characters.
      static string Border (string pattern)
         => pattern[0] + string.Join (pattern[1], Enumerable.Repeat (HORIZONTAL, N)) + pattern[2];
   }
   // Prints text without moving to the next line.
   static void Print (string text) => Console.Write (text);

   // Prints text and moves to the next line.
   static void PrintLine (string text) => Console.WriteLine (text);
   #endregion

   #region Constants ------------------------------------------------
   const string TOP = "┌┬┐";
   const string MID = "├┼┤";
   const string BOTTOM = "└┴┘";
   const string VERTICAL = "│";
   const string HORIZONTAL = "────";
   const string EMPTY = "    ";
   const string QUEEN = " ♕  ";
   const int N = 8; // Board Size
   #endregion
}
#endregion