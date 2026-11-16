// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) TRUMPF Metamation India.
// ------------------------------------------------------------------------------------------------
// FilenameParser.cs
// Implementation of file name parser using a state machine.
// ------------------------------------------------------------------------------------------------
using System.Text;
using static EParserState;

#region Class FilenameParser ----------------------------------------------------------------------
/// <summary>Parses file paths using a state machine</summary>
class FilenameParser {
   #region Methods --------------------------------------------------
   /// <summary>Parses a file path and extracts the drive, folder, file name and extension</summary>
   public static (string Drive, string Folder, string File, string Extension)? Parse (string input) {
      if (input is null) return null;
      EParserState state = A;
      Action none = () => { }, action;
      StringBuilder drive = new (), folder = new (), name = new (), extension = new ();
      foreach (var ch in input + '~') {
         (state, action) = (state, ch) switch {
            (A, >= 'A' and <= 'Z') => (B, () => drive.Append (ch)),
            (B, ':') => (C, () => drive.Append (ch)),
            (C, '\\') => (D, none),
            (D or E, >= 'A' and <= 'Z') => (E, () => name.Append (ch)),
            (E, '\\') => (D, () => {
               folder.Append (folder.Length > 0 ? "\\" : "").Append (name); name.Clear ();
            }
            ),
            (E, '.') => (F, none),
            (F or G, >= 'A' and <= 'Z') => (G, () => extension.Append (ch)),
            (G, '~') => (H, none),
            _ => (Error, none)
         };
         action ();
         if (state == Error) break;
      }
      return state == H && folder.Length > 0
   ? (drive.ToString (), folder.ToString (), name.ToString (), extension.ToString ()) : null;
   }
   #endregion
}
#endregion

#region enum EParserState  ------------------------------------------------------------------------
/// <summary>States of the file-name state machine; A..H match the labels in the state diagram</summary>
enum EParserState {
   A,        // Start: expects the drive letter
   B,        // Drive letter read: expects ':'
   C,        // ':' read: expects '\'
   D,        // '\' read: expects a folder or file name
   E,        // Inside a folder or file name: expects A..Z, '\' or '.'
   F,        // '.' read: expects the extension
   G,        // Inside the extension: expects A..Z or the end marker '~'
   H,        // Accepted
   Error     // Rejected
}
#endregion