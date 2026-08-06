// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2026.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------------------------------------
// Program.cs
// Program to guess a number thought of by the user between 1 and 100 using the user's responses
// ------------------------------------------------------------------------------------------------
Console.WriteLine ("Think of a number between 1 and 100, I'll guess it!");
Console.WriteLine ("Answer each question with 'Y' for Yes or 'N' for No.");
int number = 0;
int divisor = 2;
int remainder = 1;
for (int i = 0; i < 7; i++) {
   while (true) {
      Console.Write ($"Is the remainder when divided by {divisor} >= {remainder}? (Y/N): ");
      string? answer = Console.ReadLine ();
      if (answer == null) {
         Console.WriteLine ("Input cannot be null. Please enter Y or N.");
         continue;
      }
      answer = answer.Trim ();
      if (string.Equals (answer, "Y", StringComparison.OrdinalIgnoreCase)) {
         number += remainder;
         break;
      }
      if (string.Equals (answer, "N", StringComparison.OrdinalIgnoreCase)) break;
      Console.WriteLine ("Invalid input. Please enter only Y or N.");
   }
   remainder = divisor;
   divisor *= 2;
}
Console.WriteLine ($"The number you thought of is {number}.");