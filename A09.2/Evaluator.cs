// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) TRUMPF Metamation India.
// ------------------------------------------------------------------------------------------------
// Evaluator.cs
// Evaluates mathematical expressions and handles operator precedence and variables
// ------------------------------------------------------------------------------------------------

#region Class EvalException -----------------------------------------------------------------------
class EvalException : Exception {
   #region Constructors ---------------------------------------------
   public EvalException (string message) : base (message) { }
   #endregion
}
#endregion

#region Class Evaluator ---------------------------------------------------------------------------
class Evaluator {
   #region Methods --------------------------------------------------
   /// <summary>Evaluates an expression and returns its result</summary>
   public double Evaluate (string text) {
      mOperands.Clear ();
      mOperators.Clear ();
      List<Token> tokens = new ();
      var tokenizer = new Tokenizer (this, text);
      for (; ; ) {
         var token = tokenizer.Next ();
         if (token is TEnd) break;
         if (token is TError err) throw new EvalException (err.Message);
         tokens.Add (token);
      }
      TVariable? tVariable = null;
      if (tokens.Count > 2 && tokens[0] is TVariable tvar &&
          tokens[1] is TOpArithmetic { Op: '=' }) {
         tVariable = tvar;
         tokens.RemoveRange (0, 2);
      }
      foreach (var t in tokens) Process (t);
      while (mOperators.Count > 0) ApplyOperator ();
      if (mOperands.Count == 0) throw new EvalException ("Too few operands");
      if (mOperands.Count > 1) throw new EvalException ("Too many operands");
      double f = mOperands.Pop ();
      if (tVariable != null) mVars[tVariable.Name] = f;
      return f;
   }

   /// <summary>Returns the value of a previously assigned variable</summary>
   public double GetVariable (string name) {
      if (mVars.TryGetValue (name, out double f)) return f;
      throw new EvalException ($"Unknown variable: {name}");
   }
   #endregion

   #region Implementation -------------------------------------------
   // Process tokens and handles them based on their types
   void Process (Token token) {
      switch (token) {
         case TNumber num:
            mOperands.Push (num.Value);
            break;
         case TOperator op:
            if (op is not (TOpUnary or TOpFunction))
               while (mOperators.Count > 0 && !OkToPush (op)) ApplyOperator ();
            mOperators.Push (op);
            break;
         case TPunctuation: break;
         default: throw new EvalException ($"Unknown token: {token}");
      }
   }

   // Checks whether the incoming operator can be pushed onto the operator stack
   bool OkToPush (TOperator op) {
      var top = mOperators.Peek ();
      return top.Priority < op.Priority ||
             (top.Priority == op.Priority && op is TOpArithmetic { Op: '^' });
   }

   // Applies the top operator using the required operands and stores the result
   void ApplyOperator () {
      var op = mOperators.Pop ();
      if (mOperands.Count == 0) throw new EvalException ("Too few operands");
      var f1 = mOperands.Pop ();
      if (op is TOpUnary unary) mOperands.Push (unary.Evaluate (f1));
      else if (op is TOpFunction func) mOperands.Push (func.Evaluate (f1));
      else if (op is TOpArithmetic arith) {
         if (mOperands.Count == 0) throw new EvalException ("Too few operands");
         var f2 = mOperands.Pop ();
         mOperands.Push (arith.Evaluate (f2, f1));
      }
   }
   #endregion

   #region Private Data ---------------------------------------------
   readonly Dictionary<string, double> mVars = new ();
   readonly Stack<double> mOperands = new ();
   readonly Stack<TOperator> mOperators = new ();
   #endregion
}
#endregion