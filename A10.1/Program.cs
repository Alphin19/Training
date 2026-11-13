
// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) TRUMPF Metamation India.
// ------------------------------------------------------------------------------------------------
// Program.cs
// Tests the implementation of file name parser using a state machine.
// ------------------------------------------------------------------------------------------------
using static System.Console;

#region class program -----------------------------------------------------------------------------
class Program {
   #region Methods --------------------------------------------------
   /// <summary>Tests the file name parser with different inputs and displays the results</summary>
   public static void Main () {
      WriteLine ($"{"INPUT",-35} {"EXPECTED",-40} {"ACTUAL",-40} STATUS");
      WriteLine (new string ('-', 125));
      int passed = 0, failed = 0;
      foreach (var (input, expected) in sCases) {
         var result = FileNameParser.Parse (input);
         bool ok = result == expected;
         string expectedText = expected?.ToString () ?? "null";
         string actualText = result?.ToString () ?? "null";
         Write ($"{input,-35} {expectedText,-40} {actualText,-40} ");
         ForegroundColor = ok ? ConsoleColor.Green : ConsoleColor.Red;
         WriteLine (ok ? "PASS" : "FAIL");
         ResetColor ();
         if (ok) passed++;
         else failed++;
      }
      WriteLine (new string ('-', 125));
      WriteLine ($"Total: {passed + failed} | Passed: {passed} | Failed: {failed}");
   }
   #endregion

   #region Private Data ---------------------------------------------
   static readonly (string Input, (string, string, string, string)? Expected)[] sCases = [
      (@"C:\ABC\FILE.TXT", ("C:", "ABC", "FILE", "TXT")),
      (@"D:\A\B.C", ("D:", "A", "B", "C")),
      (@"Z:\FOLDER\FILE.EXT", ("Z:", "FOLDER", "FILE", "EXT")),
      (@"C:\README.TXT", null),
      (@"C:\ABC\DEF\README.TXT", null),
      (@"C:\ABC\DEF", null),
      (@"README.TXT", null),
      (@"C:\ABC\.TXT", null),
      (@"C:\ABC\FILE.", null),
      (@"C:ABC\FILE.TXT", null),
      (@"C\ABC\FILE.TXT", null),
      (@"1:\ABC\FILE.TXT", null),
      (@"C:\ABC\", null),
      (@"C:\ABC6\FILE.TXT", null),
      (@"C:\MY FOLDER\FILE.TXT", null),
      (@"C:\ABC\FILE.TXT~", null),
      (@"CD:\ABC\FILE.TXT", null),
      (@"C:\\ABC\FILE.TXT", null),
      (@"C:\DIR\\FILE.TXT", null),
      (@"C:\DIR\ARCHIVE.TAR.GZ", null),
      (@"C:/ABC/FILE.TXT", null),
      (@"C:\ABC\FILE\TXT", null),
      (@"C:\ABC\FILE.TXT.EXTRA", null),
      (@"C:\ABC\file.TXT", null),
      ("", null)
   ];
   #endregion
}
#endregion