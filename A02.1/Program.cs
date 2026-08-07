// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2026.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------------------------------------
// Program.cs
// Program to guess a number thought of by the user between 1 and 100 using the user's responses
// ------------------------------------------------------------------------------------------------
int low = 1;
int high = 100;
Console.WriteLine ($"Think of a number between {low} and {high}.");
Console.WriteLine ("Enter:\nH - Higher\nL - Lower\nC - Correct");
while (low <= high) {
   int guess = (low + high) / 2;
   Console.Write ($"Is your number higher than, lower than, or equal to {guess}? (H/L/C): ");
   string? response = Console.ReadLine ()?.Trim ().ToUpperInvariant ();
   switch (response) {
      case "H":
         low = guess + 1;
         break;
      case "L":
         high = guess - 1;
         break;
      case "C":
         Console.WriteLine ($"I guessed your number! It is {guess}.");
         return;
      default:
         Console.WriteLine ("Invalid input. Please enter H, L or C.");
         break;
   }
}
Console.WriteLine ("The responses are inconsistent.");