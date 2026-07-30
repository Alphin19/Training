//Program for Guessing a Number Between 1 and 100
int secretNumber =  new Random.Next (1, 101);
int guess;
int attempts = 0;
Console.WriteLine ("I have generated a number between 1 and 100.");
do {
   Console.Write ("Enter your guess: ");
   if (!int.TryParse (input, out guess)) 
   {
      Console.WriteLine ("Please enter a valid number.");
      continue;
   }
   attempts++;
   if (guess > secretNumber) Console.WriteLine ("Your guess is too high!");
   else if (guess < secretNumber) Console.WriteLine ("You guess is too Low!");
   else {
      Console.WriteLine ("Correct!");
      Console.WriteLine ($"You guessed it in {attempts} attempts.");
   }
} while (guess != secretNumber); 

