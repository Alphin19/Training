// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) TRUMPF Metamation India.
// ------------------------------------------------------------------------------------------------
// FilenameParser.cs
// Implementation of file name parser using a state machine.
// ------------------------------------------------------------------------------------------------
using static EParserState;

#region Class FilenameParser ----------------------------------------------------------------------
/// <summary>Parses file paths using a state machine</summary>
class FilenameParser {
   #region Methods --------------------------------------------------
   /// <summary>Parses a file path and extracts the drive, folder, file name and extension</summary>
   public static (string Drive, string Folder, string File, string Extension)? Parse (string input) {
      EParserState state = A;
      Action none = () => { }, action;
      string drive = "", folder = "", name = "", extension = "";
      foreach (var ch in input + '~') {
         (state, action) = (state, ch) switch {
            (A, >= 'A' and <= 'Z') => (B, () => drive += ch),
            (B, ':') => (C, () => drive += ch),
            (C, '\\') => (D, none),
            (D or E, >= 'A' and <= 'Z') => (E, () => name += ch),
            (E, '\\') => (D, () => (folder, name) = (folder.Length > 0 ? $"{folder}\\{name}" : name, "")),
            (E, '.') => (F, none),
            (F or G, >= 'A' and <= 'Z') => (G, () => extension += ch),
            (G, '~') => (H, none),
            _ => (Error, none)
         };
         action ();
         if (state == Error) return null;
      }
      return state == H && folder.Length > 0 ? (drive, folder, name, extension) : null;
   }
}

#region enum EParserState  ---------------------------------------------------------------------
/// <summary>States A–H of the file-name parser, corresponding to the state diagram</summary>
enum EParserState {
   A,        // Start: expects the drive letter
   B,        // Drive letter read: expects ':'
   C,        // ':' read: expects '\'States of the file-name state machine
   D,        // '\' read: expects a folder or file name
   E,        // Inside a folder or file name: expects A..Z, '\' or '.'
   F,        // '.' read: expects the extension
   G,        // Inside the extension: expects A..Z or the end marker '~'
   H,        // Accepted
   Error     // Rejected
}

#endregion
#endregion
#endregion