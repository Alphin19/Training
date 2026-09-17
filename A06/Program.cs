using System.Text;
class Program {
   static int n = 8;
   static void Main () {
      Console.OutputEncoding = Encoding.UTF8;
      Console.Write ("Enter A for all solutions or U for unique solutions: ");
      string choice = Console.ReadLine ().ToUpper ();
      int[] board = new int[n];
      List<int[]> solutions = [];
      Solve (board, 0, solutions);
      if (choice == "A") {
         Console.WriteLine ($"\nTotal solutions: {solutions.Count}");
         for (int i = 0; i < solutions.Count; i++) {
            Console.WriteLine ($"\nSolution {(i + 1)}");
            PrintBoard (solutions[i]);
         }
      } else if (choice == "U") {
         List<int[]> uniqueSolutions = GetUniqueSolutions (solutions);
         Console.WriteLine ($"\nUnique solutions: {uniqueSolutions.Count}");
         for (int i = 0; i < uniqueSolutions.Count; i++) {
            Console.WriteLine ($"\nUnique Solution {(i + 1)}");
            PrintBoard (uniqueSolutions[i]);
         }
      } else Console.WriteLine ("Invalid choice.Enter A or U");
   }
   static void Solve (int[] board, int row, List<int[]> solutions) {
      if (row == n) {
         solutions.Add ((int[])board.Clone ());
         return;
      }
      for (int col = 0; col < n; col++) {
         if (IsSafe (board, row, col)) {
            board[row] = col;
            Solve (board, row + 1, solutions);
         }
      }
   }

   static bool IsSafe (int[] board, int row, int col) {
      for (int r = 0; r < row; r++)
         if (board[r] == col || Math.Abs (r - row) == Math.Abs (board[r] - col)) return false;
      return true;
   }
   static void PrintBoard (int[] board) {
      string horizontal = "+----+----+----+----+----+----+----+----+";
      Console.WriteLine (horizontal);
      for (int row = 0; row < n; row++) {
         Console.Write ("|");
         for (int col = 0; col < n; col++) {
            if (board[row] == col) Console.Write (" ♛  |");
            else Console.Write ("    |");
         }
         Console.WriteLine ();
         Console.WriteLine (horizontal);
      }
   }
   static List<int[]> GetUniqueSolutions (List<int[]> solutions) {
      List<int[]> uniqueSolutions = [];
      HashSet<string> seen = [];
      foreach (int[] solution in solutions) {
         List<string> variants = GetVariants (solution);
         string smallest = variants[0];
         foreach (string variant in variants)
            if (variant.CompareTo (smallest) < 0) smallest = variant;
         if (seen.Add (smallest)) uniqueSolutions.Add (solution);
      }
      return uniqueSolutions;
   }

   static List<string> GetVariants (int[] board) {
      List<string> variants = [];
      int[] current = (int[])board.Clone ();
      for (int i = 0; i < 4; i++) {
         variants.Add (ToString (current));
         variants.Add (ToString (Mirror (current)));
         current = Rotate90 (current);
      }
      return variants;
   }
   static int[] Rotate90 (int[] board) {
      int[] rotated = new int[n];
      for (int row = 0; row < n; row++) {
         int col = board[row];
         rotated[col] = n - 1 - row;
      }
      return rotated;
   }
   static int[] Mirror (int[] board) => [.. board.Reverse ()];
   static string ToString (int[] board) {
      string result = "";
      for (int i = 0; i < n; i++) result += board[i] + ",";
      return result;
   }
}