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
      List<int[]> slns = [];
      Solve (board, 0, slns);
      if (choice == "A") {
         PrintLine ($"\nTotal solutions: {slns.Count}");
         for (int i = 0; i < slns.Count; i++) {
            PrintLine ($"\nSolution {i + 1}");
            PrintBoard (slns[i]);
         }
      } else if (choice == "U") {
         List<int[]> unique = GetUniqueSolutions (slns);
         PrintLine ($"\nUnique solutions: {unique.Count}");
         for (int i = 0; i < unique.Count; i++) {
            PrintLine ($"\nUnique Solution {i + 1}");
            PrintBoard (unique[i]);
         }
      } else {
         PrintLine ("Invalid choice. Enter A or U");
      }
   }

   #region Implementations ------------------------------------------
   // Find unique solutions
   static List<int[]> GetUniqueSolutions (List<int[]> slns) {
      List<int[]> unique = [];
      HashSet<string> seen = [];
      foreach (int[] sln in slns) {
         List<string> variants = GetVariants (sln);
         // Use the smallest variant as the canonical representation
         string smallest = variants[0];
         foreach (string v in variants)
            if (v.CompareTo (smallest) < 0) smallest = v;
         if (seen.Add (smallest)) unique.Add (sln);
      }
      return unique;
   }

   // Generate all rotation and mirror variations
   static List<string> GetVariants (int[] board) {
      List<string> variants = [];
      int[] clone = (int[])board.Clone ();
      for (int i = 0; i < 4; i++) {
         variants.Add (clone.ToS ());
         variants.Add (clone.Mirror ().ToS ());
         clone = clone.Rotate90 ();
      }
      return variants;
   }

   // Solving 8x8 queens using backtracking
   static void Solve (int[] board, int row, List<int[]> slns) {
      if (row == N) {
         slns.Add ((int[])board.Clone ());
         return;//Base condition for recursion
      }
      for (int col = 0; col < N; col++) {
         if (IsSafe (board, row, col)) {
            board[row] = col;
            Solve (board, row + 1, slns);//Backtracking through recursion
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
         int queen = solution[i];
         Print (VERTICAL);
         for (int j = 0; j < N; j++) {
            Print (queen == j ? QUEEN : EMPTY);
            Print (VERTICAL);
         }
         PrintLine ();
         if (i < N - 1) PrintLine (Border (MID));
      }
      PrintLine (Border (BOTTOM));
   }

   // Builds a horizontal border line from the given pattern
   static string Border (string pattern) {
      if (pattern.Length < 3)
         throw new ArgumentException ("Border pattern must contain 3 characters.");
      return pattern[0] + string.Join (pattern[1], Enumerable.Repeat (HORIZONTAL, N)) + pattern[2];
   }

   // Prints str without moving to the next line
   static void Print (string str) => Console.Write (str);

   // Prints str and moves to the next line
   static void PrintLine (string str = "") => Console.WriteLine (str);
   #endregion

   #region Constants ------------------------------------------------
   const string TOP = "┌┬┐";
   const string MID = "├┼┤";
   const string BOTTOM = "└┴┘";
   const string VERTICAL = "│";
   const string HORIZONTAL = "────";
   const string EMPTY = "    ";
   const string QUEEN = " ♕  ";
   const int N = 8;
   #endregion
}
#endregion

#region Extensions -----------------------------------------------------------------------------

// Extension methods must be declared in a top-level static class
static class BoardExtensions {
   // Rotates the board by 90 degrees
   public static int[] Rotate90 (this int[] arr) {
      int n = arr.Length;
      int[] rot = new int[n];
      for (int i = 0; i < n; i++) {
         int col = arr[i];
         rot[col] = n - 1 - i;
      }
      return rot;
   }

   // Creates a mirrored version of the solution
   public static int[] Mirror (this int[] board) => [.. board.Reverse ()];

   // Converts the solution into a string
   public static string ToS (this int[] board) => string.Join (",", board);
}
#endregion