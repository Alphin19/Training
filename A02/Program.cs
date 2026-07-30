using System;

Random random = new Random ();
int secretNumber = random.Next (1, 101);
int guess;
int attempts = 0;

Console.WriteLine ("I have generated a number between 1 and 100.");

do {
   Console.Write ("Enter your guess: ");
   guess = Convert.ToInt32 (Console.ReadLine ());

   attempts++;

   if (guess > secretNumber) {
      Console.WriteLine ("Too High!");
   } else if (guess < secretNumber) {
      Console.WriteLine ("Too Low!");
   } else {
      Console.WriteLine ("Correct!");
      Console.WriteLine ($"You guessed it in {attempts} attempts.");
   }

} while (guess != secretNumber); 

