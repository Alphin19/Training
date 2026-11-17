// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) TRUMPF Metamation India.
// ------------------------------------------------------------------------------------------------
// Program.cs
// Tests the file name parser using different inputs.
// ------------------------------------------------------------------------------------------------
using static System.Console;

#region class Program -----------------------------------------------------------------------------
/// <summary>Tests the file name parser and displays the parsing results</summary>
class Program {
   #region Methods --------------------------------------------------
   /// <summary>Tests the parser with different inputs and displays the results</summary>
   public static void Main () {
      WriteLine ($"{"INPUT",-35} {"DRIVE",-8} {"FOLDER",-20} {"FILE",-16} {"EXTENSION",-12} STATUS");
      WriteLine (new string ('-', 110));
      int passed = 0, failed = 0;
      foreach (var (path, expected) in sTestPaths) {
         var actual = FilenameParser.Parse (path);
         bool ok = Equals (actual, expected);
         Write ($"{path,-35} ");
         if (actual is { } result)
            Write ($"{result.Drive,-8} {result.Folder,-20} {result.File,-16} {result.Extension,-12} ");
         else
            Write ($"{"",-8} {"",-20} {"",-16} {"",-12} ");
         ForegroundColor = ok ? ConsoleColor.Green : ConsoleColor.Red;
         WriteLine (ok ? "PASS" : "FAIL");
         ResetColor ();
         if (ok) passed++;
         else failed++;
      }
      WriteLine (new string ('-', 110));
      WriteLine ($"Total: {passed + failed} | Passed: {passed} | Failed: {failed}");
   }
   #endregion

   #region Private Data ---------------------------------------------
   static readonly (string Input, (string Drive, string Folder, string File, string Extension)?
      Expected)[] sTestPaths = [ (@"C:\ABC\DEF\README.TXT", ("C:", @"ABC\DEF", "README", "TXT")),
      (@"C:\ABC\FILE.TXT", ("C:", "ABC", "FILE", "TXT")),
      (@"D:\A\B.C", ("D:", "A", "B", "C")),
      (@"Z:\FOLDER\FILE.EXT", ("Z:", "FOLDER", "FILE", "EXT")),
      (@"C:\A\B\C\D\E\FILE.TXT", ("C:", @"A\B\C\D\E", "FILE", "TXT")),
      (@"C:\README.TXT",null),
      (@"C:\ABC\DEF", null),
      (@"README.TXT", null),
      (@"C:\ABC\.TXT", null),
      (@"C:\ABC\FILE.", null),
      (@"C:ABC\FILE.TXT", null),
      (@"C\ABC\FILE.TXT", null),
      (@"1:\ABC\FILE.TXT", null),
      (@"C:\ABC\DEF\", null),
      (@"C:\ABC6\DEF\FILE.TXT", null),
      (@"C:\MY FOLDER\FILE.TXT", null),
      (@"C:\ABC\FILE.TXT~", null),
      (@"CD:\ABC\FILE.TXT", null),
      (@"C:\\ABC\FILE.TXT", null),
      (@"C:\DIR\\FILE.TXT", null),
      (@"C:\DIR\ARCHIVE.TAR.GZ", null),
      (@"C:/ABC/FILE.TXT", null),
      (@"C:\ABC\FILE\TXT", null),
      (@"C:\ABC\DEF\FILE.TXT.EXTRA", null),
      (@"C:", null),
      ("", null)
   ];
   #endregion
}
#endregion