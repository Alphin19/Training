
// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) TRUMPF Metamation India.
// ------------------------------------------------------------------------------------------------
// FileNameParser.cs
// Implementation of file name parser using a state machine.
// ------------------------------------------------------------------------------------------------
using System.Text;
using static EParserState;

#region class FileNameParser ----------------------------------------------------------------------
/// <summary>Parses a file path using a state machine</summary>
static class FileNameParser {
   #region Methods --------------------------------------------------
   /// <summary>Returns the drive, folder, file name and extension, or null if the path is invalid</summary>
   public static (string Drive, string Folder, string File, string Extension)? Parse (string input) {
      EParserState state = A;
      Action none = () => { }, action;
      StringBuilder drive = new (), folder = new (), name = new (), extension = new ();
      foreach (var ch in input) {
         (state, action) = (state, ch) switch {
            (A, >= 'A' and <= 'Z') => (B, () => drive.Append (ch)),
            (B, ':') => (C, () => drive.Append (ch)),
            (C, '\\') => (D, none),
            (D, >= 'A' and <= 'Z') => (E, () => name.Append (ch)),
            (E, >= 'A' and <= 'Z') => (E, () => name.Append (ch)),
            (E, '\\') => (F, () => {
               folder.Append (name);
               name.Clear ();
            }
            ),
            (F or G, >= 'A' and <= 'Z') => (G, () => name.Append (ch)),
            (G, '.') => (H, none),
            (H or I, >= 'A' and <= 'Z') => (I, () => extension.Append (ch)),
            _ => (Error, none)
         };
         action ();
         if (state == Error) break;
      }
      // Input must end inside the extension to be accepted
      if (state == I) state = J;
      return state == J
         ? (drive.ToString (), folder.ToString (), name.ToString (), extension.ToString ())
         : null;
   }
   #endregion
}
#endregion

#region enum EParserState -------------------------------------------------------------------------
/// <summary>States of the file-name state machine</summary>
enum EParserState {
   A,        // Start: expects the drive letter
   B,        // Drive letter read: expects ':'
   C,        // ':' read: expects '\'
   D,        // '\' read: expects the directory name
   E,        // Reading directory: expects A..Z or '\'
   F,        // Directory completed: expects the filename
   G,        // Reading filename: expects A..Z or '.'
   H,        // '.' read: expects the extension
   I,        // Reading extension: expects A..Z or end of input
   J,        // Accepted
   Error     // Rejected
}
#endregion
