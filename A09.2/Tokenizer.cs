// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) TRUMPF Metamation India.
// ------------------------------------------------------------------------------------------------
// Tokenizer.cs
// Converts mathematical expressions into tokens for evaluation
// ------------------------------------------------------------------------------------------------
#region Class Tokenizer----------------------------------------------------------------------------
/// <summary>Reads mathematical expressions and converts them into tokens</summary>
class Tokenizer {
   #region Constructors ---------------------------------------------
   public Tokenizer (Evaluator eval, string text) {
      mText = text; mN = 0; mEval = eval;
   }
   #endregion

   #region Properties------------------------------------------------
   // Each open bracket adds 10 to the priority of the operators inside it
   int Level => mDepth * 10;
   #endregion

   #region Methods --------------------------------------------------
   /// <summary> Returns the next token and remembers it as the previous token </summary>
   public Token Next () => mPrevious = Read ();
   #endregion

   #region Implementations ------------------------------------------
   // Reads the input character by character and identifies the corresponding token
   Token Read () {
      while (mN < mText.Length) {
         char ch = char.ToLower (mText[mN++]);
         switch (ch) {
            case ' ' or '\t': continue;
            case (>= '0' and <= '9') or '.': return GetNumber ();
            case '(': mDepth++; return new TPunctuation (ch);
            case ')':
               if (mDepth == 0) return new TError ("Parenthesis mismatched");
               mDepth--; return new TPunctuation (ch);
            case '+' or '-' or '*' or '/' or '^' or '=': return GetOperator (ch);
            case >= 'a' and <= 'z': return GetIdentifier ();
            default: return new TError ($"Unknown symbol: {ch}");
         }
      }
      return mDepth > 0 ? new TError ("Parenthesis mismatched") : new TEnd ();
   }

   // Identifies whether the operator is unary or arithmetic and creates the appropriate token
   Token GetOperator (char ch) {
      bool isUnary = (ch is '+' or '-') && mPrevious is not (TNumber or TPunctuation { Punct: ')' });
      if (isUnary) return new TOpUnary (mEval, ch, Level);
      return new TOpArithmetic (mEval, ch, Level);
   }

   // Reads an identifier and checks whether it represents a supported function or variable
   Token GetIdentifier () {
      int start = mN - 1;
      while (mN < mText.Length) {
         char ch = char.ToLower (mText[mN++]);
         if (ch is >= 'a' and <= 'z') continue;
         mN--; break;
      }
      string sub = mText[start..mN];
      if (mFuncs.Contains (sub)) return new TOpFunction (mEval, sub, Level);
      else return new TVariable (mEval, sub);
   }

   // Reads a numeric value from the input and creates a literal token
   Token GetNumber () {
      int start = mN - 1;
      while (mN < mText.Length) {
         char ch = mText[mN++];
         if (ch is (>= '0' and <= '9') or '.') continue;
         mN--; break;
      }
      string number = mText.Substring (start, mN - start);
      double f = double.Parse (number);
      return new TLiteral (f);
   }
   #endregion

   #region Private Data----------------------------------------------
   readonly Evaluator mEval;
   readonly string mText;
   int mN;
   int mDepth;
   Token? mPrevious;
   readonly string[] mFuncs = { "sin", "cos", "tan", "sqrt", "log", "exp", "asin", "acos", "atan" };
   #endregion
}
#endregion