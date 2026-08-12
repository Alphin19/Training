// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2026.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------------------------------------
// Program.cs
// Program to guess a number between 1 and 127 by asking the user questions based on the remainder.
// ------------------------------------------------------------------------------------------------
Console.WriteLine ("Think of a number between 1 and 127, I'll guess it!");
Console.WriteLine ("Answer each question with 'Y' for Yes or 'N' for No.");
int number = 0;
int divisor = 2;
int remainder = 1;
for (int i = 0; i < 7; i++) {
   string answer;
   while (true) {
      Console.Write ($"Is the remainder when divided by {divisor} >= {remainder}? (Y/N): ");
      answer = (Console.ReadLine () ?? "").Trim ().ToUpperInvariant ();
      if (answer == "Y" || answer == "N") {
         break;
      }
      Console.WriteLine ("Invalid input. Please enter Y or N.");
   }
   if (answer == "Y") number += remainder;
   remainder = divisor;
   divisor *= 2;
}
Console.WriteLine ($"Your number is {number}!");