// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2026.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------------------------------------
// Program.cs
// Program to generate a random number between 1 and 100 and allow the user to guess it.
// ------------------------------------------------------------------------------------------------
int secretNumber = new Random ().Next (1, 101);
int attempts = 0;
Console.WriteLine ("I have generated a number between 1 and 100.");
while (true) {
   Console.Write ("Enter your guess: ");
   string? input = Console.ReadLine ();
   if (!int.TryParse (input, out int guess)) {
      Console.WriteLine ("Please enter a valid number.");
      continue;
   }
   if (guess < 1 || guess > 100) {
      Console.WriteLine ("Please enter a number between 1 and 100.");
      continue;
   }
   attempts++;
   if (guess > secretNumber) Console.WriteLine ("Your guess is too high!");
   else if (guess < secretNumber) Console.WriteLine ("Your guess is too low!");
   else {
      Console.WriteLine ($"Correct!,you guessed it in {attempts} attempts");
      break;
   }
}