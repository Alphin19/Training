// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) TRUMPF Metamation India.
// ------------------------------------------------------------------------------------------------
// Program.cs
// Implementation of file name parser using a state machine.
// ------------------------------------------------------------------------------------------------
using System.Text;
using static System.Console;
using static ParserState;

#region Class Program -----------------------------------------------------------------------------
/// <summary>Parses file paths using a state machine and displays the parsing results</summary>
class Program {
   #region Methods --------------------------------------------------
   /// <summary>Tests the file name parser with different inputs and displays the results</summary>
   public static void Main () {
      WriteLine ($"{"INPUT",-35} {"DRIVE",-8} {"FOLDER",-20} {"FILE",-16} {"EXTENSION",-12} STATUS");
      WriteLine (new string ('-', 110));
      int passed = 0, failed = 0;
      foreach (var path in sTestPaths) {
         var (drive, folder, file, extension) = Parse (path);
         bool ok = drive.Length > 0;
         Write ($"{path,-35} {drive,-8} {folder,-20} {file,-16} {extension,-12} ");
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

   #region Implementation -------------------------------------------
   // Parses a file path using a state machine and extracts the drive, folder, file name and extension.
   static (string Drive, string Folder, string File, string Extension) Parse (string input) {
      ParserState state = A;
      Action none = () => { }, todo;
      bool hasFolder = false;
      StringBuilder drive = new (), folder = new (), name = new (), extension = new ();
      foreach (var ch in input) {
         (state, todo) = (state, ch) switch {
            (A, >= 'A' and <= 'Z') => (B, () => drive.Append (ch)),
            (B, ':') => (C, () => drive.Append (ch)),
            (C, '\\') => (D, none),
            (D or E, >= 'A' and <= 'Z') => (E, () => name.Append (ch)),
            (E, '\\') => (D, () => {
               if (folder.Length > 0) folder.Append ('\\');
               folder.Append (name);
               name.Clear ();
               hasFolder = true;
            }
            ),
            (E, '.') when hasFolder && name.Length > 0 => (F, none),
            (F, >= 'A' and <= 'Z') => (G, () => extension.Append (ch)),
            (G, >= 'A' and <= 'Z') => (G, () => extension.Append (ch)),
            _ => (ParserState.Error, none)
         };
         todo ();
         if (state == ParserState.Error) break;
      }
      // Move to success state only when the input ends with a non-empty extension
      if (state == G && extension.Length > 0)
         state = H;
      bool valid = state == H;
      return valid
         ? (drive.ToString (), folder.ToString (), name.ToString (), extension.ToString ())
         : ("", "", "", "");
   }
   #endregion

   #region Private data ---------------------------------------------
   static readonly string[] sTestPaths = {
      @"C:\ABC\DEF\README.TXT", @"C:\ABC\FILE.TXT", @"D:\A\B.C",@"Z:\FOLDER\FILE.EXT",
      @"C:\A\B\C\D\E\FILE.TXT",@"C:\README.TXT",@"C:\ABC\DEF",@"README.TXT",@"C:\ABC\.TXT",
      @"C:\ABC\FILE.",@"C:ABC\FILE.TXT",@"C\ABC\FILE.TXT",@"1:\ABC\FILE.TXT",@"C:\ABC\DEF\",
      @"C:\ABC6\DEF\FILE.TXT",@"C:\MY FOLDER\FILE.TXT",@"C:\ABC\FILE.TXT~",@"CD:\ABC\FILE.TXT",
      @"C:\\ABC\FILE.TXT", @"C:\DIR\\FILE.TXT", @"C:\DIR\ARCHIVE.TAR.GZ",@"C:/ABC/FILE.TXT",
      @"C:\ABC\FILE\TXT", @"C:\ABC\DEF\FILE.TXT.EXTRA",@"C:\ABC\DEF\file.TXT",""
   };
   #endregion
}
#endregion

#region Enum --------------------------------------------------------------------------
/// <summary>Represents the states used to validate and parse a file path</summary>
enum ParserState {
   A,        // Starting State
   B,        // Drive letter
   C,        // Colon
   D,        // BackSlash
   E,        // folder or file name
   F,        // Extension Dot (.)
   G,        // Extension
   H,        // Success
   Error     // Invalid input
}
#endregion