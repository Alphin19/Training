// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) TRUMPF Metamation India.
// ------------------------------------------------------------------------------------------------
// Token.cs
// Defines token classes and operators for mathematical expression evaluation
// ------------------------------------------------------------------------------------------------

#region Class Token -------------------------------------------------------------------------------
/// <summary>Represents the base class for all expression tokens.</summary>
abstract class Token { }
#endregion

#region Class TNumber -----------------------------------------------------------------------------
/// <summary>Represents a token that holds a numeric value </summary>
abstract class TNumber : Token {
   #region Properties -----------------------------------------------
   /// <summary>Gets the numeric value represented by the token </summary>
   public abstract double Value { get; }
   #endregion
}
#endregion

#region Class TLiteral ----------------------------------------------------------------------------
/// <summary>Represents a numeric literal in an expression </summary>
class TLiteral : TNumber {
   #region Constructor ----------------------------------------------
   /// <summary>Initializes a literal token with the specified value</summary>
   public TLiteral (double f) => mValue = f;
   #endregion

   #region Properties -----------------------------------------------
   /// <summary>Gets the value of the numeric literal</summary>
   public override double Value => mValue;
   #endregion

   #region Methods --------------------------------------------------
   /// <summary>Returns the string representation of the literal</summary>
   public override string ToString () => $"literal:{Value}";
   #endregion

   #region Private data ---------------------------------------------
   // Stores the numeric value of the literal
   readonly double mValue;
   #endregion
}
#endregion

#region Class TVariable ---------------------------------------------------------------------------
/// <summary>Represents a variable token whose value is retrieved from the evaluator</summary>
class TVariable : TNumber {
   #region Constructor ----------------------------------------------
   /// <summary>Initializes a variable token with its evaluator and name</summary>
   public TVariable (Evaluator eval, string name) => (Name, mEval) = (name, eval);
   #endregion

   #region Properties -----------------------------------------------
   /// <summary>Gets the name of the variable </summary>
   public string Name { get; private set; }

   /// <summary>Gets the current value assigned to the variable</summary>
   public override double Value => mEval.GetVariable (Name);
   #endregion

   #region Methods --------------------------------------------------
   /// <summary>Returns the string representation of the variable</summary>
   public override string ToString () => $"var:{Name}";
   #endregion

   #region Private data ---------------------------------------------
   // Stores the evaluator used to retrieve the variable value
   readonly Evaluator mEval;
   #endregion
}
#endregion

#region Class TOperator ---------------------------------------------------------------------------
/// <summary>Provides the base functionality for expression operators.</summary>
abstract class TOperator : Token {
   #region Constructor ----------------------------------------------
   // Initializes the operator with its evaluator and bracket priority level
   protected TOperator (Evaluator eval, int level) => (mEval, mLevel) = (eval, level);
   #endregion

   #region Properties -----------------------------------------------
   /// <summary>Gets the operator priority including the bracket priority</summary>
   public int Priority => BasicPriority + mLevel;

   // Gets the basic priority of the operator type
   protected abstract int BasicPriority { get; }
   #endregion

   #region Private data ---------------------------------------------
   // Stores the evaluator associated with the operator.
   readonly protected Evaluator mEval;

   // Stores the additional priority contributed by enclosing brackets.
   readonly int mLevel;
   #endregion
}
#endregion

#region Class TOpArithmetic -----------------------------------------------------------------------
/// <summary>Represents arithmetic operators such as addition and multiplication.</summary>
class TOpArithmetic : TOperator {
   #region Constructor ----------------------------------------------
   /// <summary>Initializes an arithmetic operator with its symbol and priority level.</summary>
   public TOpArithmetic (Evaluator eval, char ch, int level) : base (eval, level) => Op = ch;
   #endregion

   #region Properties -----------------------------------------------
   /// <summary>Gets the arithmetic operator symbol.</summary>
   public char Op { get; private set; }

   // Gets the basic priority assigned to the arithmetic operator
   protected override int BasicPriority => sPriority[Op];
   #endregion

   #region Methods --------------------------------------------------
   /// <summary>Returns the string representation of the arithmetic operator </summary>
   public override string ToString () => $"op:{Op}:{Priority}";

   /// <summary>Evaluates the arithmetic operation using two operands </summary>
   public double Evaluate (double a, double b) {
      return Op switch {
         '+' => a + b,
         '-' => a - b,
         '*' => a * b,
         '/' => a / b,
         '^' => Math.Pow (a, b),
         _ => throw new EvalException ($"Unknown operator: {Op}"),
      };
   }
   #endregion

   #region Private data ---------------------------------------------
   // Stores the priority assigned to each arithmetic operator
   static Dictionary<char, int> sPriority = new () {
      ['='] = 0, ['+'] = 1, ['-'] = 1, ['*'] = 2, ['/'] = 2, ['^'] = 4,
   };
   #endregion
}
#endregion

#region Class TOpUnary ----------------------------------------------------------------------------
/// <summary>Represents unary prefix operators such as positive and negative signs</summary>
class TOpUnary : TOperator {
   #region Constructor ----------------------------------------------
   /// <summary>Initializes a unary operator with its symbol and priority level</summary>
   public TOpUnary (Evaluator eval, char ch, int level) : base (eval, level) => Op = ch;
   #endregion

   #region Properties -----------------------------------------------
   /// <summary>Gets the unary operator symbol</summary>
   public char Op { get; private set; }

   // Gets the basic priority of the unary operator
   protected override int BasicPriority => 3;
   #endregion

   #region Methods --------------------------------------------------
   /// <summary>Returns the string representation of the unary operator</summary>
   public override string ToString () => $"unary:{Op}:{Priority}";

   /// <summary>Evaluates the unary operation using the specified operand</summary>
   public double Evaluate (double f) {
      return Op switch {
         '+' => f,
         '-' => -f,
         _ => throw new EvalException ($"Unknown unary operator: {Op}"),
      };
   }
   #endregion
}
#endregion

#region Class TOpFunction --------------------------------------------------------------------------
/// <summary>Represents mathematical functions that operate on a single operand</summary>
class TOpFunction : TOperator {
   #region Constructor ----------------------------------------------
   /// <summary>Initializes a function token with its name and priority level</summary>
   public TOpFunction (Evaluator eval, string name, int level) : base (eval, level) => Func = name;
   #endregion

   #region Properties -----------------------------------------------
   /// <summary>Gets the name of the mathematical function</summary>
   public string Func { get; private set; }

   // Gets the basic priority of the mathematical function
   protected override int BasicPriority => 5;
   #endregion

   #region Methods --------------------------------------------------
   /// <summary>Returns the string representation of the mathematical function</summary>
   public override string ToString () => $"func:{Func}:{Priority}";

   /// <summary>Evaluates the mathematical function using the specified operand </summary>
   public double Evaluate (double f) {
      return Func switch {
         "sin" => Math.Sin (D2R (f)),
         "cos" => Math.Cos (D2R (f)),
         "tan" => Math.Tan (D2R (f)),
         "sqrt" => Math.Sqrt (f),
         "log" => Math.Log (f),
         "exp" => Math.Exp (f),
         "asin" => R2D (Math.Asin (f)),
         "acos" => R2D (Math.Acos (f)),
         "atan" => R2D (Math.Atan (f)),
         _ => throw new EvalException ($"Unknown function: {Func}")
      };
   }
   #endregion

   #region Implementation--------------------------------------------
   // Converts an angle from degrees to radians
   double D2R (double f) => f * Math.PI / 180;

   // Converts an angle from radians to degrees
   double R2D (double f) => f * 180 / Math.PI;
   #endregion
}
#endregion

#region Class TPunctuation -------------------------------------------------------------------------
/// <summary>Represents punctuation symbols such as opening and closing brackets</summary>
class TPunctuation : Token {
   #region Constructor ----------------------------------------------
   /// <summary>Initializes a punctuation token with the specified character</summary>
   public TPunctuation (char ch) => Punct = ch;
   #endregion

   #region Properties -----------------------------------------------
   /// <summary>Gets the punctuation character</summary>
   public char Punct { get; private set; }
   #endregion

   #region Methods --------------------------------------------------
   /// <summary>Returns the string representation of the punctuation token</summary>
   public override string ToString () => $"punct:{Punct}";
   #endregion
}
#endregion

#region Class TEnd --------------------------------------------------------------------------------
/// <summary>Represents the end of an expression</summary>
class TEnd : Token {
   #region Methods --------------------------------------------------
   /// <summary>Returns the string representation of the end token</summary>
   public override string ToString () => "end";
   #endregion
}
#endregion

#region Class TError ------------------------------------------------------------------------------
/// <summary>Represents an error encountered while tokenizing an expression</summary>
class TError : Token {
   #region Constructor ----------------------------------------------
   /// <summary>Initializes an error token with the specified error message</summary>
   public TError (string message) => Message = message;
   #endregion

   #region Properties -----------------------------------------------
   /// <summary>Gets the error message.</summary>
   public string Message { get; private set; }
   #endregion

   #region Methods --------------------------------------------------
   /// <summary>Returns the string representation of the error token</summary>
   public override string ToString () => $"error:{Message}";
   #endregion
}
#endregion