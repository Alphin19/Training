// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2026.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------------------------------------
// Program.cs
// Spellbee game
// ------------------------------------------------------------------------------------------------
char[] validLetters = { 'U', 'X', 'A', 'L', 'T', 'N', 'E' };
char firstLetter = validLetters[0];
string filePath = "words.txt";
string[] words = File.ReadAllLines (filePath);
int score = 0;
Dictionary<string, (int Score, bool IsPangram)> dict = [];
foreach (string word in words) {
   string validWord = word.ToUpper ();
   if (!validWord.Contains (firstLetter) || !validWord.All (validLetters.Contains) ||
      validWord.Length < 4 || dict.ContainsKey (validWord)) continue;
   int wordScore = validWord.Length == 4 ? 1 : validWord.Length;
   bool isPangram = validLetters.All (validWord.Contains);
   if (isPangram) wordScore += 7;
   score += wordScore;
   dict.Add (validWord, (wordScore, isPangram));
}
foreach (var item in dict.OrderByDescending (x => x.Value.Score).ThenBy (x => x.Key)) {
   if (item.Value.IsPangram) Console.ForegroundColor = ConsoleColor.Green;
   Console.WriteLine ($"{item.Value.Score,3}. {item.Key}");
   Console.ResetColor ();
}
Console.WriteLine ("----");
Console.WriteLine ($"{score} total");