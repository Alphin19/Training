// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------------------------------------
// Program.cs
// A07: double.Parse
// ------------------------------------------------------------------------------------------------
using static System.Console;
class Program {
   static void Main () {
      string[] testCases = ["42.75", "-18.625", "3.14e2", "-2.5e-3", "100",
         "0.000045", "+78.5", "9e4", "25.0", "7.", ".75", "4.5e", "8e+",
         "5.2e-2.5", "12abc", "--45", "3..14", "1.2*3", "-0.0045",
         "  56.789  ", "90E2", "+7.25e+2", "0.5e-4" ];
      foreach (string testCase in testCases) {
         double result = DoubleParse (testCase);
         WriteLine ($"Input: \"{testCase}\"  Result: {result}");
      }
   }
   static double DoubleParse (string input) {
      input = input.Trim ();
      if (input.Length == 0) return NAN;
      int position = 0;
      bool hasDigits = false;
      bool isNegative = false;
      double number = 0;
      if (input[0] == '+' || input[0] == '-') {
         isNegative = input[0] == '-';
         position++;
      }
      while (position < input.Length && char.IsDigit (input[position])) {
         int digit = input[position] - '0';
         number = number * 10 + digit;
         hasDigits = true;
         position++;
      }
      if (position < input.Length && input[position] == '.') {
         position++;
         double decimalNumber = 0;
         int decimalDigits = 0;
         while (position < input.Length && char.IsDigit (input[position])) {
            int digit = input[position] - '0';
            decimalNumber = decimalNumber * 10 + digit;
            decimalDigits++;
            hasDigits = true;
            position++;
         }
         number += decimalNumber / Math.Pow (10, decimalDigits);
      }
      if (!hasDigits) return NAN;
      if (position < input.Length &&
          (input[position] == 'e' || input[position] == 'E')) {
         position++;
         bool exponentNegative = false;
         // Parse exponent sign
         if (position < input.Length &&
             (input[position] == '+' || input[position] == '-')) {
            exponentNegative = input[position] == '-';
            position++;
         }
         int exponent = 0;
         bool hasExponentDigits = false;
         while (position < input.Length && char.IsDigit (input[position])) {
            int digit = input[position] - '0';
            exponent = exponent * 10 + digit;
            hasExponentDigits = true;
            position++;
         }
         if (!hasExponentDigits) return NAN;
         if (exponentNegative) exponent = -exponent;
         number *= Math.Pow (10, exponent);
      }
      if (position != input.Length) return NAN;
      if (isNegative) number = -number;
      return number;
   }
   const double NAN = double.NaN;
}
