// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2026.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------------------------------------
// Program.cs
// Program to guess a number thought of by the user between 1 and 100 using the user's responses
// ------------------------------------------------------------------------------------------------
Console.WriteLine ("Think of a number between 1 and 100.");
Console.WriteLine ("Enter:");
Console.WriteLine ("H - Higher");
Console.WriteLine ("L - Lower");
Console.WriteLine ("C - Correct");
int low = 1;
int high = 100;
while (low <= high) {
   int guess = (low + high) / 2;
   while (true) {
      Console.Write ($"Is your number {guess}? (H/L/C): ");
      string? response = Console.ReadLine ();
      if (string.IsNullOrWhiteSpace (response)) {
         Console.WriteLine ("Please enter H, L or C.");
         continue;
      }
      response = response.Trim ();
      if (string.Equals (response, "C", StringComparison.OrdinalIgnoreCase)) {
         Console.WriteLine ($"I guessed your number! It is {guess}.");
         return;
      }
      if (string.Equals (response, "H", StringComparison.OrdinalIgnoreCase)) {
         low = guess + 1;
         break;
      }
      if (string.Equals (response, "L", StringComparison.OrdinalIgnoreCase)) {
         high = guess - 1;
         break;
      }
      Console.WriteLine ("Invalid input. Please enter H, L or C.");
   }
}
Console.WriteLine ("The responses are inconsistent.");