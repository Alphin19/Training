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
      Write ($"{"Input",-18} {"Result",-15} {"Expected",-15} ");
      WriteLine ("Status");
      foreach (string testCase in testCases) {
         double result = DoubleParse (testCase);
         double expected = double.TryParse (testCase, out double actual) ? actual : double.NaN;
         bool passed = double.IsNaN (expected) ? double.IsNaN (result) : result.Equals (expected);
         Write ($"{testCase,-18} {result,-15:G8} {expected,-15:G8} ");
         ForegroundColor = passed ? ConsoleColor.Green : ConsoleColor.Red;
         WriteLine (passed ? "PASS" : "FAIL");
         ResetColor ();
      }
   }
   static double DoubleParse (string input) {
      if (input.Length == 0) return NAN;
      input = input.Trim ();
      int pos = 0;
      bool hasDigits = false;
      bool isNegative = false;
      double num = 0;
      // 1. Parse the sign
      if (input[0] == '+' || input[0] == '-') {
         isNegative = input[0] == '-';
         pos++;
      }
      // 2. Parse the digits
      while (pos < input.Length && char.IsDigit (input[pos])) {
         num = num * 10 + (input[pos] - '0');
         hasDigits = true;
         pos++;
      }
      // 3. Parse the decimal point and decimal digits
      if (pos < input.Length && input[pos] == '.') {
         pos++;
         double decimalNo = 0;
         double divisor = 1;
         while (pos < input.Length && char.IsDigit (input[pos])) {
            decimalNo = decimalNo * 10 + (input[pos] - '0');
            divisor = divisor * 10;
            hasDigits = true;
            pos++;
         }
         num += decimalNo / divisor;
      }
      if (!hasDigits) return NAN;
      if (pos < input.Length &&
          (input[pos] == 'e' || input[pos] == 'E')) {
         pos++;
         bool expNegative = false;
         // 4. Parse exponent sign
         if (pos < input.Length &&
             (input[pos] == '+' || input[pos] == '-')) {
            expNegative = input[pos] == '-';
            pos++;
         }
         // 5. Parse the exponent digit
         int exp = 0;
         bool hasExpDigits = false;
         while (pos < input.Length && char.IsDigit (input[pos])) {
            int digit = input[pos] - '0';
            exp = exp * 10 + digit;
            hasExpDigits = true;
            pos++;
         }
         if (!hasExpDigits) return NAN;
         if (expNegative) exp = -exp;
         num *= Math.Pow (10, exp);
      }
      if (pos != input.Length) return NAN;
      // 6. Apply the sign and return parsed digit
      return isNegative ? -num : num;
   }
   const double NAN = double.NaN;
}
